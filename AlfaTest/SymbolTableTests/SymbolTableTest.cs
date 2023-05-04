using System.Collections;
using System.Security.Cryptography.X509Certificates;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;
using Moq;
using Newtonsoft.Json;

namespace AlfaTest.SymbolTableTests;

public class SymbolTableTest
{
    private SymbolTable _sut;
    public SymbolTableTest()
    {
        _sut = new SymbolTable();
    }
    [Theory]
    [InlineData("Rect1")]
    [InlineData("Square")]
    [InlineData("æsel")]
    [InlineData("1000$")]
    [InlineData("👶🏿👵🏿✊🏿")]
    public void RetrieveSymbolFails(string name)
    {
        Assert.Null(_sut.RetrieveSymbol(name));
    }

    [Theory]
    [ClassData(typeof(SymbolTableTestData))]
    public void RetrieveSymbolRetrievesSymbol(Symbol expectedSymbol, SymbolTable sut)
    {
        Assert.Equal(expectedSymbol, sut.RetrieveSymbol(expectedSymbol.Name));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 4)]
    public void OpenScopeIncrementsDepthAndAddsNullToDepth(int expectedDepth, int openThisManyScopes)
    {
        for (int i = 0; i < openThisManyScopes; i++)
        {
            _sut.OpenScope();
            Assert.Equal(null, _sut._scopeDisplay[i]);
        }
        Assert.Equal(expectedDepth, _sut._depth);
        
        
    }

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
        catch (RedeclaredVariableException rve )
        {
            Assert.True(true);
        }
    }

    [Theory]
    [ClassData(typeof(CloseScopeTestData))]
    public void CloseScope(SymbolTable symbolTable, int expectedDepth, List<Symbol> expectedSymbols)
    {
        symbolTable.CloseScope();
        Assert.Equal(expectedDepth, symbolTable._depth);
        foreach (var symbol in expectedSymbols)
        {
            Assert.Equal(symbol, symbolTable._symbols[symbol.Name]);
        }
        
    }

}

public class SymbolTableTestData : IEnumerable<object[]>
{
    
    public IEnumerator<object[]> GetEnumerator()
    {
        NumNode numNodeLetters = new NumNode(20, 25, 20);
        Symbol letters = new Symbol("Num", numNodeLetters, ALFATypes.TypeEnum.@int, 19, 20);
        
        SymbolTable symbolTableLetters = new SymbolTable();
        int depthLetters = 0;
        symbolTableLetters._scopeDisplay[depthLetters] = null;
        symbolTableLetters._scopeDisplay[depthLetters++] = letters;
        symbolTableLetters._symbols.Add(letters.Name, letters);

        yield return new object[]
        {
            letters, symbolTableLetters
        };
        
        NumNode numNodeLetters1 = new NumNode(20, 25, 20);
        Symbol letters1 = new Symbol("Num", numNodeLetters1, ALFATypes.TypeEnum.@int, 19, 20);
        
        SymbolTable symbolTableLetters1 = new SymbolTable();
        int depthLetters1 = 0;
        symbolTableLetters1._scopeDisplay[depthLetters1] = null;
        symbolTableLetters1._scopeDisplay[depthLetters1++] = letters1;
        symbolTableLetters1._symbols.Add(letters1.Name, letters1);
        
        NumNode numNodeRedeclaredLetters = new NumNode(500, 15, 40);
        Symbol redeclaredLetters = new Symbol("Num", numNodeRedeclaredLetters, ALFATypes.TypeEnum.@int, 60, 10);
        symbolTableLetters1._scopeDisplay.Add(null);
        symbolTableLetters1._scopeDisplay[depthLetters1] = redeclaredLetters;
        symbolTableLetters1._symbols.Remove(redeclaredLetters.Name);
        symbolTableLetters1._symbols.Add(redeclaredLetters.Name, redeclaredLetters);
        
        yield return new object[]
        {
            redeclaredLetters, symbolTableLetters1
        };
        
        NumNode numNode1 = new NumNode(20, 25, 20);
        Symbol symbol1 = new Symbol("Num", numNode1, ALFATypes.TypeEnum.@int, 1, 1);
        int depth1 = 0;
        symbol1.Depth = depth1;
        
        SymbolTable symbolTable1 = new SymbolTable();
        symbolTable1._scopeDisplay[depth1] = null;
        symbolTable1._scopeDisplay[depth1++] = symbol1;
        symbolTable1._symbols.Add(symbol1.Name, symbol1);

        NumNode numNode2 = new NumNode(500, 15, 40);
        Symbol symbol2 = new Symbol("Num", numNode2, ALFATypes.TypeEnum.@int, 2, 2);
        symbol2.Depth = 1;
        symbol2.PrevSymbol = symbol1;
        symbolTable1._scopeDisplay.Add(null);
        symbolTable1._scopeDisplay[depth1] = symbol2;
        symbolTable1._symbols.Remove(symbol2.Name);
        symbolTable1._symbols.Add(symbol2.Name, symbol2);

        yield return new object[]
        {
            symbol1, symbolTable1
        };
        
        
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
        symbolTableRedeclaredSymbol._scopeDisplay.Add(null);
        
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
        symbolTableRedeclaredSymbol._scopeDisplay.Add(null);
        
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

public class CloseScopeTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol oldSymbol = new Symbol("Num1", numNode, ALFATypes.TypeEnum.@int, 19, 20);
        Symbol midSymbol = new Symbol("Num2", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        Symbol newSymbol1 = new Symbol("Num3", numNode, ALFATypes.TypeEnum.@int, 15, 15);
        newSymbol1.Depth = 1;
        oldSymbol.Depth = 1;
        midSymbol.Depth = 1;
        newSymbol1.PrevSymbol = midSymbol;
        midSymbol.PrevSymbol = oldSymbol;
        SymbolTable symbolTable = new SymbolTable();
        symbolTable._depth = 1;
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