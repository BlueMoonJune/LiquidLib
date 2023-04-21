using System;
using Terraria;
using Terraria.ModLoader;

namespace LiquidLib
{
	public class LiquidLib : Mod
	{
		public static byte PlaceLiquid(int i, int j, int type, byte amount) {
			ref LiquidTileData data = ref Main.tile[i, j].Get<LiquidTileData>();
			if (data.type == type) {
				if (data.amount + amount > 255) {
					byte change = (byte)(255 - data.amount);
					data.amount = 255;
					return change;
				}
				data.amount += (byte)Math.Min(data.amount + amount, 255);
				return amount;
			}
			else {
				return 0;
			}
		}

		public static void TakeLiquid(int i, int j, int amount) {
			LiquidTileData data = Main.tile[i, j].Get<LiquidTileData>();
			data.amount = (byte)Math.Max(data.amount - amount, 0);
		}
	}
}