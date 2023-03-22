namespace ALFA.AST_Nodes;

public class VarDclNode : StatementNode
{
    public string Type { get; set; }
    
    public string Identifier { get; set; }
    public Node Value { get; set; } // either a FuncCallNode or a NumNode

    public VarDclNode(string type, string identifier, Node value)
    {
        Type = type;
        Identifier = identifier;
        Value = value;
    }
}