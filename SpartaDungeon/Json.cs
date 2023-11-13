using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class ItemList
    {
        public List<Weapon> Weapons { get; set; }
        public List<Armor> Armors { get; set; }
    }

    public class Json
    {
        public ItemList GetJsonData()
        {
            string filePath = @"D:\jchwoon\CSGrammar\Item.json";

            // JSON 파일에서 데이터 읽어오기
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                ItemList itemList = JsonConvert.DeserializeObject<ItemList>(jsonData);

                return itemList;
            }
            else
            {
                    return null;
            }

        }
    }
}
