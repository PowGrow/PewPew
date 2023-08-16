namespace Pewpew.Logic.Inventory
{
    public class Item
    {
        public int Id { get; private set; }
        public int Quantity { get; set; }

        public Item(int id, int quantity = 1)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
