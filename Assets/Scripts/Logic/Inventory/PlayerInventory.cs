using Pewpew.Logic.Inventory;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private int CargoSize = 20;
    public Inventory Cargo { get; private set; }

    public void Initialize(Items items)
    {
        Cargo = new Inventory(items, CargoSize);
    }
}
