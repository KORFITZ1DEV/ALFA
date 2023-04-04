namespace ALFA.AST_Nodes;

public class FuncCallNode : Node
{
    public BuiltInsNode BuiltIns { get; set; } // 'createRect', 'move', or 'wait'
    public List<Node> Arguments { get; set; } // either a NumNode or an IdNode
    
    public FuncCallNode(BuiltInsNode builtIns, List<Node> arguments, int line, int col) : base(line, col)
    {
        BuiltIns = builtIns;
        Arguments = arguments;
    }
}