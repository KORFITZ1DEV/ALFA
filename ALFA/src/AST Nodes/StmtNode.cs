namespace ALFA.AST_Nodes;

public class StmtNode : Node
{
    public VarDclNode VarDcl;
    public FuncCallNode FuncCall;
    
    public StmtNode(VarDclNode varDcl)
    {
        this.VarDcl = varDcl;
    }
    
    public StmtNode(FuncCallNode funcCall)
    {
        this.FuncCall = funcCall;
    }
}