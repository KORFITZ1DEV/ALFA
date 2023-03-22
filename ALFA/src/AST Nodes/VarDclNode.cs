namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public FuncCallNode? FuncCall;
    public string Id;
    public int? Num;

    public VarDclNode(FuncCallNode funcCall, string id, int line, int col) : base(line, col)
    {
        this.FuncCall = funcCall;
        this.Id = id;
    }
    public VarDclNode(int num, string id, int line, int col) : base(line, col)
    {
        this.Num = num;
        this.Id = id;
    }
}