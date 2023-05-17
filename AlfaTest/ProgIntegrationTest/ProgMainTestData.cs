using Newtonsoft.Json;

namespace AlfaTest.ProgTest;


public class ProgMainNoExceptionTestData : JsonTestData
{
    private string _path = "../../../ProgIntegrationTest/ProgMainNoException.json";

    public override Item[] LoadJson()
    {
        return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(_path));
    }
}