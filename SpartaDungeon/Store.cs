using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Store
    {
        public Store() { }

        public void DisplayStore(Player player)
        {
            ItemList itemList = SpartaDungeon.json.GetJsonData();

            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            int count = 1;
            List<Weapon> weaponList = itemList.Weapons;
            List<Armor> armorList = itemList.Armors;

            foreach (Weapon weapon in weaponList)
            {
                Console.WriteLine($"{count++} {weapon.Name}  |  공격력 +{weapon.Damage}  |  {weapon.Description}");
            }
            foreach (Armor armor in armorList)
            {
                Console.WriteLine($"{count++} {armor.Name}  |  방어력 +{armor.Defence}  |  {armor.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("구매할 아이템의 번호를 입력해주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine(">>");
            int input = SpartaDungeon.CheckValidInput(0, weaponList.Count + armorList.Count);
            PurchaseItem(player, input);
        }
        public void PurchaseItem(Player player, int input)
        {
            ItemList itemList = SpartaDungeon.json.GetJsonData();

            List<Weapon> weaponList = itemList.Weapons;
            List<Armor> armorList = itemList.Armors;

            if (input == 0) SpartaDungeon.DisplayGameIntro(player);
            else if (input >= 1 && input <= weaponList.Count + armorList.Count)
            {
                if (input <= weaponList.Count)
                {
                    if (weaponList[input - 1].Gold > player.Gold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                        Thread.Sleep(2000);
                        DisplayStore(player);
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        player.Gold -= weaponList[input - 1].Gold;
                        player.Inventory.AddItem(weaponList[input - 1]);
                        Thread.Sleep(2000);
                        DisplayStore(player);
                    }
                }
                else
                {
                    if (armorList[input - 1 - weaponList.Count].Gold > player.Gold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                        Thread.Sleep(2000);
                        DisplayStore(player);
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        player.Gold -= armorList[input - 1].Gold;
                        player.Inventory.AddItem(armorList[input - 1]);
                        Thread.Sleep(2000);
                        DisplayStore(player);
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(2000);
                DisplayStore(player);
            }
        }
    }
}
