namespace ALFA.Types;

public class BuiltIn
{
    public List<ALFATypes.TypeEnum> FormalParams;
    public ALFATypes.BuiltInTypeEnum Type;

    public BuiltIn(List<ALFATypes.TypeEnum>formalParams, ALFATypes.BuiltInTypeEnum type)
    {
        FormalParams = formalParams;
        Type = type;
    }
}
