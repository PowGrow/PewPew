using Pewpew.Logic.Inventory;

namespace Pewpew.Infrastructure.Services.Inventory
{
    public interface IItemsInfoService : IService
    {
        Items Items { get; }
    }
}
