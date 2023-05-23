namespace ALFA.AST_Nodes;

public class NumNode : Node
{
    public int Value { get; set; }
    public int LocalValue { get; set; }

    public NumNode(int value, int line, int col) : base(line, col)
    {
        Value = value;
    }

    public NumNode(int value)
    {
        Value = value;
    }
}