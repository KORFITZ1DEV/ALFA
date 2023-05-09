using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace AlfaTest.SymbolTableTests;

public class DeclaredLocally
{
    [Theory]
    [ClassData(typeof(DeclaredLocallyTestData))]
    public void DeclaredLocallyTests(string symbolName, SymbolTable symbolTable, bool expected)
    {
        Assert.Equal(expected ,symbolTable.DeclaredLocally(symbolName));
    }
}

public class DeclaredLocallyTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        SymbolTable _symbolTable = new SymbolTable();
        _symbolTable._depth = 0;
        NumNode _numNode = new NumNode(20, 25, 20);
        Symbol oldSymbol = new Symbol("Num1", _numNode, ALFATypes.TypeEnum.@int, 19, 20);
        Symbol midSymbol = new Symbol("Num2", _numNode, ALFATypes.TypeEnum.@int, 15, 15);
        Symbol newSymbol1 = new Symbol("Num3", _numNode, ALFATypes.TypeEnum.@int, 16, 16);
        newSymbol1.Depth = 0;
        oldSymbol.Depth = 0;
        midSymbol.Depth = 0;
        newSymbol1.PrevSymbol = midSymbol;
        midSymbol.PrevSymbol = oldSymbol;
        _symbolTable._scopeDisplay[0] = newSymbol1;
        yield return new object[]
        {
            newSymbol1.Name, _symbolTable, true
        };
        yield return new object[]
        {
            oldSymbol.Name, _symbolTable, true
        };
        yield return new object[]
        {
            midSymbol.Name, _symbolTable, true
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}