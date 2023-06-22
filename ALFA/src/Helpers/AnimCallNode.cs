namespace ALFA.AST_Nodes;
using ALFA.Types;

public abstract class AnimCallNode<T> : Node
{
    public List<Node> Arguments { get; set; }
    public abstract T Type { get; set; } 
    public AnimCallNode(int line, int col, List<Node> arguments) : base(line, col)
    {
        Arguments = arguments;
    }
}
