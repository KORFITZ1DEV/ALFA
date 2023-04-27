using System.Diagnostics;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public class BuildASTVisitor : ALFABaseVisitor<Node>
{
    private SymbolTable _symbolTable;

    public BuildASTVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }
    
    public override ProgramNode VisitProgram(ALFAParser.ProgramContext context)
    {
        List<Node> childList = new List<Node>();
        
        //Debug.Assert(context.statement() != null);
        foreach (var stmt in context.statement())
        {
            childList.Add(Visit(stmt));
        }
        
        return new ProgramNode(childList!);
    }
    
    public override Node VisitStatement(ALFAParser.StatementContext context)
    {
        if (context.varDcl() != null)
            return VisitVarDcl(context.varDcl());
        
        return VisitFuncCall(context.funcCall());
    }
    
    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        var parent = (ALFAParser.StatementContext)context.Parent;
        string id = context.ID().GetText();
      //program should thorw an exception if one of the children is a ErrorNodeImpl.
      ALFATypes.TypeEnum typeEnum;
        
        switch (parent.type().GetText())
        {
            case "int":
                typeEnum = ALFATypes.TypeEnum.@int;
                break;
            case "rect":
                typeEnum = ALFATypes.TypeEnum.rect;
                break;
            default:
                throw new TypeException("Invalid type on line " + context.Start.Line + ":" + context.Start.Column);
        }
        
        if (context.funcCall() != null)
        {
            var funcCall = (FuncCallNode)Visit(context.funcCall());
            _symbolTable.EnterSymbol(new Symbol(id, funcCall, typeEnum, context.Start.Line, context.Start.Column));
            return new VarDclNode(typeEnum, id, funcCall, context.Start.Line, 0);
        }
        
        // Can we use this???
        // Debug.Assert(context.NUM() == null && context.funcCall() == null);
                
        if (context.NUM() == null)
        {
            throw new TypeException("expected int on line " + context.Start.Line + ":" + context.Start.Column);
        }
        NumNode num = new NumNode(int.Parse(context.NUM().GetText()), context.Start.Line, context.Start.Column);
        _symbolTable.EnterSymbol(new Symbol(id, num, typeEnum, context.Start.Line, context.Start.Column));
        return new VarDclNode(typeEnum,id, num, context.Start.Line, 0);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        string? identifier = null;
        var type = context.builtIns().GetText();
        ALFATypes.BuiltInTypeEnum builtInTypeEnum;
        List<ALFATypes.TypeEnum> formalParams = new List<ALFATypes.TypeEnum>();
        
        switch (type)
        {
            case "createRect":
                ALFATypes.TypeEnum[] formalCSParamsArray = {ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int};
                formalParams.AddRange(formalCSParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.createRect;
                
                var parent = (ALFAParser.VarDclContext)context.Parent;
                identifier = parent.ID().GetText();
                break;
            case "move":
                ALFATypes.TypeEnum[] formalMoveParamsArray = {ALFATypes.TypeEnum.rect, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int};
                formalParams.AddRange(formalMoveParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.move;
                if (context.args().arg()[0].ID() == null) throw new ArgumentTypeException("You are trying to move something that isnt a rect");
                identifier = context.args().arg()[0].ID().GetText();
                break;
            case "wait":
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.wait;
                ALFATypes.TypeEnum[] formalWaitParamsArray = {ALFATypes.TypeEnum.@int};
                formalParams.AddRange(formalWaitParamsArray);
                break;
            default:
                throw new UnknownBuiltinException("Invalid built-in function");
        }
        
        FuncCallNode funcCallNode = new FuncCallNode(
            new BuiltInsNode(builtInTypeEnum, formalParams, context.Start.Line, context.Start.Column),
            new List<Node>(),
            context.Start.Line,
            context.Start.Column
        );

        if (context.args() != null)
        {
            foreach (var argCtx in context.args().arg())
            {
                var id = argCtx.ID();
                var num = argCtx.NUM();

                if (id != null)
                {
                    Symbol? sym = _symbolTable.RetrieveSymbol(id.GetText());
                    if (sym == null) 
                        throw new UndeclaredVariableException($"Variable {id.GetText()} not declared at line {id.Symbol.Line}:{id.Symbol.Column}");
            
                    IdNode idNode = new IdNode(id.GetText(), context.Start.Line, context.Start.Column);
                    funcCallNode.Arguments.Add(idNode);
                    continue;
                }
                
                if (argCtx.NUM() == null)
                    throw new TypeException("expected int on line " + context.Start.Line + ":" + context.Start.Column);
                
                NumNode numNode = new NumNode(int.Parse(num.GetText()), context.Start.Line, context.Start.Column);
                funcCallNode.Arguments.Add((numNode));
            }
        }

        return funcCallNode;
    }
    
    public override BuiltInsNode VisitBuiltIns(ALFAParser.BuiltInsContext context)
    {
        var type = context.GetText();
        BuiltIn builtIn = FormalParameters.FormalParams[type];
        return new BuiltInsNode(builtIn.Type, builtIn.FormalParams,  context.Start.Line, context.Start.Column);
    }
}