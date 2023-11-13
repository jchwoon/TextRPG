using EnumsNamespace;
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
        ItemList itemList = SpartaDungeon.json.GetJsonData();
        public Store() { }

        public void DisplayStore()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{SpartaDungeon.player1.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            List<Weapon> weaponList = itemList.Weapons;
            List<Armor> armorList = itemList.Armors;

            foreach (Weapon weapon in weaponList)
            {
                Console.WriteLine($"- {weapon.Name}  |  공격력 +{weapon.Damage}  |  {weapon.Description}  | {weapon.Gold}골드");
            }
            foreach (Armor armor in armorList)
            {
                Console.WriteLine($"- {armor.Name}  |  방어력 +{armor.Defence}  |  {armor.Description}  |  {armor.Gold}골드");
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");
            int input = SpartaDungeon.CheckValidInput(0, weaponList.Count + armorList.Count);

            switch(input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro();
                    break;
                case 1:
                    DisplayPurchaseItemList();
                    break;
                case 2:
                    DisplaySellItemList();
                    break;
            }
            PurchaseItem(input);
        }

        public void DisplayPurchaseItemList()
        {
            Console.Clear();
            Console.WriteLine("상점 -  아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{SpartaDungeon.player1.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            int count = 1;
            List<Weapon> weaponList = itemList.Weapons;
            List<Armor> armorList = itemList.Armors;

            int itemTotalLength = weaponList.Count + armorList.Count;

            foreach (Weapon weapon in weaponList)
            {
                Console.WriteLine($"{count++} {weapon.Name}  |  공격력 +{weapon.Damage}  |  {weapon.Description}  | {weapon.Gold}골드");
            }
            foreach (Armor armor in armorList)
            {
                Console.WriteLine($"{count++} {armor.Name}  |  방어력 +{armor.Defence}  |  {armor.Description}  |  {armor.Gold}골드");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("구매하실 아이템 번호를 입력해주세요");
            Console.Write(">>");
            int input = SpartaDungeon.CheckValidInput(0, itemTotalLength);

            if (input == 0)
            {
                DisplayStore();
            } 
            else if (input >= 1 && input <= itemTotalLength)
            {
                if (input <= weaponList.Count)
                {
                    if (weaponList[input - 1].Gold > SpartaDungeon.player1.Gold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                        Thread.Sleep(1000);
                        DisplayStore();
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        SpartaDungeon.player1.Gold -= weaponList[input - 1].Gold;
                        SpartaDungeon.player1.Inventory.AddItem(weaponList[input - 1]);
                        Thread.Sleep(1000);
                        DisplayStore();
                    }
                }
                else
                {
                    if (armorList[input - 1 - weaponList.Count].Gold > SpartaDungeon.player1.Gold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                        Thread.Sleep(1000);
                        DisplayStore();
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        SpartaDungeon.player1.Gold -= armorList[input - 1].Gold;
                        SpartaDungeon.player1.Inventory.AddItem(armorList[input - 1]);
                        Thread.Sleep(1000);
                        DisplayStore();
                    }
                }
            }
        }

        public void DisplaySellItemList()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{SpartaDungeon.player1.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
            int numCount = 1;
            bool hasSellItem = false;
            Item?[] itemArray = SpartaDungeon.player1.Inventory.Items;

            float percentage = 0.85f;

            foreach (Item item in itemArray)
            {
                if (item == null) continue;

                hasSellItem = true;

                if (item.ItemType == ItemType.IT_Weapon)
                {
                    Weapon weapon = (Weapon)item;
                    Console.WriteLine($"{numCount++} {weapon.Name}  |  공격력 +{weapon.Damage}  |  {weapon.Description}  | {Math.Ceiling(weapon.Gold * percentage)}골드");
                }
                else
                {
                    Armor armor = (Armor)item;
                    Console.WriteLine($"{numCount++} {armor.Name}  |  방어력 +{armor.Defence}  |  {armor.Description}  | {Math.Ceiling(armor.Gold * percentage)}골드");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.Write(">>");

            if (!hasSellItem) Console.WriteLine("판매할 아이템이 없습니다.");
            else
            {
                Console.WriteLine("판매하실 아이템 번호를 입력해주세요");
            }
            

            int input = SpartaDungeon.CheckValidInput(0, numCount-1);
            

            if (input == 0)
            {
                DisplayStore();
            }
            else if (input >= 1 && input <= numCount-1)
            {
                SpartaDungeon.player1.Gold += itemArray[input-1].Gold;
                SpartaDungeon.player1.Inventory.RemoveItem(itemArray[input-1]);
                SpartaDungeon.player1.UpdateStatInfo();
                Console.WriteLine("판매되었습니다.");
                Thread.Sleep(1000);
                DisplayStore();
            }
        }
        public void PurchaseItem(int input)
        {
            ItemList itemList = SpartaDungeon.json.GetJsonData();

            List<Weapon> weaponList = itemList.Weapons;
            List<Armor> armorList = itemList.Armors;

            if (input == 0) SpartaDungeon.DisplayGameIntro();
            else if (input >= 1 && input <= weaponList.Count + armorList.Count)
            {
                if (input <= weaponList.Count)
                {
                    if (weaponList[input - 1].Gold > SpartaDungeon.player1.Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Thread.Sleep(2000);
                        DisplayStore();
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        SpartaDungeon.player1.Gold -= weaponList[input - 1].Gold;
                        SpartaDungeon.player1.Inventory.AddItem(weaponList[input - 1]);
                        Thread.Sleep(2000);
                        DisplayStore();
                    }
                }
                else
                {
                    if (armorList[input - 1 - weaponList.Count].Gold > SpartaDungeon.player1.Gold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                        Thread.Sleep(2000);
                        DisplayStore();
                    }
                    else
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        SpartaDungeon.player1.Gold -= armorList[input - 1].Gold;
                        SpartaDungeon.player1.Inventory.AddItem(armorList[input - 1]);
                        Thread.Sleep(2000);
                        DisplayStore();
                    }
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(2000);
                DisplayStore();
            }
        }
    }
}
