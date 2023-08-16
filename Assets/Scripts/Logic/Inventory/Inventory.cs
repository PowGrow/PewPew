
using Assets.Scripts.Logic.Inventory;
using Pewpew.Logic.Inventory;
using System;
using System.Collections.Generic;

namespace PewPew.Scripts.Logic.Inventory
{
    public class Inventory
    {
        public uint Size { get; private set; }
        public List<Item> Items { get; private set; }

        private Items _items;

        public Inventory(Items itemsData, uint size) 
        {
            _items = itemsData;
            Items = new List<Item>();
            Size = size;
        }

        public bool AddItem(Item item)
        {
            var itemInfo = _items.GetItemInfo(item.Id);
            if (!itemInfo.IsStackable)
            {
                if (Items.Count < Size)
                    Items.Add(item);
                else
                    return false;
                return true;
            }

            var quantityToAdd = item.Quantity;

            foreach (Item itemInInventory in Items)
            {
                if(itemInInventory.Id ==  item.Id)
                {
                    var allowToAdd = itemInfo.StackSize - itemInInventory.Quantity;
                    if(allowToAdd >= quantityToAdd)

                    itemInInventory.Quantity += allowToAdd;
                    quantityToAdd -= allowToAdd;
                }
            }
            item.Quantity = quantityToAdd;

            return true;
        }

        public Item GetItem(int itemId, int quantity)
        {
            if(_items.GetItemInfo(itemId).StackSize < quantity)
            {
                throw new ArgumentOutOfRangeException($"Item: {itemId}, Incorrect quantity size to get");
            }

            Item item = null;
            var quantityToGet = quantity;
            for (int index = Items.Count; index > 0; index--)
            {
                if (Items[index].Id == itemId)
                {
                    if (quantityToGet == 0)
                        break;

                    if (IsQuantityLessOrEqualNeeded(quantityToGet, index))
                    {
                        if (item == null)
                            item = Items[index];
                        else
                            item.Quantity += Items[index].Quantity;
                        quantityToGet -= item.Quantity;
                        Items.Remove(Items[index]);
                        continue;
                    }

                    if (IsQuantityMoreThanNeeded(quantityToGet, index))
                    {
                        if (item == null)
                            item = Items[index];
                        item.Quantity += quantityToGet;
                        Items[index].Quantity -= quantityToGet;
                        quantityToGet = 0;
                    }
                }
            }
            return item;
        }

        public List<Item> GetItems(int itemId, int quantity)
        {
            var items = new List<Item>();
            var itemInfo = _items.GetItemInfo(itemId);

            (int stacksQuantity, int remainderQuantity) needed = GetStacksAndRemainderNeeded(quantity, itemInfo.StackSize);

            for(int index = 0; index < needed.stacksQuantity; index++)
                items.Add(GetItem(itemId, itemInfo.StackSize));

            items.Add(GetItem(itemId, needed.remainderQuantity));
            
            return items;
        }

        private (int stacksQuantity, int remainderQuantity) GetStacksAndRemainderNeeded(int quantityToGet, int itemStackSize)
        {
            return (quantityToGet / itemStackSize, quantityToGet % itemStackSize);
        }

        private bool IsQuantityMoreThanNeeded(int neededQuantity, int index)
        {
            return Items[index].Quantity > neededQuantity;
        }

        private bool IsQuantityLessOrEqualNeeded(int neededQuantity, int index)
        {
            return Items[index].Quantity <= neededQuantity;
        }
    }
}
