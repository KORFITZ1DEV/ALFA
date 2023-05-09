using ALFA.Types;
namespace ALFA;

public static class FormalParameters
{
    public static readonly Dictionary<string, BuiltIn> FormalParams = new()
    {
        {"createRect", new BuiltIn(
            new List<ALFATypes.TypeEnum>() 
                { ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int }, 
            ALFATypes.BuiltInTypeEnum.createRect)},
            
        {"move", new BuiltIn(new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.rect, ALFATypes.TypeEnum.@int, ALFATypes.TypeEnum.@int
        },ALFATypes.BuiltInTypeEnum.move)},
            
        {"wait", new BuiltIn(new List<ALFATypes.TypeEnum>()
        {
            ALFATypes.TypeEnum.@int
        }, ALFATypes.BuiltInTypeEnum.wait)}
    };
    
}