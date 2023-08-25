using Pewpew.Logic.Inventory;

namespace Pewpew.Infrastructure.Services.Inventory
{
    public class ItemsInfoService : IItemsInfoService
    {
        public Items ItemsInfo { get; private set; }

        public ItemsInfoService(Items _items)
        {
            ItemsInfo = _items;
        }
    }
}
