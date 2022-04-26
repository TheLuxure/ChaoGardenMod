using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ChaoGardenMod.Core
{
    public class ChaoFeature
    {
        private string name;
        private string type;
        private string subType;
        private Func<string, string> tooltip;
        private Func<string, string> buffTooltip;
        private ChaoType chaoType;
        private Action<Projectile> projAction;
        private Action<string, Player, int> buffAction;
        private int rarity;
        private float scale;
        private Vector2 size;

        public ChaoFeature()
        {
            subType = "";
            rarity = 0;
            scale = 1f;
            size = new Vector2(32f, 48f);
        }

        public new string ToString()
        {
            string array = "";
            array += $"{{name}} = {name};";
            array += $"{{type}} = {type};";
            array += $"{{subType}} = {subType};";
            array += $"{{tooltip}} = {tooltip};";
            array += $"{{buffTooltip}} = {buffTooltip};";
            array += $"{{chaoType}} = {chaoType};";
            array += $"{{projAction}} = {projAction};";
            array += $"{{buffAction}} = {buffAction};";
            array += $"{{rarity}} = {rarity};";
            array += $"{{scale}} = {scale};";
            array += $"{{size}} = {size};";
            return $"{{ {array} }}";
        }

        public string GetName() => name;
        public ChaoFeature SetName(string value)
        {
            name = value;
            return this;
        }

        public string getType() => type;
        public ChaoFeature SetType(string value)
        {
            type = value;
            return this;
        }

        public string GetSubType() => subType;
        public ChaoFeature SetSubType(string value)
        {
            subType = value;
            return this;
        }

        public ChaoType GetChaoType() => chaoType;
        public ChaoFeature SetChaoType(ChaoType value)
        {
            chaoType = value;
            return this;
        }

        public int GetRarity() => rarity;
        public ChaoFeature SetRarity(int value)
        {
            rarity = value;
            return this;
        }

        public float GetScale() => scale;
        public ChaoFeature SetScale(float value)
        {
            scale = value;
            return this;
        }

        public Action<Projectile> GetProjAction() => projAction;
        public ChaoFeature SetProjAction(Action<Projectile> value)
        {
            projAction = value;
            return this;
        }

        public Action<string, Player, int> GetBuffAction() => buffAction;
        public ChaoFeature SetBuffAction(Action<string, Player, int> value)
        {
            buffAction = value;
            return this;
        }

        public Vector2 GetSize() => size;
        public ChaoFeature SetSize(Vector2 value)
        {
            size = value;
            return this;
        }

        public Func<string, string> GetTooltip() => tooltip;
        public ChaoFeature SetTooltip(Func<string, string> value)
        {
            tooltip = value;
            return this;
        }

        public Func<string, string> GetBuffTooltip() => buffTooltip;
        public ChaoFeature SetBuffTooltip(Func<string, string> value)
        {
            buffTooltip = value;
            return this;
        }

        public ChaoFeatureContext Create()
        {
            if (string.IsNullOrEmpty(name) || buffAction == null)
            {
                throw new InvalidOperationException("Some of required parameters are missing!");
            }
            return new ChaoFeatureContext(this);
        }
    }

    public record ChaoFeatureContext
    {
        private readonly ChaoFeature feature;

        internal ChaoFeatureContext(ChaoFeature feature)
        {
            this.feature = feature;
        }

        public ChaoFeature GetFeature() => feature;
    }
}
