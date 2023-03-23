namespace ALFA.AST_Nodes;

public class TypeNode : Node
{
    public string Type { get; set; } // 'int' or 'square'

    public TypeNode(string type, int line, int col) : base(line, col)
    {
        Type = type;
    }
}