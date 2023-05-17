using ALFA;

namespace AlfaTest.ProgTest;

public class ProgMainTest
{
    [Theory]
    [ClassData(typeof(ProgMainThrowsExceptionTestData))]
    public void TranspilingThrowsExpectedException(string prog, string comment, Type exceptionType)
    {
        try
        {
            if (exceptionType == null)
            {
                Console.WriteLine(prog);
            }
            Prog.Main(new string[] {prog, "../../../../ALFA/Output", "--test"});
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
                case RedeclaredVariableException:
                    Assert.Equal(exceptionType, typeof(RedeclaredVariableException));
                    break;
                case SyntacticException:
                    Assert.Equal(exceptionType, typeof(SyntacticException));
                    break;
                default:
                    Assert.Equal(new Exception("randomstuff"), actualException);
                    break;
            }
        }
    }

    [Theory]
    [ClassData(typeof(ProgMainNoExceptionTestData))]
    public void TranspilingThrowsNoException(string prog, string comment)
    {
        Prog.Main(new string[] {prog, "../../../../ALFA/CodeGen-p5.js/Output", "--test"});
        Assert.True(true);
    }
}



