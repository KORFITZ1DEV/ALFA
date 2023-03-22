namespace ALFA.AST_Nodes;

public class NumNode : Node
{
    public int Value { get; set; }

    public NumNode(int value)
    {
        Value = value;
    }
}