namespace ALFA.AST_Nodes;

public class StmtNode : Node
{
    public VarDclNode VarDcl;
    
    public StmtNode(VarDclNode varDcl)
    {
        this.VarDcl = varDcl;
    }
}