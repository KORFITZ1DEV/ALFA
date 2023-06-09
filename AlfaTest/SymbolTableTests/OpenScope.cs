using ALFA;

namespace AlfaTest.SymbolTableTests;

public class OpenScope
{
    private SymbolTable _sut;
    
    public OpenScope()
    {
        _sut = new SymbolTable();
    } 
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 4)]
    public void OpenScopeIncrementsDepth(int expectedDepth, int openThisManyScopes)
    {
        for (int i = 0; i < openThisManyScopes; i++)
        {
            _sut.OpenScope();
        }
        
        Assert.Equal(expectedDepth, _sut._depth);
        
    }
}