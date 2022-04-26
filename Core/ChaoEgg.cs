using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class ChaoEgg : ModItem
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

        public ChaoEgg(ChaoFeatureContext featureContext)
        {
            context = featureContext;
            feature = context.GetFeature();

            if (!feature.getType().Contains(' '))
            {
                feature.SetType(Regex.Replace(feature.getType(), "([A-Z])", " $1").Trim());
            }
        }

        public override void SetStaticDefaults()
        {
            string tooltip = feature.GetTooltip().Invoke(feature.GetName());
            string uniqueSub = "\nThis is a";
            foreach (KeyValuePair<ChaoType, string> entry in ChaoGardenMod.chaoTypePairs)
            {
                if (feature.GetChaoType().HasFlag(entry.Key))
                {
                    uniqueSub += $" {entry.Value}";
                }
            }
            uniqueSub += " Chao!";

            DisplayName.SetDefault(feature.GetName() + " Chao");
            Tooltip.SetDefault(tooltip + uniqueSub);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 1;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>(feature.GetName()).Type;
            Item.buffType = Mod.Find<ModBuff>(feature.GetName()).Type;
            Item.rare = feature.GetRarity();
            Item.noMelee = true;
            Item.accessory = true;
        }
    }
}
