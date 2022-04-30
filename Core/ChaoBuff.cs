using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class ChaoBuff : ModBuff
    {
        protected ChaoFeatureContext context;
        protected ChaoFeature feature;

        public override string Name => feature.GetName(); // the internal name...
        public override string Texture
        {
            get
            {
                // Hella confusing unreadable code letsgo!
                string subType = feature.GetSubType() == "" ? "" : $"{feature.GetSubType()}/";
                string path = $"ChaoGardenMod/Assets/Eggs/{feature.GetName().Replace($"{feature.getType()} ", "")}/{subType}{(feature.getType() != "" ? feature.getType() : "Default")}";
                if (ModContent.RequestIfExists<Texture2D>(path, out _)) // Check if texture exists...
                {
                    return path;
                }
                // ... if not then give Unloaded Item texture
                return $"ChaoGardenMod/Assets/UnloadedItem";
            }
        }

        public ChaoBuff(ChaoFeatureContext featureContext)
        {
            context = featureContext;
            feature = context.GetFeature();

            if (!feature.getType().Contains(' '))
            {
                feature.SetType(Regex.Replace(feature.getType(), "([A-Z])", " $1").Trim());
            }
        }

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(feature.GetName());
            Description.SetDefault(feature.GetBuffTooltip().Invoke(feature.GetName()));
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (feature.GetBuffAction() != null)
            {
                feature.GetBuffAction().Invoke(feature.GetName(), player, buffIndex);
            }

            player.buffTime[buffIndex] = 999999;
            player.GetModPlayer<ChaoPlayer>().currentChao = feature.GetName();
            bool petProjectileNotSpawned = true;
            if (player.ownedProjectileCounts[ModContent.Find<ModProjectile>("ChaoGardenMod", feature.GetName()).Type] > 0)
            {
                petProjectileNotSpawned = false;
            }
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.Find<ModProjectile>("ChaoGardenMod", feature.GetName()).Type, 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
