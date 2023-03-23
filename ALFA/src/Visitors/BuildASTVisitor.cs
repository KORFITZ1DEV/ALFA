using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA;

public class BuildASTVisitor : ALFABaseVisitor<Node>
{
    private SymbolTable _symbolTable;

    public BuildASTVisitor(SymbolTable symbolTable)
    {
        _symbolTable = symbolTable;
    }
    
    public override ProgramNode VisitProgram(ALFAParser.ProgramContext context)
    {
        List<StatementNode> childList = new List<StatementNode>();
        
        foreach (var stmt in context.statement())
        {
            childList.Add((StatementNode)Visit(stmt));
        }
        
        return new ProgramNode(childList!);
    }
    
    public override StatementNode VisitStatement(ALFAParser.StatementContext context)
    {
        if (context.varDcl() != null)
        {
            return VisitVarDcl(context.varDcl());
        }
        
        return VisitFuncCall(context.funcCall());
    }
    
    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        var parent = (ALFAParser.StatementContext)context.Parent;
        string id = context.ID().GetText();
        if (context.funcCall() != null)
        {
            var funcCall = (FuncCallNode)Visit(context.funcCall());
            _symbolTable.EnterSymbol(new Symbol(id, 0, ALFATypes.TypeEnum.Square, context.Start.Line, context.Start.Column));
            return new VarDclNode(parent.type().GetText(), id, funcCall);
        }

        NumNode num = new NumNode(int.Parse(context.NUM().GetText()), context.Start.Line, context.Start.Column);
        _symbolTable.EnterSymbol(new Symbol(id, num.Value, ALFATypes.TypeEnum.Int, context.Start.Line, context.Start.Column));
        return new VarDclNode(parent.type().GetText(),id, num);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        var type = context.builtIns().GetText();
        ALFATypes.BuiltInTypeEnum builtInTypeEnum;
        List<ALFATypes.TypeEnum> formalParams = new List<ALFATypes.TypeEnum>();
        
        switch (type)
        {
            case "createSquare":
                ALFATypes.TypeEnum[] formalCSParamsArray = {ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalCSParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.CreateSquare;
                break;
            case "move":
                ALFATypes.TypeEnum[] formalMovParamsArray = {ALFATypes.TypeEnum.Square, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalMovParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.Move;
                break;
            case "wait":
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.Wait;
                ALFATypes.TypeEnum[] formalWaitParamsArray = {ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalWaitParamsArray);
                break;
            default:
                throw new Exception("Invalid built-in function");
        }
        
        FuncCallNode funcCallNode = new FuncCallNode(
            new BuiltInsNode(builtInTypeEnum, formalParams, context.Start.Line, context.Start.Column),
            new List<Node>()
        );

        if (context.args() != null)
        {
            foreach (var argCtx in context.args().arg())
            {
                ArgNode argNode = (ArgNode)Visit(argCtx);
                funcCallNode.Arguments.Add(argNode);
            }
        }

        return funcCallNode;
    }
    
    public override BuiltInsNode VisitBuiltIns(ALFAParser.BuiltInsContext context)
    {
        var type = context.GetText();
        ALFATypes.BuiltInTypeEnum builtInTypeEnum;
        List<ALFATypes.TypeEnum> formalParams = new List<ALFATypes.TypeEnum>();
        
        switch (type)
        {
            case "createSquare":
                ALFATypes.TypeEnum[] formalCSParamsArray = {ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalCSParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.CreateSquare;
                break;
            case "move":
                ALFATypes.TypeEnum[] formalMovParamsArray = {ALFATypes.TypeEnum.Square, ALFATypes.TypeEnum.Int, ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalMovParamsArray);
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.Move;
                break;
            case "wait":
                builtInTypeEnum = ALFATypes.BuiltInTypeEnum.Wait;
                ALFATypes.TypeEnum[] formalWaitParamsArray = {ALFATypes.TypeEnum.Int};
                formalParams.AddRange(formalWaitParamsArray);
                break;
            default:
                throw new Exception("Invalid built-in function");
        }
        
        return new BuiltInsNode(builtInTypeEnum, formalParams,  context.Start.Line, context.Start.Column);
    }
    
    public override ArgNode VisitArg(ALFAParser.ArgContext context)
    {
        var id = context.ID();
        var num = context.NUM();

        if (id != null)
        {
            Symbol? sym = _symbolTable.RetrieveSymbol(id.GetText());
            if (sym == null) throw new Exception($"Variable {id.GetText()} not declared at line {id.Symbol.Line}:{id.Symbol.Column}");
            IdNode idNode = new IdNode(id.GetText(), context.Start.Line, context.Start.Column);
            return new ArgNode(idNode, context.Start.Line, context.Start.Column);
        }

        NumNode numNode = new NumNode(int.Parse(num.GetText()), context.Start.Line, context.Start.Column);
        return new ArgNode(numNode, context.Start.Line, context.Start.Column);
    }
}