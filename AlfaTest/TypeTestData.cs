using System.Collections;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfaTest;

    public class TypeTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
           var items = LoadJson();
            foreach (var item in items)
            {
                yield return new object[] {item.Prog, item.Comment};
            } 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Item[] LoadJson()
        {
            return JsonConvert.DeserializeObject<Item[]>(File.ReadAllText(TypeVisitorTest.path));
        }
        
        [Serializable]
        public class Item
        {
            public string Prog;
            public string Comment;
        }
    }
