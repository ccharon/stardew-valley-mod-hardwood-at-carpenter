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
        private const int HARDWOOD_ITEM_ID = 709;
        private const int HARDWOOD_PRICE = 750;
        private const int HARDWOOD_STACKSIZE = int.MaxValue;

        public override void Entry(IModHelper helper)
        {
            MenuEvents.MenuChanged += Events_MenuChanged;
        }

        void Events_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            if (e.NewMenu is ShopMenu currentShopMenu)
            {
                if (currentShopMenu.portraitPerson.Equals(StardewValley.Game1.getCharacterFromName("Robin", false)))
                {
                    Item hardwood = (Item) new StardewValley.Object(Vector2.Zero, HARDWOOD_ITEM_ID, HARDWOOD_STACKSIZE);

                    IReflectedField<List<Item>> forSaleInformation = this.Helper.Reflection.GetField<List<Item>>(currentShopMenu, "forSale");
                    List<Item> forSale = forSaleInformation.GetValue();
                    forSale.Insert(1,hardwood);
                    forSaleInformation.SetValue(forSale);

                    IReflectedField<Dictionary<Item, int[]>> inventoryInformation = this.Helper.Reflection.GetField<Dictionary<Item, int[]>>(currentShopMenu, "itemPriceAndStock");
                    Dictionary<Item, int[]> itemPriceAndStock = inventoryInformation.GetValue();
                    itemPriceAndStock.Add(hardwood, new int[2] { HARDWOOD_PRICE, HARDWOOD_STACKSIZE });
                    inventoryInformation.SetValue(itemPriceAndStock);
                }
            }
        }
    }
}
