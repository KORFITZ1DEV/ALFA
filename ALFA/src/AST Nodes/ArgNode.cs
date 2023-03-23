namespace ALFA.AST_Nodes;

public class ArgNode : Node
{
    public Node Value { get; set; } // either a NumNode or an IdNode

    public ArgNode(Node value, int line, int col) : base(line, col)
    {
        Value = value;
    }
}