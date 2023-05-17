using System.Collections;
using System.Text.Json;
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
        //Serialized to avoid asserting equality on the reference.
        var serializedExpectedSymbol = JsonSerializer.Serialize(expectedSymbol);
        var serializedActualSymbol = JsonSerializer.Serialize(sut.RetrieveSymbol(expectedSymbol.Name));
        Assert.Equal(serializedExpectedSymbol, serializedActualSymbol);
    }

    [Fact]
    public void RetrieveSymbolReturnsNullWhenSymbolIsOnlyDeclaredAtHigherDepth()
    {
        //Tests that a symbol that is declared on a higher depth will not be retrieved
        NumNode numNode = new NumNode(20, 25, 20);
        Symbol symbol = new Symbol("Num", numNode, ALFATypes.TypeEnum.@int, 1, 1);
        int depth = 1;
        symbol.Depth = depth;
        
        SymbolTable sut = new SymbolTable();
        sut.OpenScope();
        sut.EnterSymbol(symbol);
        sut.CloseScope();
        
        Assert.Null(sut.RetrieveSymbol("Num"));
    } 

}

public class RetrieveSymbolTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        //Tests that a symbol added to the scopeDisplay on depth 0 can be retrieved
        NumNode numNodeLetters = new NumNode(20, 25, 20);
        Symbol letters = new Symbol("Num", numNodeLetters, ALFATypes.TypeEnum.@int, 19, 20);
        
        SymbolTable symbolTableLetters = new SymbolTable();
        symbolTableLetters.EnterSymbol(letters);

        yield return new object[]
        {
            letters, symbolTableLetters
        };
        
        //Tests that a symbol under the same name that has been added to the scopeDisplay on depth 0 and 1
        //will retrieve the closest symbol, here the one in the scopeDisplay for depth 1.
        NumNode numNodeLetters1 = new NumNode(20, 25, 20);
        Symbol letters1 = new Symbol("Num", numNodeLetters1, ALFATypes.TypeEnum.@int, 19, 20);
        
        SymbolTable symbolTableLetters1 = new SymbolTable();
        symbolTableLetters1.EnterSymbol(letters1);
        
        NumNode numNodeRedeclaredLetters = new NumNode(500, 15, 40);
        Symbol redeclaredLetters = new Symbol("Num", numNodeRedeclaredLetters, ALFATypes.TypeEnum.@int, 60, 10);
        symbolTableLetters1.OpenScope();
        symbolTableLetters1.EnterSymbol(redeclaredLetters);
        
        yield return new object[]
        {
            redeclaredLetters, symbolTableLetters1
        };
        
        //Tests that a symbol that is declared on a lower depth can be retrieved
        //In this case a symbol under the name num is declared on depth 0 but is retrieved on depth 1.
        NumNode numNode1 = new NumNode(20, 25, 20);
        Symbol symbol1 = new Symbol("Num", numNode1, ALFATypes.TypeEnum.@int, 1, 1);
        int depth1 = 0;
        symbol1.Depth = depth1;
        
        SymbolTable symbolTable1 = new SymbolTable();
        symbolTable1.EnterSymbol(symbol1);
        symbolTable1.OpenScope();

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

public class RetrieveSymbolIntegrationTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {

        };

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


