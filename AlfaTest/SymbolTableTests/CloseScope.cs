using System.Collections;
using System.Text.Json;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using Newtonsoft.Json;

namespace AlfaTest.SymbolTableTests;

public class CloseScope
{
    private SymbolTable _sut;
    public CloseScope()
    {
        _sut = new SymbolTable();
    }
    
    [Theory]
    [ClassData(typeof(CloseScopeHigherDepthSymbolsData))]
    public void CloseScopeHigherDepthSymbolsCannotBeRetrieved(SymbolTable sut, int expectedDepth, List<Symbol> expectedSymbols)
    {
        sut.CloseScope();
        Assert.Equal(expectedDepth, sut._depth);
        foreach (var higherDepthDeclaredSymbol in expectedSymbols)
        {
            Assert.Null(sut.RetrieveSymbol(higherDepthDeclaredSymbol.Name));
        }
        
    }

    [Fact]
    public void ItIsNotPossibleToRetrieveSymbolsFromAClosedScopeThatDoesNotPersistInAnAnimationDeclaration()
    {
        /*Arrange*/
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol symbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        symbol.Depth = 1;
        _sut.OpenScope();
        _sut.EnterSymbol(symbol);
        _sut.CloseScope();
        _sut.OpenScope();
        
        /*Act*/
        Symbol? actualSymbol = _sut.RetrieveSymbol("Num");

        /*Assert*/
        Assert.Null(actualSymbol);
    }

    [Fact]
    public void ARedeclaredSymbolWillAddTheNearestSymbolWithSameNameTo_symbolsWhenScopeIsClosed()
    {
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol symbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        Symbol redeclaredSymbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 25, 20);
        _sut.EnterSymbol(symbol);
        _sut.OpenScope();
        _sut.EnterSymbol(redeclaredSymbol);
        _sut.CloseScope();

        var serializedExpectedSymbol = JsonConvert.SerializeObject(symbol);
        var serializedActualSymbol = JsonConvert.SerializeObject(_sut.RetrieveSymbol(symbol.Name));

        Assert.Equal(serializedExpectedSymbol, serializedActualSymbol);
    }
}

public class CloseScopeHigherDepthSymbolsData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        
        //Tests that symbols entered on a higher depth cannot be retrieved on a lower depth.
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol oldSymbol = new Symbol("Num1", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        oldSymbol.Depth = 1;
        Symbol midSymbol = new Symbol("Num2", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        midSymbol.Depth = 1;
        Symbol newSymbol1 = new Symbol("Num3", numNode, ALFATypes.TypeEnum.@int, 16, 16);
        newSymbol1.Depth = 1;

        SymbolTable symbolTable = new SymbolTable();
        symbolTable.OpenScope();
        symbolTable.EnterSymbol(oldSymbol);
        symbolTable.EnterSymbol(midSymbol);
        symbolTable.EnterSymbol(newSymbol1);
        
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