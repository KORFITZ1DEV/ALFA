namespace ALFA;

public class SymbolTable
{
    private int _depth = 0;
    private Dictionary<string, Symbol> _symbols = new();
    private List<Symbol?>_scopeDisplay = new();

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
    
    public void EnterSymbol(string name, int value)
    {
        Symbol? oldSymbol = RetrieveSymbol(name);
        if (oldSymbol != null && oldSymbol.Depth == _depth)
        {
            throw new Exception($"Symbol {name} already declared");
        }

        Symbol newSymbol = new(name, value);
        newSymbol.Depth = _depth;
        _scopeDisplay[_depth] = newSymbol;

        if (oldSymbol == null)
        {
            _symbols.Add(name, newSymbol);
        }
        else
        {
            _symbols.Remove(oldSymbol.Name);
            _symbols.Add(name, newSymbol);
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