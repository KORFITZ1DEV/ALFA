namespace ALFA.AST_Nodes;

public class ProgramNode : Node
{
    public List<Node> Statements { get; set; }

    public ProgramNode(List<Node> statements)
    {
        Statements = statements;
    }
}