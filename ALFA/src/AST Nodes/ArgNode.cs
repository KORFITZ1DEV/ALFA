namespace ALFA.AST_Nodes;

public class ArgNode : Node
{
    public string? Id;
    public int? Num;

    public ArgNode(int num, int line, int col) : base(line, col)
    {
        this.Num = num;
    }
    public ArgNode(string id, int line, int col) : base(line, col)
    {
        this.Id = id;
    }
    
}
    
    