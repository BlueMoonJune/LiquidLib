using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace LiquidLib
{
	public class GalacticDropper : ModItem
	{
		public int type = 0;

		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Mechanic's Hammer");
			// Tooltip.SetDefault("Left click to trigger blocks\nRight click to spark wires");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(gold: 3, silver: 25);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; // Automatically re-swing/re-use this item after its swinging animation is over.
			Item.mech = true;
		}

		public override void UseAnimation(Player player) {
			SoundEngine.PlaySound(SoundID.Splash);
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer) {
				Point pos = Main.MouseWorld.ToTileCoordinates();
				if (type >= 0) {
					LiquidLib.PlaceLiquid(pos.X, pos.Y, 0, 255);
				} else {
					LiquidLib.TakeLiquid(pos.X, pos.Y, 255);
				}
				return true;
			}
			return false;
		}
		public override bool AltFunctionUse(Player player) {
			if (player.whoAmI == Main.myPlayer) {
				if (type >= 0) {
					type = -1;
					return true;
				}
				type++;
				return true;
			}
			return false;
		}
	}
}
