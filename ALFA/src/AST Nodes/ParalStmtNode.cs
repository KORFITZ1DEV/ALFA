namespace ALFA.AST_Nodes;

public class ParalStmtNode : Node
{
    public ParalBlockNode Block { get; set; }

    public ParalStmtNode(ParalBlockNode block, int line, int col) : base(line, col)
    {
        Block = block;
    }
}