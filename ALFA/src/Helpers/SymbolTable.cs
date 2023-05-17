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
        _scopeDisplay.Add(null);
    }
    public void CloseScope()
    {
        Symbol? sym = _scopeDisplay[_depth];
        while (sym != null)
        {
            Symbol? prevSymbol = sym.PrevSymbol;
            // If a variable is declared under the same name in an outer scope (when sym.PrevSymbol != null)
            // it should be added to the dictionary symbols dictionary.
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
        if (oldSymbol != null && oldSymbol.Depth == _depth) //
        {
            throw new RedeclaredVariableException(
                $"Symbol {symbol.Name} already declared on line {oldSymbol.LineNumber}:{oldSymbol.ColumnNumber}");
        }

        Symbol newSymbol = new(symbol.Name, symbol.Value, symbol.Type, symbol.LineNumber, symbol.ColumnNumber);
        newSymbol.Depth = _depth;
        _scopeDisplay[_depth] = newSymbol;

        if (oldSymbol == null || oldSymbol.Depth < _depth)
        {
            if (_symbols.ContainsKey(symbol.Name))
            {
                oldSymbol = _symbols[symbol.Name];
                _symbols.Remove(symbol.Name);
            }
            _symbols.Add(symbol.Name, newSymbol);
        }

        //As the oldSymbol is set to previous symbol, one with the same name
        //This could result in an issue when closing a scope as the oldsymbol
        //may be overwritten and thus maybe lost.
        newSymbol.PrevSymbol = oldSymbol!;
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