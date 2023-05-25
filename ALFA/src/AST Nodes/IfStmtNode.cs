namespace ALFA.AST_Nodes;

public class IfStmtNode : Node
{
    public List<Node> Expressions { get; set; }
    public List<BlockNode> Blocks { get; set; }
    
    public IfStmtNode(List<Node> expressions, List<BlockNode> blocks, int line, int col) : base(line, col)
    {
        Expressions = expressions;
        Blocks = blocks;
    }
}