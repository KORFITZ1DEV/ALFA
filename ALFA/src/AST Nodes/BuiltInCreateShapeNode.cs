using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInCreateShapeNode : Node
{
    public ALFATypes.CreateShapeEnum Type { get; set; } //createRect
    public List<Node> Arguments { get; set; } // can consist of NumNode or IdNode


    public BuiltInCreateShapeNode(ALFATypes.CreateShapeEnum type, List<Node> arguments)
    {
        Type = type;
        Arguments = arguments;
    }
}