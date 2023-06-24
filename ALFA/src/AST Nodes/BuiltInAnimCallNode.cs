using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInAnimCallNode : AnimCallNode<ALFATypes.BuiltInAnimEnum>
{
    public BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum type, List<Node> arguments, int line, int col) : base(line, col, arguments)
    {
        Type = type;
    }
}