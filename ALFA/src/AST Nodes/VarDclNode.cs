namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public FuncCallNode FuncCall;
    public string Id;
    public VarDclNode(FuncCallNode funcCall, string id)
    {
        this.FuncCall = funcCall;
        this.Id = id;
    }
    public override string ToString()
    {
        return "\t\tVarDclNode";
    }
}