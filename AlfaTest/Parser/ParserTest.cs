using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Moq;

namespace AlfaTest.Parser;

public class ParserTest
{
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
        var mockedProgram = expectedTree.Object.GetChild(0);

        
        if(tree.ChildCount == mockedProgram.ChildCount)
        {
            DescentTree(tree, mockedProgram);
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
            
            Assert.Equal(expectedTreeChild.GetType(), treeChild.GetType());
            Assert.Equal(expectedTreeChild.ChildCount, treeChild.ChildCount);
            if ("TerminalNodeImpl" == treeChild.GetType().Name)
            {
                Assert.Equal(expectedTreeChild.ToString(), treeChild.ToString());
            }
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
        var programNode = _programTreeMocker.MockProgramTreeWithIntVarDcl();
        
        mockedParseTree.Setup(x=>x.GetChild(0)).Returns(programNode);
        yield return new object[] { "int i = 2;", mockedParseTree};

        var mockedParseTree1 = new Mock<IParseTree>();
        mockedParseTree1.SetupGet(x => x.ChildCount).Returns(2);
        var programNode1 = _programTreeMocker.MockProgramTreeWithRectVarDcl();
        mockedParseTree1.Setup(x => x.GetChild(0)).Returns(programNode1);
        yield return new object[] { "rect myRect1 = createRect(100, 100, 100, 100);", mockedParseTree1};
        
        var mockedParseTree2 = new Mock<IParseTree>();
        mockedParseTree2.SetupGet(x => x.ChildCount).Returns(2);
        var programNode2 = _programTreeMocker.MockProgramTreeWithWait();
        mockedParseTree2.Setup(x => x.GetChild(0)).Returns(programNode2);
        yield return new object[] { "wait(100);", mockedParseTree2};
        
        var mockedParseTree3 = new Mock<IParseTree>();
        mockedParseTree3.SetupGet(x => x.ChildCount).Returns(2);
        var programNode3 = _programTreeMocker.MockProgramTreeWithMove();
        mockedParseTree3.Setup(x => x.GetChild(0)).Returns(programNode3);
        yield return new object[] {"move(myRect1, 100, 100);", mockedParseTree3};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}