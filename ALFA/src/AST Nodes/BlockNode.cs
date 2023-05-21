namespace ALFA.AST_Nodes;

public class BlockNode : Node
{
    public List<Node> Statements { get; set; } = new List<Node>();

    public BlockNode()
    {
    }
    public BlockNode(List<Node> statements)
    {
        Statements = statements;
    }
}