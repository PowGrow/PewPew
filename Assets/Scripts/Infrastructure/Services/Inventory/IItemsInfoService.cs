using Pewpew.Infrastructure.Services;
using Pewpew.Logic.Inventory;

namespace Pewpew.Infrastructure.Services.Inventory
{
    public interface IItemsInfoService : IService
    {
        Items ItemsInfo { get; }
    }
}
