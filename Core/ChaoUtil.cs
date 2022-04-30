using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    public static class ChaoUtil
    {
        public static void AddChaoInShop(ref Chest shop, ref int nextSlot, string type, Func<string, int> priceCost, Func<string, bool> condition, params string[] exceptions)
        {
            List<Item> inList = new();
            ChaoGardenMod.GetModItems.FindAll(x =>
            {
                bool bl = true;
                foreach (string exception in exceptions)
                {
                    bl &= !x.Name.Contains(exception);
                }
                return x.Name.Contains(type) && bl;
            }).ForEach(new Action<ModItem>((item) => inList.AddWithCondition(item.Item, condition.Invoke(item.Name))));
            foreach (Item item in inList)
            {
                shop.item[nextSlot].SetDefaults(item.type);
                shop.item[nextSlot].shopCustomPrice = priceCost.Invoke(item.Name);
                nextSlot++;
            }
        }

        public static void AddWithCondition<T>(this List<T> list, T content, bool condition)
        {
            if (condition)
            {
                list.Add(content);
            }
        }
    }
}
