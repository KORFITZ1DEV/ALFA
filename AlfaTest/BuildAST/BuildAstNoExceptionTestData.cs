using Newtonsoft.Json;

namespace AlfaTest.BuildAST;

public class BuildAstNoExceptionTestData : JsonTestData
{
    private string _path = "../../../BuildAST/BuildAstNoException.json";
    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    } 
}
