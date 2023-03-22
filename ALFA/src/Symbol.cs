using ALFA.AST_Nodes;

namespace ALFA;

public class Symbol
{
    public StmtNode.TypeEnum Type;
    
    public string Name;
    public int Value;
    public int Depth; 
    
    public int LineNumber;
    public int ColumnNumber;

    public Symbol? PrevSymbol;

    public Symbol(string name, int value, StmtNode.TypeEnum type, int line, int column)
    {
        Name = name;
        Value = value;
        LineNumber =  line;
        ColumnNumber = column;
        Type = type;
    }

}