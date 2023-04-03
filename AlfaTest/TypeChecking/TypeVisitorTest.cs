using ALFA;

namespace AlfaTest;

public class TypeVisitorTest
{
    [Theory]
    [ClassData(typeof(FalseTypeTestData))]
    public void TypeTestThrowException(string prog, string comment, Type exceptionType)
    {
        try
        {
            Prog.MyParseMethod(prog: prog, output: "../../../../ALFA/Output/sketch.js");
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
                case NumArgumentException:
                    Assert.Equal(exceptionType, typeof(NumArgumentException));
                    break;
                case UnknownBuiltinException:
                    Assert.Equal(exceptionType, typeof(UnknownBuiltinException));
                    break;
                case UndeclaredVariable:
                    Assert.Equal(exceptionType, typeof(UndeclaredVariable));
                    break;
                default:
                    Assert.Equal(new Exception("randomstuff"), actualException);
                    break;
            }
        }
    }
    [Theory]
    [ClassData(typeof(TrueTypeTestData))]
    public void TypeTestPass(string prog, string comment, Type exceptionType)
    {
        Prog.MyParseMethod(prog: prog, output: "../../../../ALFA/Output/sketch.js");
        Assert.True(true);
    }
}



