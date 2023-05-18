using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Moq;

namespace AlfaTest.Parser;

public class ParserTest
{
    
    [Theory]
    [ClassData(typeof(ParserTestData))]
    public void ParserProducesExpectedTree(string input, IParseTree expectedTree)
    {
        ICharStream stream = CharStreams.fromString(input);
        ITokenSource lexer = new ALFALexer(stream);
        ITokenStream tokens = new CommonTokenStream(lexer);
        ALFAParser parser = new ALFAParser(tokens);
        parser.BuildParseTree = true;
        IParseTree tree = parser.program();
        
        if(tree.ChildCount == expectedTree.ChildCount)
            DescendTree(tree, expectedTree);
        else
            Assert.Equal(expected: expectedTree, actual: tree);
    }

    private void DescendTree(IParseTree tree, IParseTree expectedTree)
    {
        for (int i = 0; i < tree.ChildCount; i++)
        {
            var treeChild = tree.GetChild(i);
            var expectedTreeChild = expectedTree.GetChild(i);
            
            Assert.Equal(expectedTree.GetType(), tree.GetType());
            Assert.Equal(expectedTree.ChildCount, tree.ChildCount);
            if ("TerminalNodeImpl" == tree.GetType().Name)
            {
                Assert.Equal(expectedTree.ToString(), tree.ToString());
            }
            DescendTree(treeChild, expectedTreeChild);
        }
    }

}

public class ParserTestData : IEnumerable<object[]>
{
    private readonly ProgramTreeMocker _programTreeMocker = new ProgramTreeMocker();
    public IEnumerator<object[]> GetEnumerator()
    {
        var expectedTree = _programTreeMocker.MockProgramTreeWithIntVarDcl();
        yield return new object[] { "int i = 2;", expectedTree};

        var expectedTree1 = _programTreeMocker.MockProgramTreeWithRectVarDcl();
        yield return new object[] { "rect myRect1 = createRect(100, 100, 100, 100);", expectedTree1};
        
        var expectedTree2 = _programTreeMocker.MockProgramTreeWithWait();
        yield return new object[] { "wait(100);", expectedTree2};
        
        var expectedTree3 = _programTreeMocker.MockProgramTreeWithMove();
        yield return new object[] {"move(myRect1, 100, 100);", expectedTree3};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}