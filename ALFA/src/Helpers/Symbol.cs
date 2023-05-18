using System.Diagnostics.CodeAnalysis;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace ALFA;

[ExcludeFromCodeCoverage]
public class Symbol
{
    public ALFATypes.TypeEnum Type;
    
    public string Name;
    public Node Value;
    public int Depth; 
    
    public int LineNumber;
    public int ColumnNumber;

    public Symbol? PrevSymbol;

    public Symbol(string name, Node value, ALFATypes.TypeEnum type, int line, int column)
    {
        Name = name;
        Value = value;
        LineNumber =  line;
        ColumnNumber = column;
        Type = type;
    }
}