namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public FuncCallNode FuncCall;
    public string ID;
    public VarDclNode(FuncCallNode funcCall, string id)
    {
        this.FuncCall = funcCall;
        this.ID = id;
    }
}