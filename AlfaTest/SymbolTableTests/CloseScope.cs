using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace AlfaTest.SymbolTableTests;

public class CloseScope
{
    [Theory]
    [ClassData(typeof(CloseScopeTestData))]
    public void CloseScopeTest(SymbolTable symbolTable, int expectedDepth, List<Symbol> expectedSymbols)
    {
        symbolTable.CloseScope();
        Assert.Equal(expectedDepth, symbolTable._depth);
        foreach (var symbol in expectedSymbols)
        {
            Assert.Equal(symbol, symbolTable._symbols[symbol.Name]);
        }
        
    }
    
}

public class CloseScopeTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol oldSymbol = new Symbol("Num1", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        Symbol midSymbol = new Symbol("Num2", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        Symbol newSymbol1 = new Symbol("Num3", numNode, ALFATypes.TypeEnum.@int, 16, 16);
        newSymbol1.Depth = 1;
        oldSymbol.Depth = 1;
        midSymbol.Depth = 1;
        newSymbol1.PrevSymbol = midSymbol;
        midSymbol.PrevSymbol = oldSymbol;
        SymbolTable symbolTable = new SymbolTable();
        symbolTable._depth = 1;
        symbolTable._symbols.Add(newSymbol1.Name, newSymbol1);
        symbolTable._scopeDisplay.Add(null);
        symbolTable._scopeDisplay[1] = newSymbol1;
        int expectedDepth = 0;
        var _symbols = new List<Symbol>()
        {
            newSymbol1, oldSymbol, midSymbol
        };
        yield return new object[]
        {
            symbolTable, expectedDepth, _symbols
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}