namespace ALFA.AST_Nodes;

public class ParalBlockNode : Node
{
    public List<BuiltInParalAnimCallNode> Statements { get; set; } = new List<BuiltInParalAnimCallNode>();
    
    public ParalBlockNode()
    {
    }
    public ParalBlockNode(List<BuiltInParalAnimCallNode> statements)
    {
        Statements = statements;
    }
}