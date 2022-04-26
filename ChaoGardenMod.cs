using ChaoGardenMod.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod
{
    public class ChaoGardenMod : Mod
	{
		public static Dictionary<ChaoType, string> chaoTypePairs = new();

		public override void Load()
		{
			chaoTypePairs.Add(ChaoType.Rare, "[c/c765f6:Rare]");
			chaoTypePairs.Add(ChaoType.Unique, "[c/fafd20:Unique]");
			chaoTypePairs.Add(ChaoType.Support, "[c/b61239:Support]");
			chaoTypePairs.Add(ChaoType.Harvester, "[c/be7327:Harvester]");

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
					register(new ChaoFeature()
						.SetName($"{(types[i] != "" ? types[i] + " " : "")}Default")
						.SetType(types[i])
						.SetRarity(0)
						.SetScale(1.1f)
						.SetSize(new Vector2(32f, 48f))
						.SetTooltip((name) => $"Summons an adorable {name} Chao!")
						.SetBuffTooltip((name) => $"Boo!!!")
						.SetChaoType(ChaoType.Unique | ChaoType.Support)
						.SetBuffAction(new Action<string, Player, int>((summon, player, buffIndex) =>
						{
							player.statLife = 1;
						}))
						.Create());
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
					register(new ChaoFeature()
						.SetName($"{(types[i] != "" ? types[i] + " " : "")}Axolotl")
						.SetType(types[i])
						.SetRarity(8)
						.SetScale(1.1f)
						.SetSize(new Vector2(32f, 48f))
						.SetTooltip((name) => $"Summons an adorable {name} Axolotl Chao to play with you and help you regenerate!")
						.SetBuffTooltip((name) => $"Your adorable {name} Axolotl Chao is swimming around you and granting you buffs!" +
					   "\nIncreases your Bait Power by 10% and grants you gills while in water" +
					   "\nIncreases your Life Regen and Mana Regen by 10, doubled while in water" +
					   "\nAxolotl Chao hate going into hot biomes and will start to dry out if they go there!")
						.SetChaoType(ChaoType.Unique | ChaoType.Support)
						.SetBuffAction(new Action<string, Player, int>((summon, player, buffIndex) =>
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
						}))
						.Create());
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
			register(new ChaoFeature()
				.SetName("Iron")
				.SetType("Iron")
				.SetRarity(6)
				.SetScale(1.1f)
				.SetSize(new Vector2(32f, 48f))
				.SetTooltip((name) => "Summons an Iron Chao to follow you around and gather Iron Bars for you!")
				.SetBuffTooltip((name) => "Your Iron Chao is following you around and gathering Iron Bars for you!" +
				"\nIncreases your Life, Mana and Defense Regeneration by 4")
				.SetChaoType(ChaoType.Rare | ChaoType.Support | ChaoType.Harvester)
				.SetProjAction((projectile) =>
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
				})
				.SetBuffAction(new Action<string, Player, int>((summon, player, buffIndex) =>
				{
					player.lifeRegen += 4;
					player.manaRegen += 4;
					player.statDefense += 4;
				}))
				.Create());
		}

		private bool register(ChaoFeatureContext feature)
        {
			bool success = true;
			AddContent(new ChaoEgg(feature));
			AddContent(new ChaoProj(feature));
			AddContent(new ChaoBuff(feature));
			return success;
        }
    }
}