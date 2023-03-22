namespace ALFA;

public class Symbol
{
    public string Name;
    public int Value;
    public int Depth; 
    
    public int LineNumber;
    public int ColumnNumber;

    public Symbol? PrevSymbol;

    public Symbol(string name, int value, int line, int column)
    {
        Name = name;
        Value = value;
        LineNumber =  line;
        ColumnNumber = column;
    }

}