using System.Collections;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;

namespace AlfaTest.SymbolTableTests;

public class RetrieveSymbol
{
    private readonly SymbolTable _sut;
    
    public RetrieveSymbol()
    {
        _sut = new SymbolTable();
    }
    
    [Theory]
    [InlineData("Rect1")]
    [InlineData("Square")]
    [InlineData("æsel")]
    [InlineData("1000$")]
    [InlineData("👶🏿👵🏿✊")]
    public void RetrieveSymbolFails(string name)
    {
        Assert.Null(_sut.RetrieveSymbol(name));
    }
    
    [Theory]
    [ClassData(typeof(RetrieveSymbolTestData))]
    public void RetrieveSymbolRetrievesSymbol(Symbol expectedSymbol, SymbolTable sut)
    {
        Assert.Equal(expectedSymbol, sut.RetrieveSymbol(expectedSymbol.Name));
    }
}

public class RetrieveSymbolTestData : IEnumerable<object[]>
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


