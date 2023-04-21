using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace LiquidLib
{
	internal class Update : ModSystem
	{
		public override void PostUpdateWorld() {
			for (int i = 0; i < Main.maxTilesX; i++) {
				for (int j = Main.maxTilesY; j > 0; j--) {
					ref LiquidTileData data = ref Main.tile[i, j].Get<LiquidTileData>();
					if (data.amount > 0) {
						Tile below = Main.tile[i, j + 1];
						if (!below.HasTile || !Main.tileSolid[below.TileType] || Main.tileSolidTop[below.TileType]) {
							data.amount -= LiquidLib.PlaceLiquid(i, j + 1, data.type, data.amount);
						}
					}
				}
			}
		}
	}
}
