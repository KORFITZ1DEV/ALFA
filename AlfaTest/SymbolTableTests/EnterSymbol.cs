using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using Newtonsoft.Json;

namespace AlfaTest.SymbolTableTests;

public class EnterSymbol
{
    [Theory]
    [ClassData(typeof(EnterSymbolTestData))]
    public void EnterSymbolAddsSymbolToSymbolsDictionary(Symbol expectedSymbol, SymbolTable sut)
    {
        sut.EnterSymbol(expectedSymbol);

        var expectedSymbolSerialized = JsonConvert.SerializeObject(expectedSymbol);
        var actualSymbolSerialized = JsonConvert.SerializeObject(sut._symbols[expectedSymbol.Name]);
        
        Assert.Equal(expectedSymbolSerialized, actualSymbolSerialized);
    }
    
    [Theory]
    [ClassData(typeof(RedeclaredVariableExceptionTestData))]
    public void EnterSymbolThrowsRedeclaredVariableException(Symbol expectedSymbol, SymbolTable sut)
    {
        try
        {
            sut.EnterSymbol(expectedSymbol);
        }
        catch (VariableAlreadyDeclaredException rve )
        {
            Assert.True(true);
        }
    }
}

public class EnterSymbolTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol newSymbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        SymbolTable symbolTableNewSymbol = new SymbolTable();

        yield return new object[]
        {
            newSymbol, symbolTableNewSymbol
        };
        
        Symbol oldSymbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        oldSymbol.Depth = 0;
        Symbol newSymbol1 = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        newSymbol1.Depth = 1;
        newSymbol1.PrevSymbol = oldSymbol;
        SymbolTable symbolTableRedeclaredSymbol = new SymbolTable();
        symbolTableRedeclaredSymbol._symbols.Add(oldSymbol.Name, oldSymbol);
        symbolTableRedeclaredSymbol._depth++;

        yield return new object[]
        {
            newSymbol1, symbolTableRedeclaredSymbol
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class RedeclaredVariableExceptionTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol oldSymbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        Symbol newSymbol1 = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        newSymbol1.Depth = 0;
        newSymbol1.PrevSymbol = oldSymbol;
        SymbolTable symbolTableRedeclaredSymbol = new SymbolTable();
        symbolTableRedeclaredSymbol._symbols.Add(oldSymbol.Name, oldSymbol);
        
        yield return new object[]
        {
            newSymbol1, symbolTableRedeclaredSymbol
        };
        
        
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}