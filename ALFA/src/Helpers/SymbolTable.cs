namespace ALFA;
// Fischer Crafting a Compiler page 292 figure 8.7
public class SymbolTable
{
    public int _depth = 0;
    public Dictionary<string, Symbol> _symbols = new();
    public List<Symbol?>_scopeDisplay = new() {null};  //open the first scope (Global scope) init with null


    public void OpenScope()
    {
        _depth++;
        if (_scopeDisplay.Count != _depth + 1)
            _scopeDisplay.Add(null);
    }
    public void CloseScope()
    {
        List<KeyValuePair<string, Symbol>> keyValuePairs = new List<KeyValuePair<string, Symbol>>();
        foreach (var kvp in _symbols)
        {
            keyValuePairs.Add(kvp);
        }

        foreach (var symbolKvp in keyValuePairs)
        {
            var sym = symbolKvp.Value; 
            Symbol? prevSymbol = sym.PrevSymbol;

            //If a variable is equal or higher than current depth then the symbol must be removed.
            if (sym.Depth >= _depth)
            {
                _symbols.Remove(sym.Name);
                // If a variable is declared under the same name in an outer scope (when sym.PrevSymbol != null)
                // it should be added to the dictionary symbols dictionary.
                if (prevSymbol != null)
                {
                    _symbols.Add(prevSymbol.Name, prevSymbol); 
                }
            }
        }
        _depth--;

    }
    
    public void EnterSymbol(Symbol symbol)
    {
        Symbol? oldSymbol = RetrieveSymbol(symbol.Name);
        if (oldSymbol != null && oldSymbol.Depth == _depth) //
        {
            throw new VariableAlreadyDeclaredException(
                $"Symbol {symbol.Name} already declared on line {oldSymbol.LineNumber}:{oldSymbol.ColumnNumber}");
        }

        //If there is a variable declared with the same name on a lower depth
        if (oldSymbol == null || oldSymbol.Depth < _depth) 
        {
            if (_symbols.ContainsKey(symbol.Name))
            {
                oldSymbol = _symbols[symbol.Name];
                _symbols.Remove(symbol.Name);
            }

            symbol.Depth = _depth; //Need to assign correct depth
            _symbols.Add(symbol.Name, symbol);
        }

        //Sets the oldSymbol with the same name as the newSymbol to be the PrevSymbol
        //because newSymbol is in the nearest scope with the name.
        symbol.PrevSymbol = oldSymbol!;
    }

    public Symbol? RetrieveSymbol(string name) 
    {
        Symbol? sym;
        sym = _symbols.TryGetValue(name, out sym) ? sym : null;

        while (sym != null)
        {
            if (sym.Name == name && sym.Depth <= _depth) 
            {
                return sym;
            }

            sym = sym.PrevSymbol;
        }

        return sym;
    }

    public bool DeclaredLocally(string name)
    {
        bool isDeclaredLocally = false;
        Symbol? sym = _scopeDisplay[_depth];
        while (sym != null)
        {
            if (sym.Name == name)
            {
                isDeclaredLocally = true;
            }

            sym = sym.PrevSymbol;
        }

        return isDeclaredLocally; 
    }
}
