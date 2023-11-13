using EnumsNamespace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{

    public class Inventory
    {
        public static int MaxSize = 20;
        public Inventory() 
        {
            for (int i = 0; i < MaxSize; i++)
                _items[i] = null;
        }

        public bool UseItem(int slot)
        {
            if (_items[slot] == null)
            {
                Console.WriteLine("해당 슬롯은 비어 있습니다.");
                Console.WriteLine("다시 입력해주세요:");
                return false;
            }

            if (_items[slot].ItemType == ItemType.IT_Weapon)
            {
                Weapon weapon = (Weapon)_items[slot];
                if (SpartaDungeon.weaponJobMapping[weapon.WeaponType] != SpartaDungeon.player1.JobType)
                {
                    Console.WriteLine("해당 무기는 착용할 수 없습니다. 직업에 맞는 무기를 착용해주세요");
                    return false;
                }
            }

            if (_items[slot].ItemType == ItemType.IT_Weapon || _items[slot].ItemType == ItemType.IT_Armor)
            {
                //나의 인벤토리에서 착용중인 아이템의 타입을 갖고와서 매개변수로 들어온 아이템과 비교
                //타입이 일치하면 착용중인 아이템을 ToggleEquipItem을 통해서 해제
                //매개변수로 들어온 아이템(장착하려는 아이템) 착용
                foreach(Item item in SpartaDungeon.player1.Inventory.Items)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    if (item.IsEquip == true && item.ItemType == _items[slot].ItemType && item.Name != _items[slot].Name)
                    {
                        ToggleEquipItem(item);
                    }
                }
                ToggleEquipItem(_items[slot]);
            }

            return true;
        }


        public bool AddItem(Item item)
        {
            if (_currentSize >= MaxSize)
            {
                Console.WriteLine("인벤토리가 가득 찼습니다.");
                return false;
            }

            _items[_currentSize] = item;
            _currentSize++;

            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (item == null)
                return false;

            int slot = FindItemSlot(item);
            if (slot < 0)
                return false;

            _items[slot] = null;
            _currentSize--;

            return true;
        }

        public void SetInitialItem(JobType jobType)
        {
            Item initialWeapon;
            Item initialArmor;

            ItemList itemList = SpartaDungeon.json.GetJsonData();

            switch (jobType)
            {
                case JobType.JT_Warrior:
                    Weapon warriorInitItem = itemList.Weapons[0];
                    initialWeapon = new Weapon(warriorInitItem.Name, warriorInitItem.Description, warriorInitItem.Damage, 1, WeaponType.WT_Sword);
                    break;
                case JobType.JT_Mage:
                    Weapon mageInitItem = itemList.Weapons[1];
                    initialWeapon = new Weapon(mageInitItem.Name, mageInitItem.Description, mageInitItem.Damage, 1, WeaponType.WT_Wand);
                    break;
                case JobType.JT_Thief:
                    Weapon thiefInitItem = itemList.Weapons[2];
                    initialWeapon = new Weapon(thiefInitItem.Name, thiefInitItem.Description, thiefInitItem.Damage, 1, WeaponType.WT_Knuckle);
                    break;
                default:
                    Weapon archerInitItem = itemList.Weapons[3];
                    initialWeapon = new Weapon(archerInitItem.Name, archerInitItem.Description, archerInitItem.Damage, 1, WeaponType.WT_Arrow);
                    break;
            }
            Armor initArmorData = itemList.Armors[0];
            initialArmor = new Armor(initArmorData.Name, initArmorData.Description, initArmorData.Defence, 1);

            AddItem(initialWeapon);
            AddItem(initialArmor);
        }
        private void ToggleEquipItem(Item item)
        {
            if (item.IsEquip)
            {
                item.IsEquip = false;
                Console.Clear();
            }
            else
            {
                item.IsEquip = true;
                Console.Clear();
            }
        }

        private int FindItemSlot(Item item)
        {
            for (int i = 0; i < MaxSize; i++)
            {
                if (_items[i] == item)
                    return i;
            }

            return -1;
        }

        public Item?[] Items
        {
            get { return _items;}
            set {  _items = value; }
        }
        
        private Item?[] _items = new Item[MaxSize];
        private int _currentSize = 0;
    }
}
