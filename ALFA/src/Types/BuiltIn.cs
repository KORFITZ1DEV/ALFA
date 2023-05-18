using System.Diagnostics.CodeAnalysis;

namespace ALFA.Types;

[ExcludeFromCodeCoverage]
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
