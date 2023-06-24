namespace ALFA;
// Fischer Crafting a Compiler page 292 figure 8.7
public class SymbolTable
{
    public int _depth = -1;
    public Dictionary<string, Symbol> _symbols = new();

    public SymbolTable() => OpenScope();  //open the first scope (Global scope)

    public void OpenScope() => _depth++;
    
    public void CloseScope()
    {
        foreach (var symbol in _symbols.Values.Where(s => s.Depth == _depth))
        {
            if (symbol.PrevSymbol != null)
            {
                _symbols[symbol.Name] = symbol.PrevSymbol;
                continue;
            }
            _symbols.Remove(symbol.Name);
        }
        
        _depth--;
    }
    
    public void EnterSymbol(Symbol symbol)
    {
        Symbol? oldSymbol = RetrieveSymbol(symbol.Name); // check if symbol already exists
        if (oldSymbol != null && oldSymbol.Depth == _depth) // if symbol exists in current scope
            throw new VariableAlreadyDeclaredException(
                $"Symbol {symbol.Name} already declared on line {oldSymbol.LineNumber}:{oldSymbol.ColumnNumber}");
        
        // no need to construct a new symbol, but still need to update depth
        symbol.Depth = _depth;
        
        // if shadowing
        if (oldSymbol != null && oldSymbol.Depth < _depth)
        {
            symbol.PrevSymbol = oldSymbol;
            _symbols[oldSymbol.Name] = symbol;
            return;
        }

        _symbols.Add(symbol.Name, symbol);
    }

    public Symbol? RetrieveSymbol(string name)
    {
        Symbol? sym;
        sym = _symbols.TryGetValue(name, out sym) ? sym : null;

        while (sym != null)
        {
            if (sym.Name == name && sym.Depth <= _depth)
                return sym;

            sym = sym.PrevSymbol;
        }
        
        return sym;
    }
}