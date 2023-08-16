namespace Pewpew.Logic.Inventory
{
    public abstract class Item
    {
        public int Id { get; private set; }
        public int Quantity { get; set; }

        public Item(int id, uint quantity = 1)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
