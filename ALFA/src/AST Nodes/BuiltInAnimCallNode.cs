using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInAnimCallNode : Node
{
    public ALFATypes.BuiltInAnimEnum Type { get; set; }
    public List<Node> Arguments { get; set; }

    public BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum type, List<Node> arguments, int line, int col) : base(line, col)
    {
        Type = type;
        Arguments = arguments;
    }
}