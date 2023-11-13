using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnumsNamespace;

namespace SpartaDungeon
{
    public class Item
    {
        public Item(ItemType itemType, string name, string description)
        {
            _itemType = itemType;
            _name = name;
            _description = description;
        }

        public string Description
        {
            get { return _description; } 
            set { _description = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ItemType ItemType 
        { 
            get { return _itemType; } 
        }

        public bool IsEquip
        {
            get { return _isEquip; }
            set { _isEquip = value; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold = value;}
        }

        private string _description = "";
        private string _name;
        private ItemType _itemType;
        private bool _isEquip = false;
        private int _gold = 0;
    }

    public class Weapon : Item
    {
        public Weapon(string name, string description, int damage)
            :base(ItemType.IT_Weapon, name, description)
        {
            _damage = damage;
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        private int _damage;
    }

    public class Armor : Item
    {
        public Armor(string name, string description, int defence) 
            :base(ItemType.IT_Armor, name, description)
        {
            _defence = defence;
        }

        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }

        private int _defence;
    }
}
