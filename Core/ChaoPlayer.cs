using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    public class ChaoPlayer : ModPlayer
    {
        public string currentChao;

        public override void ResetEffects()
        {
            currentChao = "";
        }
    }
}
