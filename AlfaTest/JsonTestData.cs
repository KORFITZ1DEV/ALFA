using System.Collections;
using ALFA;

namespace AlfaTest;

public abstract class JsonTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
       var items = LoadJson();
       Type? exceptionType = default;

        foreach (var item in items)
        {
            switch(item.ExceptionType)
            {
                case "TypeException":
                    exceptionType = typeof(TypeException);
                    break;
                case "ArgumentTypeException":
                    exceptionType = typeof(ArgumentTypeException);
                    break;
                case "InvalidNumberOfArgumentsException":
                    exceptionType = typeof(InvalidNumberOfArgumentsException);
                    break;
                case "UnknownBuiltinException":
                    exceptionType = typeof(UnknownBuiltinException);
                    break;
                case "UndeclaredVariableException":
                    exceptionType = typeof(UndeclaredVariableException);
                    break;
            }

            yield return new object[] { item.Prog, item.Comment, exceptionType };
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public abstract Item[] LoadJson();
    
    [Serializable]
    public class Item
    {
        public string Prog;
        public string Comment;
        public string ExceptionType;
    }
}