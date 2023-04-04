using ALFA.Types;

namespace ALFA.AST_Nodes;

public class VarDclNode : Node
{
    public ALFATypes.TypeEnum Type { get; set; }
    
    public string Identifier { get; set; }
    public Node Value { get; set; } // either a FuncCallNode or a NumNode

    public VarDclNode(ALFATypes.TypeEnum type, string identifier, Node value, int line, int col) : base(line, col)
    {
        Type = type;
        Identifier = identifier;
        Value = value;
    }
}