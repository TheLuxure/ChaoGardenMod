using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ChaoGardenMod.Content.NPCs
{
    public class ChaoPrinciple : ModNPC
    {
        private static int shop = 0;

        public override string TownNPCName() => "???";

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int j = 0; j < Main.maxPlayers; j++)
            {
                Player player = Main.player[j];
                if (player.active)
                {
                    for (int i = 0; i < player.inventory.Length; i++)
                    {
                        if (player.inventory[i].type == ItemID.Wood)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string GetChat()
        {
            return Main.rand.Next(10) switch
            {
                0 => "Neutral Chao are very uncommon but as a gift, I have given you two for the start of your garden.",
                1 => "Looking for information? Here at Chao Kindergarten, we have prepared a website dedicated to helping answer your questions. Please keep in mind it's still a work in progress.",
                2 => "There is no such thing as a perfect Chao or the best Chao. Each Chaos stats and abilties are different. Try raising and caring for many different types of Chao and you may find the perfect helper for you!",
                3 => "There are more than 888 different kinds of Chao and new ones are being discovered every day. How do I know the exact number? Call it an ''educated guess'', if you will. Hohoho!",
                4 => "The world is huge and filled with many Chao Gardens. In the future, you'll discover some new ones that have never before discovered Chao. Just make sure to keep your eye out for when that happens!",
                5 => "How's the Omo-Chao I gave you doing? Is it helping you out on your journey outside the garden? If you lose Omo-Chao or if it gets damaged, you can buy another one from the Chao Merchant.",
                6 => "My highest recommendation is to carefully study what giving your Chao will turn it into, before you give it to them because Chao evolutions are permanent!",
                7 => "Omo-Chao is incredibly useful, especially for when you're venturing out of the safety of the garden. If Omo-Chao senses danger, it'll alert you immediately.",
                8 => "What do you wish to learn about today?",
                _ => "Good day to you! I’m the Chao Kindergarten principal. I am here to provide valuable information that will make your life easier in and out of the garden.",
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (shop == 0)
            {
                button = "Item Shop";
            }
            else
            {
                button = "Other Shop";
            }
            button2 = "Change Shops";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
        {
            if (firstButton)
            {
                openShop = true;
            }
            else if (shop == 0)
            {
                shop = 1;
            }
            else
            {
                shop = 0;
            }
        }
    }
}
