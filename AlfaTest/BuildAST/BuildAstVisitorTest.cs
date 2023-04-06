using ALFA;

namespace AlfaTest.BuildAST;

public class BuildAstVisitorTest
{
    [Theory]
    [ClassData(typeof(BuildAstThrowsExceptionTestData))]
    public void BuildAstTestThrowsException(string prog, string comment, Type exceptionType)
    {
        try
        {
            Prog.Main(new string[] {prog, "../../../../ALFA/Output/sketch.js", "--test"});
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
        Prog.Main(new string[] {prog, "../../../../ALFA/Output/sketch.js", "--test"});
        Assert.True(true);
    }
}