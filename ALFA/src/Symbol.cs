namespace ALFA;

public class Symbol
{
    public string Name;
    public int Value;
    public int Depth; //hvorfor ikke uint?
    
    public uint LineNumber;
    public uint ColumnNumber;

    public Symbol? PrevSymbol;

    public Symbol(string name, int value)
    {
        Name = name;
        Value = value;
    }

}