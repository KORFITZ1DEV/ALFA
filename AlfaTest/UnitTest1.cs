namespace AlfaTest;

public class UnitTest1
{
    [Theory]
    [InlineData(ast1)]
    public void TestBuildAST()
    {
        AssertEqual(ast1, ast2);
    }
}



