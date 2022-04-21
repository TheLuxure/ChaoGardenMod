using ChaoGardenMod.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod
{
    public class ChaoGardenMod : Mod
	{
		public override void Load()
		{
			#region Default
			{
				string[] types = new string[24]
				{
					"",
					"Black",
					"Blue",
					"Brown",
					"Cadet Blue",
					"Colorless",
					"Corruption",
					"Crimson",
					"Cyan",
					"Gray",
					"Green",
					"Lime Green",
					"Lunar",
					"Nebula",
					"Orange",
					"Pink",
					"Purple",
					"Red",
					"Sky Blue",
					"Solar",
					"Stardust",
					"Vortex",
					"White",
					"Yellow"
				};

				for (int i = 0; i < types.Length; i++)
				{
					register(
						// Egg item rarity, Projectile chao scale, and size...
						new ValueTuple<int, float, Vector2>(0, 1.1F, new Vector2(32, 48)),
						// Internal name and egg name...
						$"{(types[i] != "" ? types[i] + " " : "")}Default",
						// Chao type (from types var)
						types[i],
						// Egg description
						$"Summons an adorable {types[i]} Chao!",
						// Buff description
						$"Boo!!!",
						// Chao egg type
						ChaoType.Unique | ChaoType.Support,
						// Projection! (projectile EXTRA ai; leave empty if you don't want changes to it)
						new Action<Projectile>((projectile) =>
						{
							// Modifying proj ai goes here!
						}),
						// Buff action
						new Action<string, Player, int>((summon, player, buffIndex) =>
						{
							player.statLife = 1;
							player.buffTime[buffIndex] = 999999;
							player.GetModPlayer<ChaoPlayer>().currentChao = summon;
							bool petProjectileNotSpawned = true;
							if (player.ownedProjectileCounts[Find<ModProjectile>(summon).Type] > 0)
							{
								petProjectileNotSpawned = false;
							}
							if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
							{
								Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Find<ModProjectile>(summon).Type, 0, 0f, player.whoAmI, 0f, 0f);
							}
						}));
				}
			}
			#endregion
			#region Axolotl
			{
				string[] types = new string[22]
				{
					"",
					"Black",
					"Blue",
					"Brown",
					"Cadet Blue",
					"Cyan",
					"Gray",
					"Green",
					"Lime Green",
					"Orange",
					"Pink",
					"Purple",
					"Red",
					"Sky Blue",
					"White",
					"Yellow",
					"Colorless",
					"Lunar",
					"Nebula",
					"Solar",
					"Stardust",
					"Vortex"
				};

				for (int i = 0; i < types.Length; i++)
				{
					register(
						new ValueTuple<int, float, Vector2>(8, 1.1F, new Vector2(32, 48)),
						$"{(types[i] != "" ? types[i] + " " : "")}Axolotl",
						types[i],
						$"Summons an adorable {types[i]} Axolotl Chao to play with you and help you regenerate!",

						$"Your adorable {types[i]} Axolotl Chao is swimming around you and granting you buffs!" +
					   "\nIncreases your Bait Power by 10% and grants you gills while in water" +
					   "\nIncreases your Life Regen and Mana Regen by 10, doubled while in water" +
					   "\nAxolotl Chao hate going into hot biomes and will start to dry out if they go there!",
						ChaoType.Unique | ChaoType.Support,
						new Action<Projectile>((projectile) => {
						}),
						new Action<string, Player, int>((summon, player, buffIndex) =>
						{
							player.lifeRegen += 10;
							player.manaRegen += 10;
							player.fishingSkill += 10;
							if (player.wet)
							{
								player.lifeRegen += 10;
								player.manaRegen += 10;
								player.gills = true;
							}
							if (player.ZoneDesert)
							{
								player.lifeRegen -= 100;
								player.manaRegenBonus -= 30;
								player.GetDamage(DamageClass.Generic) -= 0.1f;
								player.moveSpeed -= 0.2f;
							}
							player.buffTime[buffIndex] = 999999;
							player.GetModPlayer<ChaoPlayer>().currentChao = summon;
							bool petProjectileNotSpawned = true;
							if (player.ownedProjectileCounts[Find<ModProjectile>(summon).Type] > 0)
							{
								petProjectileNotSpawned = false;
							}
							if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
							{
								Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Find<ModProjectile>(summon).Type, 0, 0f, player.whoAmI, 0f, 0f);
							}
						})
						);
				}
			}
            #endregion
            
			SingularRegisters();
        }

		/// <summary>
		/// Put single-type chaos here!
		/// </summary>
		public void SingularRegisters()
		{
			register(new ValueTuple<int, float, Vector2>(6, 1.1F, new Vector2(32F, 48F)),
				"Iron",
				"Iron",
				"Summons an Iron Chao to follow you around and gather Iron Bars for you!",
				"Your Iron Chao is following you around and gathering Iron Bars for you!" +
				"\nIncreases your Life, Mana and Defense Regeneration by 4",
				ChaoType.Rare | ChaoType.Support | ChaoType.Harvester,
				new Action<Projectile>((projectile) =>
				{
					Player player = Main.player[projectile.owner];

					if (!((double)Math.Abs(projectile.velocity.X) > 0.2))
					{
						if (projectile.localAI[0] > 800)
						{
							Item.NewItem(player.GetProjectileSource_Item(Find<ModItem>("Iron").Item), (int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 22, Main.rand.Next(2), false, 0, false, false);
							projectile.localAI[0] = 0;
						}
						projectile.localAI[0]++;
					}
				}),
				new Action<string, Player, int>((summon, player, buffIndex) =>
				{
					player.lifeRegen += 4;
					player.manaRegen += 4;
					player.statDefense += 4;
					player.buffTime[buffIndex] = 999999;
					player.GetModPlayer<ChaoPlayer>().currentChao = summon;
					bool petProjectileNotSpawned = true;
					if (player.ownedProjectileCounts[Find<ModProjectile>(summon).Type] > 0)
					{
						petProjectileNotSpawned = false;
					}
					if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Find<ModProjectile>(summon).Type, 0, 0f, player.whoAmI, 0f, 0f);
					}
				})
				);
		}

		private bool register(ValueTuple<int, float, Vector2> values,
                        string name,
                        string type,
                        string tooltip,
                        string buffTooltip,
                        ChaoType chao,
                        Action<Projectile> projAction,
						Action<string, Player, int> buffAction)
        {
			bool success = true;
			try
			{
				AddContent(new ChaoEgg(values, name, type, tooltip, buffTooltip, chao, projAction, buffAction));
			}
			catch
            {
				success = false;
            }
			return success;
        }
    }
}