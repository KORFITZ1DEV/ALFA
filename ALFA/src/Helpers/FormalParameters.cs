using System.Diagnostics.CodeAnalysis;
using ALFA.Types;
namespace ALFA;

[ExcludeFromCodeCoverage]
public static class FormalParameters
{
    public static readonly Dictionary<string, List<ALFATypes.TypeEnum>> FormalParams = new()
    {
        {"createRect", new List<ALFATypes.TypeEnum>() 
            { ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int }},
            
        {"move", new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.rect, ALFATypes.TypeEnum.@int,ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int
        }},
            
        {"wait", new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.@int
        }}
    };
    
}