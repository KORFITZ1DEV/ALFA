using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;


namespace AlfaTest;

public class UnitTest1
{
    static string path = "AlfaTest/AlfaTestProgForType.json.";
    [Theory]
    [ClassData(typeof(TypeVisitorTestData))]
        public void TypeVisitorTest(string prog, string comment)
        {
        }
    public class TypeVisitorTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                List<Item> items = LoadJson();
                foreach (var item in items)
                {
                    yield return new object[] {item.Prog, item.Comment};
                } ;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

        public List<Item> LoadJson()
        {
            using (StreamReader r = new StreamReader("path"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Item>>(json);
            }
        }
    
        public class Item
        {
            public string Prog;
            public string Comment;
        }
    }
}



