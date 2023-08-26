using Pewpew.Infrastructure.AssetManagment;
using Pewpew.Logic.Inventory;
using System.IO;
using UnityEngine;

namespace Pewpew.Infrastructure.Services.Inventory
{
    public class ItemsInfoService : IItemsInfoService
    {
        public Items Items { get; private set; }

        public ItemsInfoService()
        {
            Items = LoadItems();
        }

        private Items LoadItems()
        {
            var savePath = FileNameCombine(AssetPath.ItemsDataPath);
            var itemsSerializable = JsonUtility.FromJson<ItemsSerializable>(File.ReadAllText(savePath));
            return new Items(itemsSerializable.Items);
        }

        private string FileNameCombine(string path)
        {
            var resultPath = Path.Combine(Application.persistentDataPath, path);
            return resultPath;
        }
    }
}
