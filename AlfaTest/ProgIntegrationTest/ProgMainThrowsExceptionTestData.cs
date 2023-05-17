using Newtonsoft.Json;

namespace AlfaTest.ProgTest;


public class ProgMainThrowsExceptionTestData : JsonTestData
{
    private string _path = "../../../ProgIntegrationTest/ProgMainThrowsException.json";
    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    }    
}