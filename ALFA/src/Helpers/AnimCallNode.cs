using System.Diagnostics.CodeAnalysis;

namespace ALFA.AST_Nodes;
using ALFA.Types;

[ExcludeFromCodeCoverage]
public abstract class AnimCallNode<T> : Node
{
    public List<Node> Arguments { get; set; }
    public T Type { get; set; }

    public AnimCallNode(int line, int col, List<Node> arguments) : base(line, col)
    {
        Arguments = arguments;
    }
}
