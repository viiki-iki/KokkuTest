using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {

       // public struct CharacterClassSpecific
       // {
       //     CharacterClass CharacterClass;
       //     float hpModifier;
       //     float ClassDamage;
       //     CharacterSkills[] skills;
       //
       // }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool ocupied;
            public int Index;
            public string character;

            public GridBox(int x, int y, bool ocupied, int index, string nameCharacter)
            {
                xIndex = x;
                yIndex = y;
                this.ocupied = ocupied;
                Index = index;
                character = nameCharacter;
            }
        }

       // public struct CharacterSkills
       // {
       //     string Name;
       //     float damage;
       //     float damageMultiplier;
       // }

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }
    }
}
