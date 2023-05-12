namespace ALFA.Types;

public class BuiltIn
{
    public List<ALFATypes.TypeEnum> FormalParams;
    public ALFATypes.BuiltInAnimEnum Anim;

    public BuiltIn(List<ALFATypes.TypeEnum>formalParams, ALFATypes.BuiltInAnimEnum anim)
    {
        FormalParams = formalParams;
        Anim = anim;
    }
}
