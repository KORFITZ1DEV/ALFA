using ALFA.AST_Nodes;

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
        List<StmtNode> childList = new List<StmtNode>();
        
        foreach (var stmt in context.statement())
        {
            childList.Add(Visit(stmt) as StmtNode);
        }
        
        return new ProgramNode(childList!, context.Start.Line, context.Start.Column);
    }
    
    public override StmtNode VisitStatement(ALFAParser.StatementContext context)
    {
        var varDcl = context.varDcl();
        
        
        if (varDcl != null)
        {
            var stmtType = context.type().GetText();
            StmtNode.TypeEnum type;
        
            switch (stmtType)
            {
                case "int":
                    type = StmtNode.TypeEnum.Int;
                    break;
                case "square":
                    type = StmtNode.TypeEnum.Square;
                    break;
                default:
                    throw new Exception($"Invalid type {stmtType} on line {context.Start.Line}:{context.Start.Column}");
            }
            
            var visitedVarDclNode = Visit(varDcl) as VarDclNode;
            
            
            
            return new StmtNode(visitedVarDclNode!, type, context.Start.Line, context.Start.Column);
        }
        return new StmtNode(Visit(context.funcCall()) as FuncCallNode, context.Start.Line, context.Start.Column);
    }
    
    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        string id = context.ID().GetText();
        if (context.funcCall() != null)
        {
            var funcCall = Visit(context.funcCall()) as FuncCallNode;
            _symbolTable.EnterSymbol(new Symbol(id, 0, StmtNode.TypeEnum.Square, context.Start.Line, context.Start.Column));
            return new VarDclNode(funcCall!, id, context.Start.Line, context.Start.Column);
        }

        int value = int.Parse(context.NUM().GetText());
        _symbolTable.EnterSymbol(new Symbol(id, value, StmtNode.TypeEnum.Int, context.Start.Line, context.Start.Column));
        return new VarDclNode(value, id, context.Start.Line, context.Start.Column);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        var builtIns = Visit(context.builtIns()) as BuiltInsNode;
        var args = context.args().arg().Select(child => Visit(child) as ArgNode).ToList();

        return new FuncCallNode(builtIns!, args!, context.Start.Line, context.Start.Column);
    }
    
    public override BuiltInsNode VisitBuiltIns(ALFAParser.BuiltInsContext context)
    {
        var type = context.GetText();
        BuiltInsNode.BuiltInTypeEnum builtInTypeEnum;
        List<StmtNode.TypeEnum> formalParams = new List<StmtNode.TypeEnum>();
        
        switch (type)
        {
            case "createSquare":
                StmtNode.TypeEnum[] formalCSParamsArray = {StmtNode.TypeEnum.Int, StmtNode.TypeEnum.Int, StmtNode.TypeEnum.Int, StmtNode.TypeEnum.Int};
                formalParams.AddRange(formalCSParamsArray);
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.CreateSquare;
                break;
            case "move":
                StmtNode.TypeEnum[] formalMovParamsArray = {StmtNode.TypeEnum.Square, StmtNode.TypeEnum.Int, StmtNode.TypeEnum.Int};
                formalParams.AddRange(formalMovParamsArray);
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.Move;
                break;
            case "wait":
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.Wait;
                StmtNode.TypeEnum[] formalWaitParamsArray = {StmtNode.TypeEnum.Int};
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
            return new ArgNode(id.GetText(), context.Start.Line, context.Start.Column);
        }
        
        return new ArgNode(int.Parse(num.GetText()), context.Start.Line, context.Start.Column);
    }
}