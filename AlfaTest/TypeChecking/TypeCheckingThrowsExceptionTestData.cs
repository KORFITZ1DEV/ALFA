using Newtonsoft.Json;

namespace AlfaTest.TypeChecking;

public class TypeCheckingThrowsExceptionTestData : JsonTestData
{
    private string _path = "../../../TypeChecking/TypeCheckingThrowsException.json";
    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    }    
}