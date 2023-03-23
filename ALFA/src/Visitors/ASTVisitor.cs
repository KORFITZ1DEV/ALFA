using ALFA.AST_Nodes;

namespace ALFA;

public abstract class ASTVisitor<T>
{
    public abstract T Visit(ArgNode node);
    public abstract T Visit(BuiltInsNode node);
    public abstract T Visit(FuncCallNode node);
    public abstract T Visit(ProgramNode node);
    public abstract T Visit(StatementNode node);
    public abstract T Visit(VarDclNode node);
    public abstract T Visit(IdNode node);
    public abstract T Visit(NumNode node);
    public abstract T Visit(TypeNode node);

    public T Visit(Node node)
    {
        return node switch
        {
            ArgNode argNode => Visit(argNode),
            BuiltInsNode builtInsNode => Visit(builtInsNode),
            FuncCallNode funcCallNode => Visit(funcCallNode),
            ProgramNode programNode => Visit(programNode),
            VarDclNode varDclNode => Visit(varDclNode),
            IdNode idNode => Visit(idNode),
            NumNode numNode => Visit(numNode),
            TypeNode typeNode => Visit(typeNode),
            _ => throw new Exception("Unknown node type")
        };
    }
}