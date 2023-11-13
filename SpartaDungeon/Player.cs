using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EnumsNamespace;

namespace SpartaDungeon
{
    //******************************//
    //            Player            //
    //******************************//
    public abstract class Player
    {
        public Player(string name)
        { 
            _name = name;
        }

        public abstract void DisplayMyInfo();
        public abstract void UpdateStatInfo();

        public void DisplayItemList()
        {
            Item?[] Items = Inventory.Items;

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                {
                    Console.WriteLine($"slot{i + 1}: 빈 슬롯");
                }
                else
                {
                    switch (Items[i]?.ItemType)
                    {
                        case ItemType.IT_Weapon:
                            if (Items[i] is Weapon weaponItem)
                            {
                                if (Items[i].IsEquip)
                                {
                                    Console.WriteLine($"slot{i + 1}: [E]{weaponItem.Name}  | 공격력 +{weaponItem.Damage} | {weaponItem.Description}");
                                }
                                else
                                {
                                    Console.WriteLine($"slot{i + 1}: {weaponItem.Name}  | 공격력 +{weaponItem.Damage} | {weaponItem.Description}");
                                }
                            }
                            break;
                        case ItemType.IT_Armor:
                            if (Items[i] is Armor armorItem)
                            {
                                if (Items[i].IsEquip)
                                {
                                    Console.WriteLine($"slot{i + 1}: [E]{armorItem.Name}  | 방어력 +{armorItem.Defence} | {armorItem.Description}");
                                }
                                else
                                {
                                    Console.WriteLine($"slot{i + 1}: {armorItem.Name}  | 방어력 +{armorItem.Defence} | {armorItem.Description}");
                                }
                            }
                            break;
                    }
                }
            }
        }

        public void SelectItem()
        {
            Console.WriteLine("장착 or 사용할 아이템 슬롯의 번호를 입력해주세요:\n");

            int input;

            input = SpartaDungeon.CheckValidInput(1, 20);
            bool isUse = _inventory.UseItem(input-1);
            while (!isUse)
            {
                input = SpartaDungeon.CheckValidInput(1, 20);
                isUse = _inventory.UseItem(input-1);
            }

            UpdateStatInfo();

            DisplayEquipManage();
        }
        public void DisplayEquipManage()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]\n");
            DisplayItemList();

            Console.WriteLine("1. 아이템 장착 or 사용하기");
            Console.WriteLine("0. 나가기\n");

            int input = SpartaDungeon.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                    SelectItem();
                    break;
            }
        }
        public void DisplayInventory()
        {
            Console.Clear();
            

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            DisplayItemList();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = SpartaDungeon.CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro(this);
                    break;
                case 1:
                    DisplayEquipManage();
                    break;
            }
        }

        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public JobType JobType
        {
            get { return _job; }
            set { _job = value; }
        }
        public Item[] EquipmentWeaponArray
        {
            get { return _equipmentWeaponArray; }
            set { }
        }

        private string _name;
        private int _level = 1;
        private int _gold = 1500;
        private Inventory _inventory =  new Inventory();
        private JobType _job;
        private Item[] _equipmentWeaponArray = new Item[2];
        private Item[] _equipmentArmorArray = new Item[5];
    }

    //******************************//
    //            Warrior            //
    //******************************//
    public class Warrior : Player
    {
        public Warrior(string name)
            : base(name)
        {
            JobType = JobType.JT_Warrior;
        }
        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}( 전사 )");
            int addAttack = _attack - _initialAttack;
            Console.WriteLine($"공격력: {_attack}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = _defence - _initialDefence;
            Console.WriteLine($"방어력: {_defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {_hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = SpartaDungeon.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro(this);
                    break;
            }
        }

        public override void UpdateStatInfo()
        {
            _attack = _initialAttack;
            _defence = _initialDefence;

            foreach (Item item in Inventory.Items)
            {
                if (item == null) return;

                if (item.IsEquip)
                {
                    if (item.ItemType == ItemType.IT_Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        _attack += weapon.Damage;
                    }
                    if (item.ItemType == ItemType.IT_Armor)
                    {
                        Armor armor = (Armor)item;
                        _defence += armor.Defence;
                    }
                }
            }
        }

        private const int _initialAttack = 10;
        private const int _initialDefence = 20;
        private int _hp = 150;
        private int _attack = 10;
        private int _maxHp = 150;
        private int _defence = 20;   
    }

    //******************************//
    //            Mage            //
    //******************************//
    public class Mage : Player
    {
        public Mage(string name)
            : base(name)
        {
            JobType = JobType.JT_Mage;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}( 마법사 )");
            int addAttack = _attack - _initialAttack;
            Console.WriteLine($"공격력: {_attack}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = _defence - _initialDefence;
            Console.WriteLine($"방어력: {_defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {_hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = SpartaDungeon.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro(this);
                    break;
            }
        }
        public override void UpdateStatInfo()
        {
            _attack = _initialAttack;
            _defence = _initialDefence;

            foreach (Item item in Inventory.Items)
            {
                if (item == null) return;

                if (item.IsEquip)
                {
                    if (item.ItemType == ItemType.IT_Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        _attack += weapon.Damage;
                    }
                    if (item.ItemType == ItemType.IT_Armor)
                    {
                        Armor armor = (Armor)item;
                        _defence += armor.Defence;
                    }
                }
            }
        }

        private const int _initialAttack = 19;
        private const int _initialDefence = 4;
        private int _hp = 90;
        private int _attack = 19;
        private int _maxHp = 90;
        private int _defence = 4;
    }

    //******************************//
    //            Thief            //
    //******************************//
    public class Thief : Player
    {
        public Thief(string name)
            : base(name)
        {
            JobType = JobType.JT_Thief;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}( 도적 )");
            int addAttack = _attack - _initialAttack;
            Console.WriteLine($"공격력: {_attack}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = _defence - _initialDefence;
            Console.WriteLine($"방어력: {_defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {_hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = SpartaDungeon.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro(this);
                    break;
            }
        }
        public override void UpdateStatInfo()
        {
            _attack = _initialAttack;
            _defence = _initialDefence;

            foreach (Item item in Inventory.Items)
            {
                if (item == null) return;

                if (item.IsEquip)
                {
                    if (item.ItemType == ItemType.IT_Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        _attack += weapon.Damage;
                    }
                    if (item.ItemType == ItemType.IT_Armor)
                    {
                        Armor armor = (Armor)item;
                        _defence += armor.Defence;
                    }
                }
            }
        }

        private const int _initialAttack = 21;
        private const int _initialDefence = 2;
        private int _hp = 80;
        private int _attack = 21;
        private int _maxHp = 80;
        private int _defence = 2;
    }

    //******************************//
    //            Archer            //
    //******************************//
    public class Archer : Player
    {
        public Archer(string name)
            : base(name)
        {
            JobType = JobType.JT_Archer;
        }

        public override void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Level}");
            Console.WriteLine($"{Name}( 궁수 )");
            int addAttack = _attack - _initialAttack;
            Console.WriteLine($"공격력: {_attack}" + (addAttack != 0 ? $" (+{addAttack})" : ""));
            int addDefence = _defence - _initialDefence;
            Console.WriteLine($"방어력: {_defence}" + (addDefence != 0 ? $" (+{addDefence})" : ""));
            Console.WriteLine($"체력: {_hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = SpartaDungeon.CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    SpartaDungeon.DisplayGameIntro(this);
                    break;
            }
        }
        public override void UpdateStatInfo()
        {
            _attack = _initialAttack;
            _defence = _initialDefence;

            foreach (Item item in Inventory.Items)
            {
                if (item == null) return;

                if (item.IsEquip)
                {
                    if (item.ItemType == ItemType.IT_Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        _attack += weapon.Damage;
                    }
                    if (item.ItemType == ItemType.IT_Armor)
                    {
                        Armor armor = (Armor)item;
                        _defence += armor.Defence;
                    }
                }
            }
        }

        private const int _initialAttack = 15;
        private const int _initialDefence = 4;
        private int _hp = 100;
        private int _attack = 15;
        private int _maxHp = 100;
        private int _defence = 4;
    }
}
