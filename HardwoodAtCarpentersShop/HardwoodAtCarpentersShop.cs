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

        public override void Entry(IModHelper helper)
        {
            MenuEvents.MenuChanged += Events_MenuChanged;
        }

        void Events_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            if (e.NewMenu is ShopMenu shopMenu && shopMenu.portraitPerson.Equals(Game1.getCharacterFromName("Robin", false)))
            {
                Item hardwoodItem = new Object(Vector2.Zero, hardwoodItemId, ShopMenu.infiniteStock);

                IReflectedField<List<Item>> itemsForSale = Helper.Reflection.GetField<List<Item>>(shopMenu, "forSale");

                int woodItemPosition = itemsForSale.GetValue().FindIndex(item => item.parentSheetIndex == woodItemId);
                itemsForSale.GetValue().Insert(woodItemPosition + 1, hardwoodItem);

                IReflectedField<Dictionary<Item, int[]>> itemsInventory = Helper.Reflection.GetField<Dictionary<Item, int[]>>(shopMenu, "itemPriceAndStock");
                itemsInventory.GetValue().Add(hardwoodItem, new int[] { hardwoodPrice, ShopMenu.infiniteStock });
            }
        }
    }
}
