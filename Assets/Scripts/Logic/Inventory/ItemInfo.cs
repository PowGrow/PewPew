using System;
using UnityEngine;

namespace Pewpew.Logic.Inventory
{
    [Serializable]
    public class ItemInfo
    {
        [field:SerializeField]
        public int Id { get; private set; }
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public string Sprite { get; private set; }
        [field: SerializeField]
        public bool IsStackable { get; private set; }
        [field: SerializeField]
        public int StackSize { get; private set; }

        public ItemInfo(int id, string name, string description, string sprite, bool isStackable = false, int stackSize = 1)
        {
            Id = id;
            Name = name;
            Description = description;
            Sprite = sprite;
            IsStackable = isStackable;
            StackSize = stackSize;

            CheckAndCorrectStackSize(isStackable, stackSize);
        }

        private void CheckAndCorrectStackSize(bool isStackable, int stackSize)
        {
            if (!isStackable && stackSize > 1)
                StackSize = 1;
        }
    }
}
