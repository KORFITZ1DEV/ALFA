namespace ALFA.AST_Nodes;

public class BoolNode : Node
{
    public bool Value { get; set; }

    public BoolNode(bool value, int line, int col) : base(line, col)
    {
        Value = value;
    }

    public BoolNode(bool value)
    {
        Value = value;
    }
}