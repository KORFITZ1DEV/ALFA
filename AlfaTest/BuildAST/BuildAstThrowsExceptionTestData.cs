using Newtonsoft.Json;

namespace AlfaTest.BuildAST;

public class BuildAstThrowsExceptionTestData : JsonTestData
{
    private string _path = "../../../BuildAST/BuildAstThrowsException.json";
    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    } 
}
