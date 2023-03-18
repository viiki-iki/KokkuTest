using System;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public int Health;
        public int BaseDamage;
        public CharacterClass _class;
        int classIndex; 
        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        SpecialSkill classSkill;
        Types types = new Types();
        public int PlayerIndex;
        public Character Target { get; set; } 

        public Character(CharacterClass characterClass)
        {           
            Health = 100;
            BaseDamage = 20;
            _class = characterClass;
        }

        public int HPModifier()
        {
            int hpModifier = classSkill.hpModifier;
            return hpModifier;
        }

        public void WalkTO(Grid battlefield, bool canWalk) // calculates in wich direction this character should move to be closer to a possible target
        {
            if (canWalk)
            {
                if (battlefield.grids.Exists(x => x.Index == currentBox.Index - 1))
                {
                    CurrentBoxStatus(battlefield, false);

                    if (currentBox.xIndex - Target.currentBox.xIndex == 0)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                        if (currentBox.yIndex > Target.currentBox.yIndex)
                        {
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked left      Current Y box: {currentBox.yIndex}\n");
                        }
                        else if (currentBox.yIndex < Target.currentBox.yIndex)
                        {
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked right      Current Y box: {currentBox.yIndex}\n");
                        }
                        return;
                    }
                    else if (currentBox.yIndex - Target.currentBox.yIndex == 0)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                        if (currentBox.xIndex > Target.currentBox.xIndex)
                        {
                            currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index - battlefield.yLength));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked up      Current X box: {currentBox.xIndex}\n");
                        }
                        else if (currentBox.xIndex < Target.currentBox.xIndex)
                        {
                            currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index + battlefield.yLength));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked down      Current X box: {currentBox.xIndex}\n");
                        }
                        return;
                    }
                    else if (currentBox.xIndex - Target.currentBox.xIndex != 0 || currentBox.yIndex - Target.currentBox.yIndex != 0)
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                        if (currentBox.xIndex > Target.currentBox.xIndex)
                        {
                            currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index - battlefield.yLength));
                            CurrentBoxStatus(battlefield, true);
                            currentBox.character = CurrentCharacter(currentBox);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked up      x: {currentBox.xIndex}      y: {currentBox.yIndex} \n");
                            return;
                        }
                        else if (currentBox.xIndex < Target.currentBox.xIndex)
                        {
                            currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index + battlefield.yLength));
                            CurrentBoxStatus(battlefield, true);
                            currentBox.character = CurrentCharacter(currentBox);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked down      x: {currentBox.xIndex}      y: {currentBox.yIndex} \n");
                            return;
                        }
                    }
                }
            }
        }
    
        public void StartTurn(Grid battlefield)
        {
            classSkill.status = false;
            if (CheckCloseTargets(battlefield))
            {
                Attack(battlefield);
                return;
            }
            else
            {
                if (classIndex == 4) // archer // 2 chance to attack / 1 chance to walk
                {
                    int arrowChance = types.GetRandomInt(1, 3);
                    if (arrowChance == 1)
                        WalkTO(battlefield, true);
                    else
                    {
                        SpecialSkill(battlefield);
                        return;
                    }
                }              
                else
                    WalkTO(battlefield, true);                                  
            }                           
        }

        bool CheckCloseTargets(Grid battlefield) // Check in x and y directions if there is any character close enough to be a target.
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);
        
            if (left || right || up || down) 
            {
                return true;
            }
            return false; 
        }

        public void CurrentBoxStatus(Grid battlefield, bool ocupied)
        {
            currentBox.ocupied = ocupied;
            currentBox.character = CurrentCharacter(currentBox);
            battlefield.grids[currentBox.Index] = currentBox;           
        }

        public string CurrentCharacter(GridBox box)
        {
            if(PlayerIndex == 0)
                box.character = "^~^";
            else if (PlayerIndex == 1)
                box.character = "ò.ó";

            string character = box.character;
            return character;
        }

        public void CheckClass()
        {
            classIndex = (int)_class;         
            classSkill = new SpecialSkill(classIndex, "", 0, false);
        }

        public void Attack (Grid battlefield)
        {
            if (Target.classIndex == 3 && Target.classSkill.status == true)
            {
                Console.WriteLine($"{Name} can't attack because {Target.Name} is invisible! \n");
                return;
            }
            else
            {
                int chance = types.GetRandomInt(1, 4);
                if (chance == 1)
                    SpecialSkill(battlefield);
                else if (chance == 2)
                    Console.WriteLine($"{Name} lost the chance to attack {Target.Name}! \n");
                else if (chance >= 3)
                {
                    Target.Health -= BaseDamage;
                    Console.WriteLine($"{Name} is attacking {Target.Name} and did {BaseDamage} damage! \n");
                }
                Console.WriteLine($"{Name} health is {Health}      {Target.Name} health is {Target.Health} \n");
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public void SpecialSkill(Grid battlefield)
        {
            Console.WriteLine($"{Name} is using the special skill {classSkill.name}! \n");
            int damage = BaseDamage + HPModifier();

            if (classIndex == 1)
            {
                Health += HPModifier();
                Console.WriteLine($"The skill gave {HPModifier()} of protection! \n");
            }
            else if (classIndex == 2 || classIndex == 4)
            {
                if (CheckCloseTargets(battlefield))
                {
                    Target.Health -= damage;
                    Console.WriteLine($"The skill did {damage} damage! \n");
                }
                else
                {
                    Target.Health -= HPModifier();
                    Console.WriteLine($"The skill did {HPModifier()} damage! \n");
                }                             
            }
            else if (classIndex == 3)
            {
                classSkill.status = true;
                Console.WriteLine($"{Name} is invisible for 1 turn! \n");               
            }                            
        }     
    }
}
