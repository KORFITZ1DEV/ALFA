namespace ALFA.AST_Nodes;

public class ProgramNode : Node
{
    public List<StatementNode> Statements { get; set; }

    public ProgramNode(List<StatementNode> statements)
    {
        Statements = statements;
    }
}