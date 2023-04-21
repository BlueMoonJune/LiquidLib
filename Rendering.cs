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
	internal class Rendering : ModSystem
	{
		public override void PostDrawTiles() {
			Vector2 zero2 = Vector2.Zero;
			if (Main.drawToScreen) {
				zero2 = Vector2.Zero;
			}
			int num12 = (int)((Main.screenPosition.X - zero2.X) / 16f - 1f);
			int num13 = (int)((Main.screenPosition.X + (float)Main.screenWidth + zero2.X) / 16f) + 2;
			int num14 = (int)((Main.screenPosition.Y - zero2.Y) / 16f - 1f);
			int num15 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + zero2.Y) / 16f) + 5;
			if (num12 < 0) {
				num12 = 0;
			}
			if (num13 > Main.maxTilesX) {
				num13 = Main.maxTilesX;
			}
			if (num14 < 0) {
				num14 = 0;
			}
			if (num15 > Main.maxTilesY) {
				num15 = Main.maxTilesY;
			}
			Point screenOverdrawOffset = Main.GetScreenOverdrawOffset();

			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

			for (int j = num14 + screenOverdrawOffset.Y; j < num15 - screenOverdrawOffset.Y; j++) {
				for (int i = num12 + screenOverdrawOffset.X; i < num13 - screenOverdrawOffset.X; i++) {

					for (int c = 0; c < 4; c++) {
						Tile tile = Main.tile[i, j];
						if (!tile.HasTile || !Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType]) {
							LiquidTileData data = Main.tile[i, j].Get<LiquidTileData>();
							if (data.amount <= 0)
								continue; 
							Vector2 pos = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y);
							//14-(data.amount/32)*2
							byte topAmount = Main.tile[i, j - 1].Get<LiquidTileData>().amount;
							if (topAmount <= 0)
								Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos + new Vector2(0, 14-(data.amount/32)*2), new Rectangle(0, 0, 16, (data.amount / 32) * 2 + 2), Lighting.GetColor(i, j));
							else {
								if (Main.tile[i, j - 2].Get<LiquidTileData>().amount > 0)
									Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos, new Rectangle(0, 16, 16, 16), Lighting.GetColor(i, j));
								else
									Main.spriteBatch.Draw(ModContent.Request<Texture2D>("LiquidLib/ExampleLiquid").Value, pos, new Rectangle(0, 2+(topAmount / 32) * 2, 16, 16), Lighting.GetColor(i, j));
							}
						}
					}
				}
			}

			Main.spriteBatch.End();
		}
	}
}
