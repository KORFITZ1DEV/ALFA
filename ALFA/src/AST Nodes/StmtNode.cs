namespace ALFA.AST_Nodes;

public class StmtNode : Node
{
    public VarDclNode? VarDcl;
    public FuncCallNode? FuncCall;
    public enum TypeEnum
    {
        Int,
        Square
    }

    public TypeEnum? Type;
    public StmtNode(VarDclNode varDcl, TypeEnum type, int line, int col) : base(line, col)
    {
        this.VarDcl = varDcl;
        this.Type = type;
    }
    
    public StmtNode(FuncCallNode funcCall, int line, int col) : base(line, col)
    {
        this.FuncCall = funcCall;
    }
}