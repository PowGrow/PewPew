using Pewpew.Logic.Inventory;
using System;
using System.Collections.Generic;

[Serializable]
public class ItemsSerializable
{
    public List<ItemInfo> Items;

    public ItemsSerializable(List<ItemInfo> items)
    {
        Items = items;
    }
}
