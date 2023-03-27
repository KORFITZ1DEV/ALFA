namespace ALFA.AST_Nodes;

public class FuncCallNode : StatementNode
{
    public string? Identifier { get; set; }
    public BuiltInsNode BuiltIns { get; set; } // 'createSquare', 'move', or 'wait'
    public List<ArgNode> Arguments { get; set; } // a list of ArgNode objects

    public FuncCallNode(BuiltInsNode builtIns, List<ArgNode> arguments, int line, int col) : base(line, col)
    {
        BuiltIns = builtIns;
        Arguments = arguments;
    }
}