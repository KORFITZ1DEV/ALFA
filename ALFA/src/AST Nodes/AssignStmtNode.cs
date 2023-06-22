using ALFA.Types;

namespace ALFA.AST_Nodes;

public class AssignStmtNode : Node
{
    public string Identifier { get; set; }
    public Node Value { get; set; }
    public ALFATypes.TypeEnum VarDclParentType { get; set; }
    
    public AssignStmtNode(int line, int col) : base(line, col)
    {
    }
    public AssignStmtNode(string identifier, Node value, int line, int col) : base(line, col)
    {
        Identifier = identifier;
        Value = value;
    } 
    public AssignStmtNode(string identifier, Node value, int line, int col, ALFATypes.TypeEnum varDclParentType) : base(line, col)
    {
        Identifier = identifier;
        Value = value;
        VarDclParentType = varDclParentType;
    }
}