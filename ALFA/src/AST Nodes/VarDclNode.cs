namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public FuncCallNode? FuncCall;
    public string Id;
    public int? Num;
    
    public VarDclNode(FuncCallNode funcCall, string id )
    {
        this.FuncCall = funcCall;
        this.Id = id;
    }
    public VarDclNode(int num, string id)
    {
        this.Num = num;
        this.Id = id;
    }
}