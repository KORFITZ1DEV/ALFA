using Antlr4.Runtime;

namespace ALFA.AST_Nodes;

public class ProgramNode : Node
{
    public List<StmtNode> stmts;
    
    public ProgramNode(List<StmtNode> stmts, int line, int col) : base(line, col)
    {
        this.stmts = stmts;
    }
    
    public override string ToString()
    {
        return "ProgramNode";
    }
}