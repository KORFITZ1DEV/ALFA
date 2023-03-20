namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public enum BuiltInTypeEnum
    {
        Create,
        Move,
        Wait
    }

    public BuiltInTypeEnum BuiltInType;

    public BuiltInsNode(BuiltInTypeEnum builtInType)
    {
        this.BuiltInType = builtInType;
    }
}