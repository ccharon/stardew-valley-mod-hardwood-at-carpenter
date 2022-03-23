using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewValley.Menus;
using Object = StardewValley.Object;

namespace HardwoodAtCarpentersShop
{ 
    public class HardwoodAtCarpentersShop : Mod
    {
        private const int WoodItemId = 388;
        private const int HardwoodItemId = 709;
        private const int HardwoodPrice = 450;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += OnMenuChanged;
        }

        private void OnMenuChanged(object sender, MenuChangedEventArgs eventArgs)
        {
            if (!(eventArgs.NewMenu is ShopMenu shopMenu) || !shopMenu.portraitPerson.Equals(Game1.getCharacterFromName("Robin"))) return;

            Item hardwoodItem = new Object(Vector2.Zero, HardwoodItemId, ShopMenu.infiniteStock);

            var forSaleInformation = Helper.Reflection.GetField<List<ISalable>>(shopMenu, "forSale");
            var forSale = forSaleInformation.GetValue();
            var woodItemPosition = forSale.FindIndex(salable => salable is Item item && item.ParentSheetIndex.Equals(WoodItemId));
            forSale.Insert(woodItemPosition + 1, hardwoodItem);
            
            var inventoryInformation = Helper.Reflection.GetField<Dictionary<ISalable, int[]>>(shopMenu, "itemPriceAndStock");
            var itemPriceAndStock = inventoryInformation.GetValue();
            itemPriceAndStock.Add(hardwoodItem, new[] {HardwoodPrice, ShopMenu.infiniteStock});
        }
    }
}
