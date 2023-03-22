namespace ALFA.AST_Nodes;

public class BuiltInsNode : Node
{
    public enum BuiltInTypeEnum
    {
        CreateSquare,
        Move,
        Wait
    }
    
    public List<StmtNode.TypeEnum> FormalParams = new List<StmtNode.TypeEnum>();
    

    public BuiltInTypeEnum BuiltInType;

    public BuiltInsNode(BuiltInTypeEnum builtInType, List<StmtNode.TypeEnum> formalParams, int line, int col) : base(line, col)
    {
        this.BuiltInType = builtInType;
        this.FormalParams = formalParams;
    }
}