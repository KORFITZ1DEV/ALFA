using System.Collections;

namespace AlfaTest.Parser;

public class ParserTest
{

}

public class ParserTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { item.Prog, item.Comment, exceptionType };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}