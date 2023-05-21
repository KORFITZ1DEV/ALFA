using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInParalAnimCallNode : Node
{
    public ALFATypes.BuiltInParalAnimEnum Type { get; set; }
    public List<Node> Arguments { get; set; }

    public BuiltInParalAnimCallNode(ALFATypes.BuiltInParalAnimEnum type, List<Node> arguments, int line, int col) : base(line, col)
    {
        Type = type;
        Arguments = arguments;
    }
}