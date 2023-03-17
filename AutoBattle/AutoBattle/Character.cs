using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string Name { get; set; }
        public int Health;
        public int BaseDamage;
        public CharacterClass _class;

        public float DamageMultiplier { get; set; }
        public GridBox currentBox;
        public int PlayerIndex;
        public Character Target { get; set; } 

        public Character(CharacterClass characterClass)
        {           
            Health = 100;
            BaseDamage = 20;
            _class = characterClass;
        }

        public bool TakeDamage(int amount)
        {
            if((Health -= amount) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void WalkTO(bool CanWalk)
        {

        }

        public void StartTurn(Grid battlefield)
        {          
            if (CheckCloseTargets(battlefield)) 
            {
                Attack(Target);
                
                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target              
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
                            // return;
                        }
                        else if (currentBox.yIndex < Target.currentBox.yIndex)
                        {
                            currentBox = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked right      Current Y box: {currentBox.yIndex}\n");
                             // return;
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
                           // return;
                        }
                        else if (currentBox.xIndex < Target.currentBox.xIndex)
                        {
                            currentBox = (battlefield.grids.Find(y => y.Index == currentBox.Index + battlefield.yLength));
                            CurrentBoxStatus(battlefield, true);
                            battlefield.UpdateBattlefield();

                            Console.WriteLine($"{Name} walked down      Current X box: {currentBox.xIndex}\n");
                           //  return;
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

       //  Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.Index == currentBox.Index - 1).ocupied);
            bool right = (battlefield.grids.Find(x => x.Index == currentBox.Index + 1).ocupied);
            bool up = (battlefield.grids.Find(x => x.Index == currentBox.Index + battlefield.xLenght).ocupied);
            bool down = (battlefield.grids.Find(x => x.Index == currentBox.Index - battlefield.xLenght).ocupied);
        
            if (left || right || up || down) 
            {
                Console.WriteLine("ATTACK!");
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
            {
                box.character = "^~^";
            }else if (PlayerIndex == 1)
            {
                box.character = "ò.ó";
            }
            string character = box.character;
            return character;
        }
        
        public void Attack (Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, BaseDamage));
            Console.WriteLine($"{Name} is attacking the player {Target.Name} and did {BaseDamage} damage\n");
        }
    }
}
