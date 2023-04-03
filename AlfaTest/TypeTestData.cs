using System.Collections;
using System.Text.Json.Serialization;
using ALFA;
using Newtonsoft.Json;

namespace AlfaTest;

    public abstract class TypeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
           var items = LoadJson();
           Type exceptionType = default;

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
                    case "NumArgumentException":
                        exceptionType = typeof(NumArgumentException);
                        break;
                    case "UnknownBuiltinException":
                        exceptionType = typeof(UnknownBuiltinException);
                        break;
                    case "UndeclaredVariable":
                        exceptionType = typeof(UndeclaredVariable);
                        break;
                }
                yield return new object[] {item.Prog, item.Comment, exceptionType};
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
public class TrueTypeTestData : TypeTestData
{
    private string truePath = "../../../TestProgTrue.json";
    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(truePath));
    }
}
public class FalseTypeTestData : TypeTestData
{
    private string falsePath = "../../../TestProgException.json";
     public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(falsePath));
    }    
}
