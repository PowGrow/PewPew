using System;
using System.Collections.Generic;

namespace Pewpew.Logic.Inventory
{
    public class Inventory
    {
        public int Size { get; private set; }
        public List<Item> Items { get; private set; }

        private Items _itemsInfo;

        public Inventory(Items itemsInfo, int size) 
        {
            _itemsInfo = itemsInfo;
            Items = new List<Item>();
            Size = size;
        }
        /// <summary>
        /// Method trying to add item in to inventory
        /// </summary>
        /// <param name="item"> Item to add </param>
        /// <returns>Int value represent remainder of item quantity in stack, value othen than zero mean there is no more space in inventory to add, and remainder quantity of item was returned</returns>
        public int TryToAddItem(Item item)
        {
            var itemInfo = _itemsInfo.GetItemInfo(item.Id);
            if (!itemInfo.IsStackable)
            {
                if (Items.Count < Size)
                    Items.Add(item);
                else
                    return item.Quantity;
                return 0;
            }

            if (Items.Count == 0)
            {
                Items.Add(new Item(item.Id,item.Quantity));
                return 0;
            }

            foreach (Item itemInInventory in Items)
            {
                if (item.Quantity == 0)
                    break;

                if(itemInInventory.Id ==  item.Id)
                {
                    var allowToAdd = itemInfo.StackSize - itemInInventory.Quantity;
                    if (allowToAdd >= item.Quantity)
                    {
                        itemInInventory.Quantity += item.Quantity;
                        item.Quantity = 0;
                        break;
                    }

                    itemInInventory.Quantity += allowToAdd;
                    item.Quantity -= allowToAdd;
                }
            }
            if (item.Quantity > 0 && Items.Count == Size)
                return item.Quantity;
            if(item.Quantity > 0)
                Items.Add(new Item(item.Id, item.Quantity));
            return 0;
        }

        public Item GetItem(int itemId, int quantity)
        {
            if (_itemsInfo.GetItemInfo(itemId).StackSize < quantity)
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
            var itemInfo = _itemsInfo.GetItemInfo(itemId);

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
