namespace ALFA;
// Fischer Crafting a Compiler page 292 figure 8.7
public class SymbolTable
{
    private int _depth = 0;
    private Dictionary<string, Symbol> _symbols = new(); 
    private List<Symbol?>_scopeDisplay = new() {null};  //open the first scope (Global scope) init with null

    public void OpenScope()
    {
        _depth++;
        _scopeDisplay[_depth] = null;
    }
    
    public void CloseScope()
    {
        Symbol? sym = _scopeDisplay[_depth];
        while (sym != null)
        {
            Symbol? prevSymbol = sym.PrevSymbol;
            _symbols.Remove(sym.Name);
            if (prevSymbol != null)
            {
                _symbols.Add(prevSymbol.Name, prevSymbol);
            }

            sym = prevSymbol;
        }
        _depth--;
    }
    
    public void EnterSymbol(Symbol symbol)
    {
        Symbol? oldSymbol = RetrieveSymbol(symbol.Name);
        if (oldSymbol != null && oldSymbol.Depth == _depth)
        {
            throw new Exception($"Symbol {symbol.Name} already declared on line {oldSymbol.LineNumber}:{oldSymbol.ColumnNumber}");
        }
        
        Symbol newSymbol = new(symbol.Name, symbol.Value, symbol.Type, symbol.LineNumber, symbol.ColumnNumber);
        newSymbol.Depth = _depth;
        _scopeDisplay[_depth] = newSymbol;

        if (oldSymbol == null)
        {
            _symbols.Add(symbol.Name, newSymbol);
        }
        else
        {
            _symbols.Remove(oldSymbol.Name);
            _symbols.Add(symbol.Name, newSymbol);
        }

        newSymbol.PrevSymbol = oldSymbol!;
    }

    public Symbol? RetrieveSymbol(string name)
    {
        Symbol? sym;
        if (!_symbols.TryGetValue(name, out sym)) return null;
        while (sym != null)
        {
            if (sym.Name == name)
            {
                return sym;
            }

            sym = sym.PrevSymbol;
        }
        return null;
    }

    public bool DeclaredLocally(string name)
    {
        return _scopeDisplay[_depth] != null && _scopeDisplay[_depth]!.Name == name;
    }
}