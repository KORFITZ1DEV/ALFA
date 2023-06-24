using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInCreateShapeCallNode : AnimCallNode<ALFATypes.CreateShapeEnum>
{
    public override ALFATypes.CreateShapeEnum Type { get; set; } //createRect


    public BuiltInCreateShapeCallNode(ALFATypes.CreateShapeEnum type, List<Node> arguments, int line, int col) : base(line, col, arguments)
    {
        Type = type;
        Arguments = arguments;
    }
}