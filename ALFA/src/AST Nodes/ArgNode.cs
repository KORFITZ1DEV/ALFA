namespace ALFA.AST_Nodes;

public class ArgNode : Node
{
    public string Id;
    public int Num;

    public ArgNode(int num)
    {
        this.Num = num;
    }
    public ArgNode(string id)
    {
        this.Id = id;
    }
}
    
    