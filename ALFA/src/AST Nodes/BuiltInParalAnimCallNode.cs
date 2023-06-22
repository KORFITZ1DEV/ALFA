using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInParalAnimCallNode : AnimCallNode<ALFATypes.BuiltInParalAnimEnum>
{
    public override ALFATypes.BuiltInParalAnimEnum Type { get; set; }

    public BuiltInParalAnimCallNode(ALFATypes.BuiltInParalAnimEnum type, List<Node> arguments, int line, int col) : base(line, col, arguments)
    {
        Type = type;
    }
}