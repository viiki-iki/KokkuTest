using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle
{
    public class Types
    {
        public struct SpecialSkill
        {
            int type;
            public string name;
            public bool status;
            public int hpModifier;

            public SpecialSkill(int typeClass, string skillName, int hp, bool skillStatus)
            {
                type = typeClass;
                if(type == 1)
                {
                    skillName = "Light Shield";
                    hp = 5;
                }else if (type == 2)
                {
                    skillName = "Dual Attack";
                    hp = 5;
                }else if (type == 3)
                {
                    skillName = "Illusion Trick";
                    hp = 0;
                }else if (type == 4)
                {
                    skillName = "Sharp Arrow";
                    hp = 5;
                }
                name = skillName;
                hpModifier = hp;
                status = skillStatus;
            }
        }       

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

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Mage = 3,
            Archer = 4
        }      
    }
}
