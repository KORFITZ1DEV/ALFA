namespace ALFA.AST_Nodes;

public class LoopStmtNode : Node
{
    public AssignStmtNode AssignStmt { get; set; }
    public Node To { get; set; }
    public BlockNode Block { get; set; }
    
    public LoopStmtNode(AssignStmtNode assignStmt, Node to, BlockNode block, int line, int col) : base(line, col)
    {
        AssignStmt = assignStmt;
        To = to;
        Block = block;
    }
}