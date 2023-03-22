using ALFA.AST_Nodes;

namespace ALFA;

public class BuildASTVisitor : ALFABaseVisitor<Node>
{
    private SymbolTable _symbolTable = new();
    
    public override ProgramNode VisitProgram(ALFAParser.ProgramContext context)
    {
        List<StmtNode> childList = new List<StmtNode>();
        
        foreach (var stmt in context.statement())
        {
            childList.Add(Visit(stmt) as StmtNode);
        }
        
        return new ProgramNode(childList!);
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
            
            
            
            return new StmtNode(visitedVarDclNode!, type);
        }
        return new StmtNode(Visit(context.funcCall()) as FuncCallNode);
    }
    
    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        string id = context.ID().GetText();
        if (context.funcCall() != null)
        {
        var funcCall = Visit(context.funcCall()) as FuncCallNode;
            return new VarDclNode(funcCall!, id);
        }

        int value = int.Parse(context.NUM().GetText());
        _symbolTable.EnterSymbol(new Symbol(id, value, context.Start.Line, context.Start.Column));
        return new VarDclNode(value, id);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        var builtIns = Visit(context.builtIns()) as BuiltInsNode;
        var args = context.args().arg().Select(child => Visit(child) as ArgNode).ToList();

        return new FuncCallNode(builtIns!, args!);
    }
    
    public override BuiltInsNode VisitBuiltIns(ALFAParser.BuiltInsContext context)
    {
        var type = context.GetText();
        BuiltInsNode.BuiltInTypeEnum builtInTypeEnum;
        
        switch (type)
        {
            case "createSquare":
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.Create;
                break;
            case "move":
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.Move;
                break;
            case "wait":
                builtInTypeEnum = BuiltInsNode.BuiltInTypeEnum.Wait;
                break;
            default:
                throw new Exception("Invalid built-in function");
        }
        
        return new BuiltInsNode(builtInTypeEnum);
    }
    
    public override ArgNode VisitArg(ALFAParser.ArgContext context)
    {
        var id = context.ID();
        var num = context.NUM();

        if (id != null)
        {
            Symbol? sym = _symbolTable.RetrieveSymbol(id.GetText());
            if (sym == null) throw new Exception($"Variable {id.GetText()} not declared at line {id.Symbol.Line}:{id.Symbol.Column}");
            return new ArgNode(id.GetText());
        }
        
        return new ArgNode(int.Parse(num.GetText()));
    }
}