namespace ALFA.AST_Nodes;

public class ProgramNode : Node
{
    public List<StmtNode> stmts;
    
    public ProgramNode(List<StmtNode> stmts)
    {
        this.stmts = stmts;
    }
    
}