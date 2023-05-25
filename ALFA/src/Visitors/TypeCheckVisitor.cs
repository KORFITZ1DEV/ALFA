using System.Linq.Expressions;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public class TypeCheckVisitor : ASTVisitor<Node>
{
    private SymbolTable _symbolTable;
    
//TODO move typecheck vardcl here
    public TypeCheckVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }

    public override ProgramNode Visit(ProgramNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit(stmt);
        }
        return node;
    }

    private void typeCheckIdNodeVarDcl(IdNode idNode, ALFATypes.TypeEnum type)
    {
        Symbol symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
        if (symbol.Type != type)
        {
            throw new TypeException($"You are assigning something of type {symbol.Type} on line {symbol.LineNumber} column {symbol.ColumnNumber} to a variable of type {type.ToString()}");
        }
    }
    

    public override Node Visit(VarDclNode node)
    {
        Visit(node.AssignStmt);

        if (node.AssignStmt.Value is IdNode idNode)
        {
            typeCheckIdNodeVarDcl(idNode, node.Type);
        }
        
        return node;
    }

    public override BuiltInAnimCallNode Visit(BuiltInAnimCallNode node)
    {
        List<ALFATypes.TypeEnum> nodeFormalParameters = FormalParameters.FormalParams[node.Type.ToString()];

        if (node.Arguments.Count != nodeFormalParameters.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {node.Type.ToString()}, expected {nodeFormalParameters.Count} but got {node.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in node.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != FormalParameters.FormalParams[node.Type.ToString()][i])
                        throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");

                    if (i == FormalParameters.FormalParams[node.Type.ToString()].Count() - 1)
                    {
                        if(idSymbol.Value is AssignStmtNode assStmt && assStmt.Value is NumNode numNode && numNode.Value <= 0) 
                            throw new NonPositiveAnimationDurationException($"The duration of an animation must be greater than 0 on line {idSymbol.LineNumber} column {idSymbol.ColumnNumber}");
                        else if (idSymbol.Value is NumNode numNode1 && numNode1.Value <= 0)
                        {
                            throw new NonPositiveAnimationDurationException($"The duration of an animation must be greater than 0 on line {idSymbol.LineNumber} column {idSymbol.ColumnNumber}");
                        }
                    }
                }
            }
            else if (actualParam is NumNode numNode)
            {
                if (nodeFormalParameters[i] != ALFATypes.TypeEnum.@int)
                    throw new ArgumentTypeException($"Invalid type expected {nodeFormalParameters[i]} but got {ALFATypes.TypeEnum.@int} on line {numNode.Line}:{numNode.Col}");
                if(i == FormalParameters.FormalParams[node.Type.ToString()].Count() - 1 && numNode.Value <= 0) 
                    throw new NonPositiveAnimationDurationException($"The duration of an animation must be greater than 0 on line {numNode.Line} column {numNode.Col}");

            }
            else if (actualParam is ExprNode exprNode)
            {
                Visit(exprNode);
            }
            i++;
        }
        return node;
    }

    public override Node Visit(BuiltInParalAnimCallNode node)
    {
        List<ALFATypes.TypeEnum> nodeFormalParameters = FormalParameters.FormalParams[node.Type.ToString()];

        if (node.Arguments.Count != nodeFormalParameters.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {node.Type.ToString()}, expected {nodeFormalParameters.Count} but got {node.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in node.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Visit(actualParam); //Not needed now but it is more extensible
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != FormalParameters.FormalParams[node.Type.ToString()][i])
                        throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                }
            }
            i++;
        }
        return node;
    }

    public override BuiltInCreateShapeCallNode Visit(BuiltInCreateShapeCallNode callNode)
    {
        List<ALFATypes.TypeEnum> nodeFormalParameters = FormalParameters.FormalParams[callNode.Type.ToString()];

        if (callNode.Arguments.Count != nodeFormalParameters.Count)
        {
            throw new InvalidNumberOfArgumentsException(
                $"Invalid number of arguments to {callNode.Type.ToString()}, expected {nodeFormalParameters.Count} but got {callNode.Arguments.Count} arguments");
        }

        int i = 0;
        foreach (var actualParam in callNode.Arguments)
        {
            if (actualParam is IdNode idNode)
            {
                Visit(actualParam); //Not needed now but it is more extensible
                Symbol? idSymbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
                if (idSymbol != null)
                {
                    if (idSymbol.Type != FormalParameters.FormalParams[callNode.Type.ToString()][i])
                        throw new ArgumentTypeException($"Invalid type, expected {nodeFormalParameters[i]} but got {idSymbol.Type} on line {idNode.Line}:{idNode.Col}");
                }
            }
            i++;
        }
        return callNode;
    }

    public override AssignStmtNode Visit(AssignStmtNode assNode)
    {
        Symbol? idSymbol = _symbolTable.RetrieveSymbol(assNode.Identifier);
        bool visitedChild = false;
        
        if (idSymbol != null)
        {
            if (idSymbol.Value is ExprNode exprValue)
            {
                EvaluateExpression(exprValue);
                visitedChild = true;
                switch (exprValue.Value.GetType().ToString())
                {
                    case "NumNode":
                        if (idSymbol.Type != ALFATypes.TypeEnum.@int) throw new ArgumentTypeException($"Invalid type, exception evaluates to an integer on line {exprValue.Value.Line}:{exprValue.Value.Col}");
                        break;
                    case "BoolNode":
                        if (idSymbol.Type != ALFATypes.TypeEnum.@bool) throw new ArgumentTypeException($"Invalid type, exception should evaluate to a boolean, but evaluates to a {idSymbol.Type} on line {exprValue.Value.Line}:{exprValue.Value.Col}");
                        break;
                }
            }

            if (idSymbol.Value is AssignStmtNode assNodeChild && assNodeChild.Value is IdNode idChildNode)
            {
                Symbol? idSymbolChild = _symbolTable.RetrieveSymbol(idChildNode.Identifier);
                if (idSymbolChild?.Type != idSymbol.Type)
                    throw new TypeException("Invalid type on line " + assNodeChild.Line + ": " + "column: " + assNodeChild.Col);
            }
        }
        if(!visitedChild) Visit(assNode.Value);

        return assNode;
    }

    public override IfStmtNode Visit(IfStmtNode ifNode)
    {
        foreach (var expr in ifNode.Expressions)
        {
            Visit((dynamic)expr);
        }

        return ifNode;
    }

    public override LoopStmtNode Visit(LoopStmtNode node)
    {
        _symbolTable.OpenScope();
        Visit(node.AssignStmt);
        Symbol loopVar = new Symbol(node.AssignStmt.Identifier, node.AssignStmt.Value, ALFATypes.TypeEnum.@int, 25, 25 );
        if (_symbolTable.RetrieveSymbol(node.AssignStmt.Identifier) == null)
        {
            _symbolTable.EnterSymbol(loopVar);
        }
        Visit(node.To);

        if (node.To is ExprNode exprTo)
        {
            node.To = Visit(exprTo);
        }
        if (node.To is IdNode idNode) {
            Symbol symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);

            Visit((dynamic)symbol.Value);

            if (symbol.Value is BoolNode || (symbol.Value is ExprNode exprNode && exprNode.Value is BoolNode))
                throw new TypeException(
                    $"The 'to' value cannot be a boolean in loop on line {symbol.LineNumber} column{symbol.ColumnNumber}");
        }
        Visit(node.Block);
        _symbolTable.CloseScope();

        return node;
    }

    public override ParalStmtNode Visit(ParalStmtNode node)
    {
        _symbolTable.OpenScope();
        foreach (var statement in node.Block.Statements)
        {
            Visit(statement);
        }
        _symbolTable.CloseScope();

        return node;
    }

    public override ExprNode Visit(ExprNode node)
    {
        EvaluateExpression(node);

        return node;
    }

    public override Node Visit(BlockNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit((dynamic)stmt);
        }

        return node;
    }

    public override Node Visit(ParalBlockNode node)
    {
        foreach (var stmt in node.Statements)
        {
            Visit((dynamic)stmt);
        }

        return node;
    }


    public void EvaluateExpression(ExprNode node)
    {
        var leftValue = node.Left;
        var rightValue = node.Right;

        ExprNode? leftValueExpr = leftValue as ExprNode;
        ExprNode? rightValueExpr = rightValue as ExprNode;

        //Visit left and right value recursively when they are expressions
        if (leftValueExpr != null)
        {
            EvaluateExpression(leftValueExpr);
            leftValue = leftValueExpr.Value;

            if (node.Operator == "()") node.Value = leftValue;
        }
        if (rightValueExpr != null)
        {
            EvaluateExpression(rightValueExpr);
            rightValue = rightValueExpr.Value;
        }

        //If both are not an expresion then we must retrieve values from id.

        switch (node.Operator)
        {
            case "<":
            case ">":
            case "<=":
            case ">=":
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
            case "u-":
                EvaluateArithmeticExpression(leftValue, rightValue, node.Operator, node);
                break;
            case "==":
            case "!=":
                EvaluateEqualityExpression(leftValue, rightValue, node.Operator, node);
                break;
            case "and":
            case "or":
            case "!":
                EvaluateBooleanExpression(leftValue, rightValue, node.Operator, node);
                break;
            case "()":
                node.Value = node.Left;
                break;
            default:
                throw new Exception("You used an arithmetic operator that is not being switched on");
        }

    }

    public void EvaluateArithmeticExpression(Node left, Node right, string op, ExprNode parent)
    {
        Tuple<NumNode, NumNode> expectedNodes = EvaluateIdNode<NumNode>(left, right, op, right != null);

        switch (op)
        {
            case "<":
                parent.Value = new BoolNode(expectedNodes.Item1.Value < expectedNodes.Item1.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case ">":
                parent.Value = new BoolNode(expectedNodes.Item1.Value > expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "<=":
                parent.Value = new BoolNode(expectedNodes.Item1.Value <= expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case ">=":
                parent.Value = new BoolNode(expectedNodes.Item1.Value >= expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "+":
                parent.Value = new NumNode(expectedNodes.Item1.Value + expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "-":
                parent.Value = new NumNode(expectedNodes.Item1.Value - expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "*":
                parent.Value = new NumNode(expectedNodes.Item1.Value * expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "/":
                parent.Value = new NumNode(expectedNodes.Item1.Value / expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "%":
                parent.Value = new NumNode(expectedNodes.Item1.Value % expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "u-":
                parent.Value = new NumNode(-expectedNodes.Item1.Value, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
        }
        
    }

    public void EvaluateEqualityExpression(Node left, Node right, string op, ExprNode parent)
    {
        var expectedNodes = EvaluateEqualityType(left, right, op);
        dynamic leftDyn = new { }, rightDyn = new { };

        if (expectedNodes.Item1 is NumNode numNodeLeft && expectedNodes.Item2 is NumNode numNodeRight)
        {
            leftDyn = numNodeLeft;
            rightDyn = numNodeRight;
        }
        else if (expectedNodes.Item1  is BoolNode boolNodeLeft && expectedNodes.Item2 is BoolNode boolNodeRight)
        {
            leftDyn = boolNodeLeft;
            rightDyn = boolNodeRight;
        }
        
        switch (op)
        {
            case "==":
                parent.Value = new BoolNode(leftDyn == rightDyn, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
            case "!=":
                parent.Value = new BoolNode(leftDyn != rightDyn, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
        }
    }

    public void EvaluateBooleanExpression(Node left, Node right, string op, ExprNode parent)
    {
        Tuple<BoolNode, BoolNode> expectedNodes = EvaluateIdNode<BoolNode>(left, right, op, right != null);

        switch (op)
        {
            case "and":
                parent.Operator = "&&";
                parent.Value = new BoolNode(expectedNodes.Item1.Value && expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "or":
                parent.Operator = "||";
                parent.Value = new BoolNode(expectedNodes.Item1.Value || expectedNodes.Item2.Value, expectedNodes.Item2.Line, expectedNodes.Item2.Col);
                break;
            case "!":
                parent.Value = new BoolNode(!expectedNodes.Item1.Value, expectedNodes.Item1.Line, expectedNodes.Item1.Col);
                break;
            default:
                throw new Exception("You used a boolean operator that is not being switched on");
        }
    }

    public Tuple<Node, Node> EvaluateEqualityType(Node left, Node right, string op)
    {
        if (left is IdNode idNode) left = VisitSymbol<NumNode>(idNode);
        if (right is IdNode idNode1) left = VisitSymbol<NumNode>(idNode1);

        if (left.GetType() != right.GetType()) throw new ArgumentTypeException($"Incompatible type {left.GetType()} in '{op}' expression on line {left.Line} column {left.Col}");
        return new Tuple<Node, Node>(left, right);
    }

    public Tuple<T, T> EvaluateIdNode<T>(Node left, Node right, string op, bool isBinary) where T : Node
    {
        T? leftTNode = null, rightTNode = null;

        if (left is T leftT) leftTNode = leftT;
        if (right is T rightT) rightTNode = rightT;
        
        if (left is IdNode idNode) leftTNode = VisitSymbol<T>(idNode);
        if (right is IdNode idNode1) rightTNode = VisitSymbol<T>(idNode1);

        if (left is ExprNode exprNode)
        {
            EvaluateExpression(exprNode);
            leftTNode = (T)exprNode.Value;
        }

        if (right is ExprNode exprNode1)
        {
            EvaluateExpression(exprNode1);
            rightTNode = (T)exprNode1.Value;
        }
        
        if (leftTNode == null)
        {
            string wrongType = left.GetType().ToString() == "ALFA.AST_Nodes.BoolNode" ? "bool" : "int";
            throw new ArgumentTypeException($"Incompatible type {wrongType} in '{op}' expression on line {left.Line} column {left.Col}");
        }

        if (rightTNode == null && isBinary)
        {
            string wrongType = right.GetType().ToString() == "ALFA.AST_Nodes.BoolNode" ? "bool" : "int";
            throw new ArgumentTypeException($"Incompatible type {wrongType} in '{op}' expression on line {right.Line} column {right.Col}");
        }

        return new Tuple<T, T>(leftTNode, rightTNode);
    }

    //VisitSymbol is called from an arithmetic or boolean expression when it must be determined
    //whether the identifier's value is a boolean or an integer
    public T VisitSymbol<T>(IdNode idNode) where T : Node
    {
        var symbol = _symbolTable.RetrieveSymbol(idNode.Identifier);
        var nodeToCast = symbol.Value;

        if (nodeToCast is ExprNode exprNode && exprNode.Value != null)
        {
            nodeToCast = exprNode.Value;
        }
        else if (nodeToCast is ExprNode && idNode.LocalValue is ExprNode locValExpr)
        {
            EvaluateExpression(locValExpr);
            nodeToCast = locValExpr.Value;
        }
        else if (nodeToCast is ExprNode exprNodeWId && idNode.LocalValue is IdNode exprIdNode)
        {
            nodeToCast = VisitSymbol<T>(exprIdNode);
        }
        else if (idNode.LocalValue is not ExprNode && idNode.LocalValue != null)
        {
            nodeToCast = idNode.LocalValue!;
        }
        else if (nodeToCast.GetType().ToString() != typeof(T).ToString())
        {
            string type = "";
            switch (typeof(T).ToString())
            {
                case "ALFA.AST_Nodes.NumNode":
                    type = "int";
                    break;
                case "ALFA.AST_Nodes.BoolNode":
                    type = "bool";
                    break;
            }
            throw new ArgumentTypeException($"Expected type {type} on line {nodeToCast.Line} column {nodeToCast.Col}");
        }
        
        return (T)nodeToCast;
    }

    public override BoolNode Visit(BoolNode node) => node;
    public override IdNode Visit(IdNode node) => node;
    public override NumNode Visit(NumNode node) => node;
}
