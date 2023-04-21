using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Moq;

namespace AlfaTest.Parser;

public class ParserTest
{
    private readonly ALFAParser _sut;
    
    private readonly Mock<ICharStream> _iCharStreamMock = new Mock<ICharStream>();
    private readonly Mock<ITokenSource> _iTokenSourceMock = new Mock<ITokenSource>();
    private readonly Mock<ITokenStream> _iTokenStreamMock = new Mock<ITokenStream>();


    public ParserTest()
    {
        _sut = new ALFAParser(_iTokenStreamMock.Object);
    }
    
    [Theory]
    [ClassData(typeof(ParserTestData))]
    public void VarDclDoesNotParse(string input)
    {
        
    }
    

}

public class ParserTestData : IEnumerable<object[]>
{
    private ParserMocker _parserMocker = new ParserMocker();
    public IEnumerator<object[]> GetEnumerator()
    {
        List<ALFAParser.StatementContext> parseTree = _parserMocker.MockParseTree();
        
        yield return new object[] { "var x = 1;" };

        
        yield return new object[] {"2 plus 7"};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}