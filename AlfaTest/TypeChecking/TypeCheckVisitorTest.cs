using ALFA;

namespace AlfaTest.TypeChecking;

public class TypeCheckVisitorTest
{
    [Theory]
    [ClassData(typeof(TypeCheckingThrowsExceptionTestData))]
    public void TypeCheckTestThrowsException(string prog, string comment, Type exceptionType)
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
                default:
                    Assert.Equal(new Exception("randomstuff"), actualException);
                    break;
            }
        }
    }
    [Theory]
    [ClassData(typeof(TypeCheckingNoExceptionTestData))]
    public void TypeCheckTestNoException(string prog, string comment, Type exceptionType)
    {
        Prog.Main(new string[] {prog, "../../../../ALFA/CodeGen-p5.js/Output", "--test"});
        Assert.True(true);
    }
}



