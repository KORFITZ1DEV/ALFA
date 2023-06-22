using System.Diagnostics;
using ALFA.AST_Nodes;
using ALFA.Types;
using Antlr4.Runtime.Tree;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public class BuildASTVisitor : ALFABaseVisitor<Node>
{
    private SymbolTable _symbolTable;

    //A function that replaces an id in an expression with the value from the symbol table.
    //int offset = 100; offset = -offset; The value of offset in the symbol table must assume an integer value when this is executed in the same scope
    //otherwise the value can never be retrieved
    private void AddLocalValueToIdInExpr(ExprNode expr, string identifier)
    {
        if (expr.Left is ExprNode leftExpr)
        {
            AddLocalValueToIdInExpr(leftExpr, identifier);
        }

        if (expr.Right is ExprNode rightExpr)
        {
            AddLocalValueToIdInExpr(rightExpr, identifier);
        }

        if (expr.Left is IdNode idNodeLeft)
        {
            var symbol = _symbolTable.RetrieveSymbol(idNodeLeft.Identifier);
            if (symbol?.Name == identifier)
            {
                idNodeLeft.LocalValue = symbol.Value;
            }
        }

        if (expr.Right is IdNode idNodeRight)
        {
            var symbol = _symbolTable.RetrieveSymbol(idNodeRight.Identifier);
            if (symbol?.Name == identifier)
            {
                idNodeRight.LocalValue = symbol.Value;
            }
        }
        
    }

    public BuildASTVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    public override ProgramNode VisitProgram(ALFAParser.ProgramContext context)
    {
        List<Node> childList = new List<Node>();

        foreach (var stmt in context.stmt())
        {
            childList.Add(Visit(stmt));
        }

        return new ProgramNode(childList!);
    }

    public override Node VisitStmt(ALFAParser.StmtContext context)
    {
        if (context.varDcl() != null)
            return VisitVarDcl(context.varDcl());

        if (context.assignStmt() != null)
        {
            AssignStmtNode assignmentNode = VisitAssignStmt(context.assignStmt());
            Symbol symbol = _symbolTable.RetrieveSymbol(assignmentNode.Identifier);
            if (symbol == null)
                throw new UndeclaredVariableException($"You are trying to assign a value to variable called {assignmentNode.Identifier} that is undeclared on line {assignmentNode.Line} column {assignmentNode.Col}");
            return assignmentNode;
        }

        if (context.builtInAnimCall() != null)
            return VisitBuiltInAnimCall(context.builtInAnimCall());

        if (context.ifStmt() != null)
            return VisitIfStmt(context.ifStmt());

        if (context.loopStmt() != null)
            return VisitLoopStmt(context.loopStmt());

        if (context.paralStmt() != null)
            return VisitParalStmt(context.paralStmt());

        throw new Exception("Invalid statement");
    }

    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        VarDclNode newVarDclNode = new VarDclNode(context.Start.Line, context.Start.Column);

        ALFATypes.TypeEnum typeEnum;

        switch (context.type().GetText())
        {
            case "int":
                typeEnum = ALFATypes.TypeEnum.@int;
                break;
            case "bool":
                typeEnum = ALFATypes.TypeEnum.@bool;
                break;
            case "rect":
                typeEnum = ALFATypes.TypeEnum.rect;
                break;
            default:
                throw new TypeException("Invalid type on line " + context.Start.Line + ":" + context.Start.Column);
        }

        newVarDclNode.Type = typeEnum;

        ALFAParser.AssignStmtContext assignStmtContext = context.assignStmt();
        if (assignStmtContext != null)
        {
            AssignStmtNode assignStmtNode = VisitAssignStmt(assignStmtContext);
            assignStmtNode.VarDclParentType = typeEnum; 
            newVarDclNode.AssignStmt = assignStmtNode;
        }

        string id = newVarDclNode.AssignStmt.Identifier;
        var symbol = _symbolTable.RetrieveSymbol(id);
        if (symbol != null && symbol.Depth == _symbolTable._depth)
            throw new VariableAlreadyDeclaredException($"Variable {id} already declared on line {symbol.LineNumber}:{symbol.ColumnNumber}");

        _symbolTable.EnterSymbol(new Symbol(id, newVarDclNode.AssignStmt.Value, typeEnum, context.Start.Line, context.Start.Column));

        return newVarDclNode;
    }

    public override AssignStmtNode VisitAssignStmt(ALFAParser.AssignStmtContext context)
    {
        AssignStmtNode newAssignStmtNode = new AssignStmtNode(context.Start.Line, context.Start.Column);
        newAssignStmtNode.Identifier = context.ID().GetText();
        if (context.builtInCreateShapeCall() != null)
        {
            var builtInCreateShapeCall = (BuiltInCreateShapeCallNode)Visit(context.builtInCreateShapeCall());
            newAssignStmtNode.Value = builtInCreateShapeCall;

        }
        else if (context.expr() != null)
        {
            var expr = Visit((dynamic)context.expr());
            if(expr is ExprNode exprNode) AddLocalValueToIdInExpr(exprNode, newAssignStmtNode.Identifier);

            newAssignStmtNode.Value = expr;
        }

        
        string id = newAssignStmtNode.Identifier;
        var symbol = _symbolTable.RetrieveSymbol(id);
        if (symbol != null)
            symbol.Value = newAssignStmtNode.Value;

        return newAssignStmtNode;
    }

    public override BuiltInAnimCallNode VisitBuiltInAnimCall(ALFAParser.BuiltInAnimCallContext context)
    {
        var type = context.builtInAnim().GetText();
        ALFATypes.BuiltInAnimEnum builtInAnimEnum;

        switch (type)
        {
            case "move":
                builtInAnimEnum = ALFATypes.BuiltInAnimEnum.move;
                break;
            case "wait":
                builtInAnimEnum = ALFATypes.BuiltInAnimEnum.wait;
                break;
            default:
                throw new UnknownBuiltinException("Unknown built-in animation");
        }

        BuiltInAnimCallNode builtInAnimCallNodeNode = new BuiltInAnimCallNode(builtInAnimEnum, new List<Node>(), context.Start.Line, context.Start.Column);

        if (context.actualParams() != null)
        {
            int i = 0;
            foreach (var exprCtx in context.actualParams().expr())
            {
                var expr = Visit((dynamic)exprCtx);

                switch (expr)
                {
                    case IdNode idNode:
                        string id = idNode.Identifier;
                        Symbol? sym = _symbolTable.RetrieveSymbol(id);
                        if (sym == null)
                            throw new UndeclaredVariableException($"Variable {id} not declared at line {exprCtx.Start.Line}:{exprCtx.Start.Column}");
                        builtInAnimCallNodeNode.Arguments.Add(idNode);
                        break;
                    case NumNode numNode:
                        builtInAnimCallNodeNode.Arguments.Add(numNode);
                        break;
                    case BoolNode boolNode:
                        throw new TypeException($"Boolean type {boolNode.Value} is not allowed in {builtInAnimCallNodeNode.Type.ToString()} on line " + expr.Line + ":" + expr.Column);
                    default:
                        builtInAnimCallNodeNode.Arguments.Add(expr);
                        break;
                }

                i++;
            }
        }

        return builtInAnimCallNodeNode;
    }

    public override BuiltInParalAnimCallNode VisitBuiltInParalAnimCall(ALFAParser.BuiltInParalAnimCallContext context)
    {
        var type = context.builtInParalAnim().GetText();
        ALFATypes.BuiltInParalAnimEnum builtInParalAnimEnum;

        switch (type)
        {
            case "move":
                builtInParalAnimEnum = ALFATypes.BuiltInParalAnimEnum.move;
                break;
            default:
                throw new UnknownBuiltinException("Unknown built-in animation");
        }

        BuiltInParalAnimCallNode builtInParalAnimCallNodeNode = new BuiltInParalAnimCallNode(builtInParalAnimEnum, new List<Node>(), context.Start.Line, context.Start.Column);

        if (context.actualParams() != null)
        {
            foreach (var exprCtx in context.actualParams().expr())
            {
                var expr = Visit((dynamic)exprCtx);

                switch (expr)
                {
                    case IdNode idNode:
                        string id = idNode.Identifier;
                        Symbol? sym = _symbolTable.RetrieveSymbol(id);
                        if (sym == null)
                            throw new UndeclaredVariableException($"Variable {id} not declared at line {expr.Line}:{expr.Col}");
                        builtInParalAnimCallNodeNode.Arguments.Add(idNode);
                        break;
                    case NumNode numNode:
                        builtInParalAnimCallNodeNode.Arguments.Add(numNode);
                        break;
                    case BoolNode boolNode:
                        throw new TypeException($"Boolean type {boolNode.Value} is not allowed in {builtInParalAnimCallNodeNode.Type.ToString()} on line " + expr.Line + ":" + expr.Column);
                    default:
                        builtInParalAnimCallNodeNode.Arguments.Add(expr);
                        break;
                }
            }
        }

        return builtInParalAnimCallNodeNode;
    }

    public override BuiltInCreateShapeCallNode VisitBuiltInCreateShapeCall(ALFAParser.BuiltInCreateShapeCallContext context)
    {
        string? identifier = null;
        string type = context.builtInCreateShape().GetText();
        ALFATypes.CreateShapeEnum createShapeEnum;

        var parent = (ALFAParser.AssignStmtContext)context.Parent;
        identifier = parent.ID().GetText();

        switch (type)
        {
            case "createRect":
                createShapeEnum = ALFATypes.CreateShapeEnum.createRect;
                break;
            default:
                throw new UnknownBuiltinException($"Cannot assign {identifier} to unknown built-in function on line {context.Start.Line}:{context.Start.Column}");
        }

        BuiltInCreateShapeCallNode builtInCreateShapeCallNode = new BuiltInCreateShapeCallNode(createShapeEnum, new List<Node>(), context.Start.Line, context.Start.Column);

        if (context.actualParams() != null)
        {
            foreach (var exprCtx in context.actualParams().expr())
            {
                var expr = Visit((dynamic)exprCtx);

                switch (expr)
                {
                    case IdNode idNode:
                        string id = idNode.Identifier;
                        Symbol? sym = _symbolTable.RetrieveSymbol(id);
                        if (sym == null)
                            throw new UndeclaredVariableException($"Variable {id} not declared at line {exprCtx.Start.Line}:{exprCtx.Start.Column}");

                        builtInCreateShapeCallNode.Arguments.Add(idNode);
                        break;
                    case NumNode numNode:
                        int value = numNode.Value;
                        builtInCreateShapeCallNode.Arguments.Add(numNode);
                        break;
                    case BoolNode boolNode:
                        throw new TypeException($"Boolean type {boolNode.Value} is not allowed in {builtInCreateShapeCallNode.Type.ToString()} on line " + exprCtx.Start.Line + ":" + exprCtx.Start.Column);
                    default:
                        builtInCreateShapeCallNode.Arguments.Add(expr);
                        break;
                }
            }
        }

        return builtInCreateShapeCallNode;
    }

    public override IfStmtNode VisitIfStmt(ALFAParser.IfStmtContext context)
    {
        List<Node> expressions = new List<Node>();
        List<BlockNode> blocks = new List<BlockNode>();

        int i = 0;
        foreach (var exprCtx in context.expr()) // if and else-ifs
        {
            _symbolTable.OpenScope();

            var expr = Visit((dynamic)exprCtx);
            expressions.Add(expr);

            BlockNode block = new BlockNode();
            foreach (var stmtCtx in context.block(i).stmt())
            {
                block.Statements.Add(Visit(stmtCtx));
                
            }
            blocks.Add(block);
            _symbolTable.CloseScope();
            i++;
        }
        if (context.block().Length > context.expr().Length) // else
        {
            _symbolTable.OpenScope();
            BlockNode block = new BlockNode();
            foreach (var stmtCtx in context.block().Last().stmt())
            {
                block.Statements.Add(Visit(stmtCtx));
            }
            _symbolTable.CloseScope();
            blocks.Add(block);
        }

        return new IfStmtNode(expressions, blocks, context.Start.Line, context.Start.Column);
    }

    public override LoopStmtNode VisitLoopStmt(ALFAParser.LoopStmtContext context)
    {
        _symbolTable.OpenScope();
        AssignStmtNode assignStmtNode = new AssignStmtNode(context.Start.Line, context.Start.Column);
        assignStmtNode.Identifier = context.ID().GetText();
        assignStmtNode.Value = Visit(context.expr(0));

        _symbolTable.EnterSymbol(new Symbol(assignStmtNode.Identifier, assignStmtNode.Value, ALFATypes.TypeEnum.@int, context.Start.Line, context.Start.Column));

        Node to = Visit(context.expr(1));

        BlockNode block = new BlockNode();
        foreach (var stmtCtx in context.block().stmt())
        {
            block.Statements.Add(Visit(stmtCtx));
        }
        _symbolTable.CloseScope();
        return new LoopStmtNode(assignStmtNode, to, block, context.Start.Line, context.Start.Column);
    }

    public override ParalStmtNode VisitParalStmt(ALFAParser.ParalStmtContext context)
    {
        ParalBlockNode paralBlock = new ParalBlockNode();

        foreach (var builtInParalAnimCallCtx in context.paralBlock().builtInParalAnimCall())
        {
            BuiltInParalAnimCallNode builtInParalAnimCallNode = VisitBuiltInParalAnimCall(builtInParalAnimCallCtx);

            paralBlock.Statements.Add(builtInParalAnimCallNode);
        }

        PropertyMerge(paralBlock);

        return new ParalStmtNode(paralBlock, context.Start.Line, context.Start.Column);
    }

    private void PropertyMerge(ParalBlockNode paralBlock) {
        Dictionary<string, List<Node>> shapesToCompare = new Dictionary<string, List<Node>>();
        
        foreach(BuiltInParalAnimCallNode callNode in paralBlock.Statements) {
            if (callNode.Arguments[0] is IdNode idArg)
            {
                string derivedId = TryDeriveIdInAssignment(idArg.Identifier);
                if (!shapesToCompare.ContainsKey(idArg.Identifier) && !shapesToCompare.ContainsKey(derivedId)) {
                    shapesToCompare.Add(idArg.Identifier, callNode.Arguments);
                }
                else {
                    TryMergeProperties(shapesToCompare, callNode.Arguments);
                }
            }
        }
    }

    private string TryDeriveIdInAssignment(string identifier)
    {
        var id = identifier;
        
        var symVal = _symbolTable.RetrieveSymbol(identifier)?.Value;
        if (symVal is IdNode symIdNode)
        {
            id = symIdNode.Identifier;
            return TryDeriveIdInAssignment(id);
        }
        return id;
    }

    private void TryMergeProperties(Dictionary<string, List<Node>> shapesToCompare, List<Node> newCallNodeArgs) {
        if (newCallNodeArgs[0] is IdNode idArg)
        {
            string identifier = TryDeriveIdInAssignment(idArg.Identifier);
            int i = 1;
            foreach(var dictArg in shapesToCompare[identifier].Skip(1))
            {
                if (i == newCallNodeArgs.Count - 1) continue;
                if(dictArg is NumNode numArg && numArg.Value == 0) {}
                //If one of the args is a NumNode with value 0 then we know the builtInParalAnimCall is allowed 
                else if (newCallNodeArgs[i] is NumNode newNumArg && newNumArg.Value == 0) {
                    newCallNodeArgs[i] = dictArg;
                }
                else {
                    throw new AttemptingToChangePropertyOfSameShapeInParalException($"Attempting to change the same property in a shape is not allowed. Error on line {newCallNodeArgs[i].Line} column {newCallNodeArgs[i].Col}");
                }
                i++;
            }

            shapesToCompare[identifier] = newCallNodeArgs;
        }
    }

    public override ExprNode VisitParens(ALFAParser.ParensContext context)
    {
        var expr = Visit((dynamic)context.expr());
        return new ExprNode("()", expr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitNot(ALFAParser.NotContext context)
    {
        var expr = Visit((dynamic)context.expr());
        return new ExprNode("!", expr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitUnaryMinus(ALFAParser.UnaryMinusContext context)
    {
        var expr = Visit((dynamic)context.expr());
        return new ExprNode("u-", expr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitMulDiv(ALFAParser.MulDivContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));
        var op = context.op.Text;

        return new ExprNode(op, leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitAddSub(ALFAParser.AddSubContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));
        var op = context.op.Text;

        return new ExprNode(op, leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitRelational(ALFAParser.RelationalContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));
        var op = context.op.Text;

        return new ExprNode(op, leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitEquality(ALFAParser.EqualityContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));
        var op = context.op.Text;

        return new ExprNode(op, leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitAnd(ALFAParser.AndContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));

        return new ExprNode("and", leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override ExprNode VisitOr(ALFAParser.OrContext context)
    {
        var leftExpr = Visit((dynamic)context.expr(0));
        var rightExpr = Visit((dynamic)context.expr(1));

        return new ExprNode("or", leftExpr, rightExpr, context.Start.Line, context.Start.Column);
    }
    public override IdNode VisitId(ALFAParser.IdContext context)
    {
        return new IdNode(context.ID().GetText(), context.Start.Line, context.Start.Column);
    }
    public override NumNode VisitNum(ALFAParser.NumContext context)
    {
        return new NumNode(int.Parse(context.NUM().GetText()), context.Start.Line, context.Start.Column);
    }
    public override BoolNode VisitBoolean(ALFAParser.BooleanContext context)
    {
        return new BoolNode(Boolean.Parse(context.@bool().GetText()), context.Start.Line, context.Start.Column);
    }
    


}