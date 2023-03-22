namespace ALFA.AST_Nodes;

public class IdNode : Node
{
    public string Identifier { get; set; }

    public IdNode(string identifier)
    {
        Identifier = identifier;
    }
}
