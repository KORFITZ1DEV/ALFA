using ALFA.AST_Nodes;

namespace ALFA;

public abstract class ASTVisitor<T>
{
    public abstract T Visit(ArgNode node);
    public abstract T Visit(BuiltInsNode node);
    public abstract T Visit(FuncCallNode node);
    public abstract T Visit(ProgramNode node);
    public abstract T Visit(StmtNode node);
    public abstract T Visit(VarDclNode node);
    
    public T Visit(Node node)
    {
        return node switch
        {
            ArgNode argNode => Visit(argNode),
            BuiltInsNode builtInsNode => Visit(builtInsNode),
            FuncCallNode funcCallNode => Visit(funcCallNode),
            ProgramNode programNode => Visit(programNode),
            StmtNode stmtNode => Visit(stmtNode),
            VarDclNode varDclNode => Visit(varDclNode),
            _ => throw new Exception("Unknown node type")
        };
    }
}