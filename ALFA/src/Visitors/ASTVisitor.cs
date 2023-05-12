using ALFA.AST_Nodes;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public abstract class ASTVisitor<T>
{

    public abstract T Visit(BuiltInAnimCallNode node);
    public abstract T Visit(ProgramNode node);
    public abstract T Visit(VarDclNode node);
    public abstract T Visit(IdNode node);
    public abstract T Visit(NumNode node);
    public abstract T Visit(BuiltInCreateShapeCallNode callNode);
    

    public T Visit(Node node)
    {
        return node switch
        {
            BuiltInAnimCallNode builtInAnimNode => Visit(builtInAnimNode),
            BuiltInCreateShapeCallNode builtInCreateShapeNode => Visit(builtInCreateShapeNode),
            ProgramNode programNode => Visit(programNode),
            VarDclNode varDclNode => Visit(varDclNode),
            IdNode idNode => Visit(idNode),
            NumNode numNode => Visit(numNode)
        };
    }
}