using System.Collections;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

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
    public IEnumerator<object[]> GetEnumerator()
    {
        /* Line 28 program. Children in IParseTree
        [0] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [1] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [2] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [3] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [4] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [5] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [6] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [7] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [8] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [9] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [10] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [11] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [12] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [13] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [14] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [15] = {ALFAParser.StatementContext} {Antlr4.Runtime.Tree.IRuleNode.RuleContext: {[16]}}
        [16] = {TerminalNodeImpl} <EOF>*/
        
        yield return new object[] {"2 plus 7"};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}