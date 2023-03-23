namespace ALFA.AST_Nodes;

public abstract class Node
{
    public int Line;
    public int Col;

    public Node() {}
    
    public Node(int line, int col)
    {
        this.Line = line;
        this.Col = col;
    }
}
 