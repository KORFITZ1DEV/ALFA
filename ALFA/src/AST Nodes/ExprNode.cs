using ALFA.Types;

namespace ALFA.AST_Nodes;

public class ExprNode : Node
{
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node Value { get; set; }
    public string Operator { get; set; }
    public ExprNode(string op, Node left, Node right, int line, int col) : base(line, col)
    {
        Operator = op;
        Left = left;
        Right = right;
    }
    public ExprNode(string op, Node left, int line, int col) : base(line, col) // unary
    {
        Operator = op;
        Left = left;
    }
}