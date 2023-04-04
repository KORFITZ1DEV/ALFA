using Newtonsoft.Json;

namespace AlfaTest.TypeChecking;

public class TypeCheckingNoExceptionTestData : JsonTestData
{
    private string _path = "../../../TypeChecking/TypeCheckingNoException.json";

    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    }
}