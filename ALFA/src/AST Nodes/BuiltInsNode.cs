using ALFA.Types;

namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public List<ALFATypes.TypeEnum> FormalParams;
    
    public ALFATypes.BuiltInTypeEnum BuiltInType;

    public BuiltInsNode(ALFATypes.BuiltInTypeEnum builtInType, List<ALFATypes.TypeEnum> formalParams, int line, int col) : base(line, col)
    {
        this.BuiltInType = builtInType;
        this.FormalParams = formalParams;
    }
}