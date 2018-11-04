using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewValley.Menus;
using System;

namespace HardwoodAtCarpentersShop
{
    public class HardwoodAtCarpenterShop : Mod
    {
        private const int woodItemId = 388;
        private const int hardwoodItemId = 709;
        // based on the price of wood, buy for 50, sell for 2, i choose 450 as hardwood sells for 15
        private const int hardwoodPrice = 450;

        public override void Entry(IModHelper helper)
        {
            MenuEvents.MenuChanged += Events_MenuChanged;
        }

        private void Events_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            if (e.NewMenu is ShopMenu currentShopMenu)
            {
                if (currentShopMenu.portraitPerson.Equals(StardewValley.Game1.getCharacterFromName("Robin", false)))
                {
                    Item hardwood = (Item) new StardewValley.Object(Vector2.Zero, hardwoodItemId, ShopMenu.infiniteStock);

                    IReflectedField<List<Item>> forSaleInformation = this.Helper.Reflection.GetField<List<Item>>(currentShopMenu, "forSale");
                    List<Item> itemsForSale = forSaleInformation.GetValue();
                    itemsForSale.Insert(GetWoodPosition(itemsForSale) + 1,hardwood);
                    forSaleInformation.SetValue(itemsForSale);

                    IReflectedField<Dictionary<Item, int[]>> inventoryInformation = this.Helper.Reflection.GetField<Dictionary<Item, int[]>>(currentShopMenu, "itemPriceAndStock");
                    Dictionary<Item, int[]> itemPriceAndStock = inventoryInformation.GetValue();
                    itemPriceAndStock.Add(hardwood, new int[2] { hardwoodPrice, ShopMenu.infiniteStock });
                    inventoryInformation.SetValue(itemPriceAndStock);
                }
            }
        }

        private int GetWoodPosition(List<Item> items) 
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].parentSheetIndex == woodItemId)
                {
                    return i;
                }
            }

            return items.Count;
        }
    }
}
