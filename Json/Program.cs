using System.Xml;
using Newtonsoft.Json;
using SpartaDungeon;


namespace Json
{
    public class ItemList
    {
        [JsonProperty("weapons")]
        public List<Weapon> Weapons { get; set; }

        [JsonProperty("armors")]
        public List<Armor> Armors { get; set; }

        public ItemList()
        {
            Weapons = new List<Weapon>();
            Armors = new List<Armor>();
        }
    }

    public class WeaponFactory
    {
        public static Weapon CreateWeapon(string name, string description, int damage, int gold)
        {
            return new Weapon(name, description, damage)
            {
                Name = name,
                Description = description,
                Damage = damage,
                Gold = gold
            };
        }
    }

    public class ArmorFactory
    {
        public static Armor CreateArmor(string name, string description, int defence, int gold)
        {
            return new Armor(name, description, defence)
            {
                Name = name,
                Description = description,
                Defence = defence,
                Gold = gold
            };
        }
    }
    class Program
    {
        static void Main()
        {
            // JSON 파일에 저장할 데이터 객체 생성
            Weapon item1 = WeaponFactory.CreateWeapon("낡은 검", "초심자의 검", 3, 500);
            Weapon item2 = WeaponFactory.CreateWeapon("낡은 완드", "초심자의 완드", 4, 500);
            Weapon item3 = WeaponFactory.CreateWeapon("낡은 너클", "초심자의 너클", 4, 500);
            Weapon item4 = WeaponFactory.CreateWeapon("낡은 활", "초심자의 활", 4, 500);

            Weapon item5 = WeaponFactory.CreateWeapon("청동 검", "어디선가 사용됐던거 같은 검입니다.", 9, 3500);
            Weapon item6 = WeaponFactory.CreateWeapon("청동 완드", "어디선가 사용됐던거 같은 완드입니다.", 10, 3500);
            Weapon item7 = WeaponFactory.CreateWeapon("청동 너클", "어디선가 사용됐던거 같은 너클입니다.", 12, 3500);
            Weapon item8 = WeaponFactory.CreateWeapon("청동 활", "어디선가 사용됐던거 같은 활입니다.", 10, 3500);

            Weapon item9 = WeaponFactory.CreateWeapon("스파르타 검", "스파르타의 전사들이 사용했다는 전설의 검입니다.", 18, 9999);
            Weapon item10 = WeaponFactory.CreateWeapon("스파르타 완드", "스파르타의 전사들이 사용했다는 전설의 완드입니다.", 22, 9999);
            Weapon item11 = WeaponFactory.CreateWeapon("스파르타 너클", "스파르타의 전사들이 사용했다는 전설의 너클입니다.", 24, 9999);
            Weapon item12 = WeaponFactory.CreateWeapon("스파르타 활", "스파르타의 전사들이 사용했다는 전설의 활입니다.", 20, 9999);

            Armor armor1 = ArmorFactory.CreateArmor("무쇠갑옷", "초심자의 갑옷", 5, 500);
            Armor armor2 = ArmorFactory.CreateArmor("청동갑옷", "어디선가 사용됐던거 같은 갑옷입니다.", 9,3500);
            Armor armor3 = ArmorFactory.CreateArmor("스파르타 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 16, 9999);


            ItemList itemList = new ItemList();
            itemList.Weapons.Add(item1);
            itemList.Weapons.Add(item2);
            itemList.Weapons.Add(item3);
            itemList.Weapons.Add(item4);
            itemList.Weapons.Add(item5);
            itemList.Weapons.Add(item6);
            itemList.Weapons.Add(item7);
            itemList.Weapons.Add(item8);
            itemList.Weapons.Add(item9);
            itemList.Weapons.Add(item10);
            itemList.Weapons.Add(item11);
            itemList.Weapons.Add(item12);

            itemList.Armors.Add(armor1);
            itemList.Armors.Add(armor2);
            itemList.Armors.Add(armor3);


            // JSON 파일 경로 지정
            string filePath = @"D:\jchwoon\CSGrammar\Item.json";

            // JSON 파일에 데이터 쓰기
            string jsonData = JsonConvert.SerializeObject(itemList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, jsonData);

            Console.WriteLine("JSON 파일이 생성되었습니다.");
        }
    }
}