using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewValley.Menus;

namespace HardwoodAtCarpentersShop
{
    public class HardwoodAtCarpenterShop : Mod
    {
        const int woodItemId = 388;
        const int hardwoodItemId = 709;
        const int hardwoodPrice = 450;
        const string fieldForSale = "forSale";
        const string fieldItemPriceAndStock = "itemPriceAndStock";
        const string shopKeeperRobin = "Robin";

        public override void Entry(IModHelper helper)
        {
            MenuEvents.MenuChanged += Events_MenuChanged;
        }

        void Events_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            if (IsCarpentersShopMenu(e.NewMenu))
            {
                ShopMenu carpentersShopMenu = (ShopMenu) e.NewMenu;

                Item hardwood = new Object(Vector2.Zero, hardwoodItemId, ShopMenu.infiniteStock);

                IReflectedField<List<Item>> itemsForSale = Helper.Reflection.GetField<List<Item>>(carpentersShopMenu, fieldForSale);
                itemsForSale.GetValue().Insert(GetWoodPosition(itemsForSale.GetValue()) + 1, hardwood);

                IReflectedField<Dictionary<Item, int[]>> itemsInventory = Helper.Reflection.GetField<Dictionary<Item, int[]>>(carpentersShopMenu, fieldItemPriceAndStock);
                itemsInventory.GetValue().Add(hardwood, new int[] { hardwoodPrice, ShopMenu.infiniteStock });
            }
        }

        bool IsCarpentersShopMenu(IClickableMenu menu) 
        {
            return (menu is ShopMenu shopMenu) && shopMenu.portraitPerson.Equals(Game1.getCharacterFromName(shopKeeperRobin, false));
        }

        int GetWoodPosition(List<Item> items) 
        {
            int woodPosition = items.FindIndex(item => item.parentSheetIndex == woodItemId);
            return (woodPosition >= 0) ? woodPosition : items.Capacity;
        }
    }
}
