namespace ALFA.AST_Nodes;

public class FuncCallNode : StatementNode
{
    public string FunctionName { get; set; } // 'createSquare', 'move', or 'wait'
    public List<Node> Arguments { get; set; } // a list of ArgNode objects

    public FuncCallNode(string functionName, List<Node> arguments)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
}