using ALFA;

namespace AlfaTest;

public class TypeVisitorTest
{
    public static string path = "../../../AlfaTestProgForType.json";

    [Theory]
    [ClassData(typeof(TypeTestData))]
    public void TypeTestThrowException(string prog, string comment)
    {
        Assert.ThrowsAny<Exception>(() => Prog.MyParseMethod(prog: prog, output: "../../../../ALFA/Output/sketch.js"));
    }
}



