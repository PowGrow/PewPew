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

        Player player;
        if(other.TryGetComponent(out player))
        {
            Debug.Log($"TO ADD: {_item.Id}: {_item.Quantity}");
            _item.Quantity = player.Cargo.TryToAddItem(_item);
            Debug.Log($"ADDED : {_item.Id}: {_item.Quantity}");
            Debug.Log($"SPACE LEFT: {player.Cargo.Size - player.Cargo.Items.Count}");
        }

        if (_item.Quantity <= 0)
            Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
