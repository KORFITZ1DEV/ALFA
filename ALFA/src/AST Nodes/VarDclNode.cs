using ALFA.Types;

namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public ALFATypes.TypeEnum Type { get; set; }
    public AssignStmtNode AssignStmt { get; set; }
    
    public VarDclNode(int line, int col) : base(line, col)
    {
    }
    public VarDclNode(ALFATypes.TypeEnum type, AssignStmtNode assignStmt, int line, int col) : base(line, col)
    {
        Type = type;
        AssignStmt = assignStmt;
    }
}