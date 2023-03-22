namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public string Name { get; set; } // 'createSquare', 'move', or 'wait'

    public BuiltInsNode(string name)
    {
        Name = name;
    }
}