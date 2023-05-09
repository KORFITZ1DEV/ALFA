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
    public void BuildAstTestThrowsException(string prog, string comment, Type exceptionType)
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
                    Assert.Equal(exceptionType, typeof(TypeException));
                    break;
                case ArgumentTypeException:
                    Assert.Equal(exceptionType, typeof(ArgumentTypeException));
                    break;
                case InvalidNumberOfArgumentsException:
                    Assert.Equal(exceptionType, typeof(InvalidNumberOfArgumentsException));
                    break;
                case UnknownBuiltinException:
                    Assert.Equal(exceptionType, typeof(UnknownBuiltinException));
                    break;
                case UndeclaredVariableException:
                    Assert.Equal(exceptionType, typeof(UndeclaredVariableException));
                    break;
                default:
                    Assert.Equal(new Exception("randomstuff"), actualException);
                    break;
            }
        }
    }
    [Theory]
    [ClassData(typeof(BuildAstNoExceptionTestData))]
    public void BuildAstTestNoException(string prog, string comment, Type exceptionType)
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