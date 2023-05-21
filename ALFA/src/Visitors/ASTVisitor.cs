using ALFA.AST_Nodes;

namespace ALFA.Visitors;
// https://stackoverflow.com/questions/29971097/how-to-create-ast-with-antlr4

public abstract class ASTVisitor<T>
{

    public abstract T Visit(ProgramNode node);
    public abstract T Visit(VarDclNode node);
    public abstract T Visit(AssignStmtNode node);
    public abstract T Visit(BuiltInCreateShapeCallNode callNode);
    public abstract T Visit(BuiltInAnimCallNode node);
    public abstract T Visit(IfStmtNode node);
    public abstract T Visit(LoopStmtNode node);
    public abstract T Visit(ParalStmtNode node);
    public abstract T Visit(ExprNode node);
    public abstract T Visit(BoolNode node);
    public abstract T Visit(IdNode node);
    public abstract T Visit(NumNode node);


    public T Visit(Node node)
    {
        return node switch
        {
            ProgramNode programNode => Visit(programNode),
            VarDclNode varDclNode => Visit(varDclNode),
            AssignStmtNode assignStmtNode => Visit(assignStmtNode),
            BuiltInCreateShapeCallNode builtInCreateShapeNode => Visit(builtInCreateShapeNode),
            BuiltInAnimCallNode builtInAnimNode => Visit(builtInAnimNode),
            IfStmtNode ifStmtNode => Visit(ifStmtNode),
            LoopStmtNode loopStmtNode => Visit(loopStmtNode),
            ParalStmtNode paralStmtNode => Visit(paralStmtNode),
            ExprNode exprNode => Visit(exprNode),
            BoolNode boolNode => Visit(boolNode),
            IdNode idNode => Visit(idNode),
            NumNode numNode => Visit(numNode)
        };
    }
}