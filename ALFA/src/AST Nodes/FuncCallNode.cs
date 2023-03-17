namespace ALFA.AST_Nodes;

public class FuncCallNode : Node
{
    public BuiltInsNode BuiltIns;
    public List<ArgNode> Args;
    
    public FuncCallNode(BuiltInsNode builtIns, List<ArgNode> args)
    {
        this.BuiltIns = builtIns;
        this.Args = args;
    }
}