using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace LiquidLib
{
	internal class LiquidTile : GlobalTile
	{
		internal static List<Texture2D> textures = new() { };

		public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch) {
			Tile tile = Main.tile[i, j];
			if (!tile.HasTile || !Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType])
				return true;
			LiquidTileData data = tile.Get<LiquidTileData>();
			if (data.amount <= 0)
				return true;
			Vector2 TileOffset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 pos = new Vector2(i, j) * 16 - Main.screenPosition + TileOffset;
			byte topAmount = Main.tile[i, j - 1].Get<LiquidTileData>().amount;
			if (topAmount <= 0)
				Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos + new Vector2(0, 14 - (data.amount / 32) * 2), new Rectangle(0, 0, 16, (data.amount / 32) * 2 + 2), Lighting.GetColor(i, j));
			else {
				if (Main.tile[i, j - 2].Get<LiquidTileData>().amount > 0)
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos, new Rectangle(0, 16, 16, 16), Lighting.GetColor(i, j));
				else
					Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos, new Rectangle(0, 2 + (topAmount / 32) * 2, 16, 16), Lighting.GetColor(i, j));
			}
			return true;
		}
	}

	internal struct LiquidTileData : ITileData {

		public byte amount = 0;
		public int type = 0;

		public LiquidTileData(int type, byte amount) {
			this.type = type;
			this.amount = amount;
		}

	}
}
