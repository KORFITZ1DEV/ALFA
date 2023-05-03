using System.Collections;
using System.Security.Cryptography.X509Certificates;
using ALFA;
using ALFA.AST_Nodes;
using ALFA.Types;

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
        NumNode numNodeRedeclaredLetters = new NumNode(500, 15, 40);
        Symbol redeclaredLetters = new Symbol("Num", numNodeRedeclaredLetters, ALFATypes.TypeEnum.@int, 60, 10);
        symbolTableLetters._scopeDisplay[depthLetters] = redeclaredLetters;
                symbolTableLetters._symbols.Add(redeclaredLetters.Name, redeclaredLetters);
        
        yield return new object[]
        {
            redeclaredLetters, symbolTableLetters
        };
        
    }
    
    

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}