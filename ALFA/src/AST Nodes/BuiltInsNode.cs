namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public enum BuiltInTypeEnum
    {
        CreateSquare,
        Move,
        Wait
    }

    public BuiltInTypeEnum BuiltInType;

    public BuiltInsNode(BuiltInTypeEnum builtInType, int line, int col) : base(line, col)
    {
        this.BuiltInType = builtInType;
    }
}