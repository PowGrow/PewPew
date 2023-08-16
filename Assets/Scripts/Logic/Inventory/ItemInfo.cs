namespace Pewpew.Logic.Inventory
{
    public class ItemInfo
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsStackable { get; private set; }
        public int StackSize { get; private set; }

        public ItemInfo(int id, string name, string description, bool isStackable = false, int stackSize = 1)
        {
            Id = id;
            Name = name;
            Description = description;
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
