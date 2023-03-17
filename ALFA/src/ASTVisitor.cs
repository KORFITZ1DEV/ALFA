using ALFA.AST_Nodes;

namespace ALFA;

public class ASTVisitor : ALFABaseVisitor<Node>
{
    public override ProgramNode VisitProgram(ALFAParser.ProgramContext context)
    {
      var childList = context.children.Select(child => Visit(child) as StmtNode).ToList();
      
        return new ProgramNode(childList!);
    }
    
    public override StmtNode VisitStatement(ALFAParser.StatementContext context)
    {
        var varDcl = Visit(context.varDcl()) as VarDclNode;
        
        return new StmtNode(varDcl!);
    }
    
    public override VarDclNode VisitVarDcl(ALFAParser.VarDclContext context)
    {
        var funcCall = Visit(context.funcCall()) as FuncCallNode;
        var id = context.ID().GetText();
        
        return new VarDclNode(funcCall!, id);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        var builtIns = Visit(context.builtIns()) as BuiltInsNode;
        var args = context.args().children.Select(child => Visit(child) as ArgNode).ToList();

        return new FuncCallNode(builtIns!, args!);
    }
    
    public override BuiltInsNode VisitBuiltIns(ALFAParser.BuiltInsContext context)
    {
        var type = context.GetText();
        BuiltInsNode.TypeEnum typeEnum;
        
        switch (type)
        {
            case "createSquare":
                typeEnum = BuiltInsNode.TypeEnum.create;
                break;
            case "move":
                typeEnum = BuiltInsNode.TypeEnum.move;
                break;
            case "wait":
                typeEnum = BuiltInsNode.TypeEnum.wait;
                break;
            default:
                throw new Exception("Invalid built-in function");
        }
        
        return new BuiltInsNode(typeEnum);
    }
    
    public override ArgNode VisitArg(ALFAParser.ArgContext context)
    {
        var id = context.ID().GetText();
        var num = context.INT().GetText();

        if (id != null)
        {
        return new ArgNode(id);
        }
        return new ArgNode(int.Parse(num));
    }
    
}