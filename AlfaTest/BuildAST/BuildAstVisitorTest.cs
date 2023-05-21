using ALFA;
using ALFA.AST_Nodes;
using ALFA.Visitors;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace AlfaTest.BuildAST;

public class BuildAstVisitorTest
{
    private BuildASTVisitor _sut;
    public BuildAstVisitorTest()
    {
         _sut = new BuildASTVisitor(new SymbolTable ());
    }
    
    [Theory]
    [ClassData(typeof(BuildAstThrowsExceptionTestData))]
    public void BuildAstTestThrowsException(string prog, string comment, Type expectedExceptionType)
    {
        ICharStream stream = CharStreams.fromString(prog);
        ITokenSource lexer = new ALFALexer(stream);
        ITokenStream tokens = new CommonTokenStream(lexer);
        ALFAParser parser = new ALFAParser(tokens);
        parser.BuildParseTree = true;
        IParseTree tree = parser.program();

        try
        {
            Node ast = _sut.Visit(tree); 
            Assert.True(false, "Expected exception was not thrown");
        }
        catch (Exception actualException)
        {
            switch (actualException)
            {
                case TypeException:
                    Assert.Equal(expectedExceptionType, typeof(TypeException));
                    break;
                case ArgumentTypeException:
                    Assert.Equal(expectedExceptionType, typeof(ArgumentTypeException));
                    break;
                case InvalidNumberOfArgumentsException:
                    Assert.Equal(expectedExceptionType, typeof(InvalidNumberOfArgumentsException));
                    break;
                case UnknownBuiltinException:
                    Assert.Equal(expectedExceptionType, typeof(UnknownBuiltinException));
                    break;
                case UndeclaredVariableException:
                    Assert.Equal(expectedExceptionType, typeof(UndeclaredVariableException));
                    break;
                case VariableAlreadyDeclaredException:
                    Assert.Equal(expectedExceptionType, typeof(VariableAlreadyDeclaredException));
                    break;
                case SyntacticException:
                    Assert.Equal(expectedExceptionType, typeof(SyntacticException));
                    break;
                case NonPositiveAnimationDurationException:
                    Assert.Equal(expectedExceptionType, typeof(NonPositiveAnimationDurationException));
                    break;
                default:
                    Assert.Equal(actualException, new Exception("look at actual"));
                    break;
            }
        }
    }
    [Theory]
    [ClassData(typeof(BuildAstNoExceptionTestData))]
    public void BuildAstTestNoException(string prog)
    {
        ICharStream stream = CharStreams.fromString(prog);
        ITokenSource lexer = new ALFALexer(stream);
        ITokenStream tokens = new CommonTokenStream(lexer);
        ALFAParser parser = new ALFAParser(tokens);
        parser.BuildParseTree = true;
        IParseTree tree = parser.program();
        Node ast = _sut.Visit(tree); 

        
        Assert.True(true);
    }
}