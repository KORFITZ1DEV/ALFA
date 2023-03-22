using ALFA.AST_Nodes;

namespace ALFA;

public class BuildASTVisitor : ALFABaseVisitor<Node>
{
    private SymbolTable _symbolTable = new();
    
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
            return new VarDclNode(parent.type().GetText(), id, funcCall);
        }

        NumNode num = new NumNode(int.Parse(context.NUM().GetText()));
        _symbolTable.EnterSymbol(new Symbol(id, num.Value, context.Start.Line, context.Start.Column));
        return new VarDclNode(parent.type().GetText(),id, num);
    }
    
    public override FuncCallNode VisitFuncCall(ALFAParser.FuncCallContext context)
    {
        FuncCallNode funcCallNode = new FuncCallNode(
            context.builtIns().GetText(),
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
        return new BuiltInsNode(context.GetText());
    }
    
    public override ArgNode VisitArg(ALFAParser.ArgContext context)
    {
        var id = context.ID();
        var num = context.NUM();

        if (id != null)
        {
            Symbol? sym = _symbolTable.RetrieveSymbol(id.GetText());
            if (sym == null) throw new Exception($"Variable {id.GetText()} not declared at line {id.Symbol.Line}:{id.Symbol.Column}");
            IdNode idNode = new IdNode(id.GetText());
            return new ArgNode(idNode);
        }

        NumNode numNode = new NumNode(int.Parse(num.GetText()));
        return new ArgNode(numNode);
    }
}