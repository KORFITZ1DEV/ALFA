namespace ALFA.AST_Nodes;

public class FuncCallNode : StatementNode
{
    public BuiltInsNode FunctionName { get; set; } // 'createSquare', 'move', or 'wait'
    public List<Node> Arguments { get; set; } // a list of ArgNode objects

    public FuncCallNode(BuiltInsNode functionName, List<Node> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
}