using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Moq;

namespace AlfaTest.Parser;

public class ParserTest
{

    public ParserTest()
    {
    }
    
    [Theory]
    [ClassData(typeof(ParserTestData))]
    public void ParserProducesExpectedTree(string input, Mock<IParseTree> expectedTree)
    {
        ICharStream stream = CharStreams.fromString(input);
        ITokenSource lexer = new ALFALexer(stream);
        ITokenStream tokens = new CommonTokenStream(lexer);
        ALFAParser parser = new ALFAParser(tokens);
        parser.BuildParseTree = true;
        IParseTree tree = parser.program();

        
        if(tree.ChildCount == expectedTree.Object.ChildCount)
        {
            DescentTree(tree, expectedTree.Object);
        }
        else
        {
            //At this point we know the two trees are not equal.
            Assert.Equal(expected: expectedTree.Object, actual: tree);
        }
        
    }

    private void DescentTree(IParseTree tree, IParseTree expectedTree)
    {
        for (int i = 0; i < tree.ChildCount; i++)
        {
            var treeChild = tree.GetChild(i);
            var expectedTreeChild = expectedTree.GetChild(i);
            
            Assert.Equal(treeChild, expectedTreeChild);
            DescentTree(tree.GetChild(i), expectedTree.GetChild(i));
        }
    }

}

public class ParserTestData : IEnumerable<object[]>
{
    private readonly ProgramTreeMocker _programTreeMocker = new ProgramTreeMocker();
    public IEnumerator<object[]> GetEnumerator()
    {
        var mockedParseTree = new Mock<IParseTree>();
        
        mockedParseTree.SetupGet(x=>x.ChildCount).Returns(2);
        var programNode = _programTreeMocker.MockProgramTree();
        
        mockedParseTree.Setup(x=>x.GetChild(0)).Returns(programNode);
        yield return new object[] { "int i = 2;", mockedParseTree};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}