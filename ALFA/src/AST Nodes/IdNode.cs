namespace ALFA.AST_Nodes;

public class IdNode : Node
{
    public string Identifier { get; set; }
    public Node? LocalValue { get; set; }

    public IdNode(string identifier, int line, int col) : base(line, col)
    {
        Identifier = identifier;
    }
}
