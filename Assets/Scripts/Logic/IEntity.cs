using Pewpew.Logic.Loot;

namespace Pewpew.Logic
{
    public interface IEntity
    {
        public void AddLoot(Drop chancesToDrop);
    }
}
