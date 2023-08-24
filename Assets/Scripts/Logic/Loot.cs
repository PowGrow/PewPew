using Pewpew.Logic.Inventory;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private Item _item;
    private bool _isActive = false;

    public void SetItem(Item item)
    {
        _item = item;
        _isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive)
            return;

        PlayerInventory playerInventory;
        if(other.TryGetComponent(out playerInventory))
        {
            Debug.Log($"TO ADD: {_item.Id}: {_item.Quantity}");
            _item.Quantity = playerInventory.Cargo.TryToAddItem(_item);
            Debug.Log($"ADDED : {_item.Id}: {_item.Quantity}");
            Debug.Log($"SPACE LEFT: {playerInventory.Cargo.Size - playerInventory.Cargo.Items.Count}");
        }

        if (_item.Quantity <= 0)
            Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
