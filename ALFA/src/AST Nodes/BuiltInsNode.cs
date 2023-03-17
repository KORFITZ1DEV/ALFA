namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public enum TypeEnum
    {
        create,
        move,
        wait
    }

    public TypeEnum Type;

    public BuiltInsNode(TypeEnum type)
    {
        this.Type = type;
    }
}