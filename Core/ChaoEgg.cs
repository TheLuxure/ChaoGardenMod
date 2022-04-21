using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class ChaoEgg : ModItem
    {
        protected string name;
        protected string type;
        protected string tooltip;
        protected string buffTooltip;
        protected ChaoType chaoType;
        protected Action<Projectile> projAction;
        protected Action<string, Player, int> buffAction;
        protected ValueTuple<int, float, Vector2> values; // RARITY | SCALE | SIZE

        public override string Name => name; // the internal name...
        public override string Texture
        {
            get
            {
                // Hella confusing unreadable code letsgo!
                string path = $"ChaoGardenMod/Assets/Eggs/{name.Replace($"{type} ", "")}/{(type != "" ? type : "Default")}";
                if (ModContent.RequestIfExists<Texture2D>(path, out _)) // Check if texture exists...
                {
                    return path;
                }
                // ... if not then give Unloaded Item texture
                return $"ChaoGardenMod/Assets/UnloadedItem";
            }
        }

        public ChaoEgg(ValueTuple<int, float, Vector2> values, string name, string type, string tooltip, string buffTooltip, ChaoType chaoType, Action<Projectile> projAction, Action<string, Player, int> buffAction)
        {
            this.name = name;
            this.type = type;
            this.tooltip = tooltip;
            this.buffTooltip = buffTooltip;
            this.chaoType = chaoType;
            this.projAction = projAction;
            this.buffAction = buffAction;
            this.values = values;
        }

        public override void SetStaticDefaults()
        {
            string tooltip = this.tooltip;
            string uniqueSub = "\nThis is a";
            if (chaoType.HasFlag(ChaoType.Rare))
            {
                uniqueSub += " [c/c765f6:Rare]";
            }
            if (chaoType.HasFlag(ChaoType.Unique))
            {
                uniqueSub += " [c/fafd20:Unique]";
            }
            if (chaoType.HasFlag(ChaoType.Support))
            {
                uniqueSub += " [c/b61239:Support]";
            }
            if (chaoType.HasFlag(ChaoType.Harvester))
            {
                uniqueSub += " [c/be7327:Harvester]";
            }
            uniqueSub += " Chao!";

            DisplayName.SetDefault(name + " Chao");
            Tooltip.SetDefault(tooltip + uniqueSub);
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            mod.AddContent(new ChaoBuff(name, type, buffTooltip, buffAction));
            mod.AddContent(new ChaoProj(values, name, type, projAction));
            return true;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 1;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>(name).Type;
            Item.buffType = Mod.Find<ModBuff>(name).Type;
            Item.rare = values.Item1;
            Item.noMelee = true;
            Item.accessory = true;
        }
    }
}
