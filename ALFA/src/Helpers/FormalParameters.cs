using ALFA.Types;
namespace ALFA;

public static class FormalParameters
{
    public static readonly Dictionary<string, List<ALFATypes.TypeEnum>> FormalParams = new()
    {
        {"createRect", new List<ALFATypes.TypeEnum>() 
            { ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int }},
            
        {"move", new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.rect, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int
        }},
            
        {"wait", new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.@int
        }}
    };
    
}