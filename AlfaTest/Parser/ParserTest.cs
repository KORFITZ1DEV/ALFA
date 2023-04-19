using System.Collections;

namespace AlfaTest.Parser;

public class ParserTest
{
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
        yield return new object[] {"2 plus 7"};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}