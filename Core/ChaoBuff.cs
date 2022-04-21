using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    [Autoload(false)]
    public class ChaoBuff : ModBuff
    {
        protected string name;
        protected string type;
        protected string tooltip;
        protected Action<string, Player, int> action;

        public override string Name => name;
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

        public ChaoBuff(string name, string type, string tooltip, Action<string, Player, int> action)
        {
            this.name = name;
            this.type = type;
            this.tooltip = tooltip;
            this.action = action;
        }

        public override bool IsLoadingEnabled(Mod mod) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(name);
            Description.SetDefault(tooltip);
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            action.Invoke(name, player, buffIndex);
        }
    }
}
