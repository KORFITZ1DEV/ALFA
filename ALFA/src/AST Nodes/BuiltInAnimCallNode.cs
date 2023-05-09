using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInAnimCallNode : Node
{
    public ALFATypes.BuiltInAnimEnum BuiltInAnimType;
    public List<Node> Arguments { get; set; }

    //Todo remove
    public BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum builtInAnimType, int line, int col) : base(line, col)
    {
        
    }

    public BuiltInAnimCallNode(ALFATypes.BuiltInAnimEnum builtInAnimType, List<Node> arguments, int line, int col) : base(line, col)
    {
        BuiltInAnimType = builtInAnimType;
        Arguments = arguments;
    }
}