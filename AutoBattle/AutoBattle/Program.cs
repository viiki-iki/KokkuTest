﻿using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid;
            CharacterClass playerCharacterClass;
            CharacterClass enemyClass;

            GridBox PlayerCurrentLocation;
            GridBox EnemyCurrentLocation;
            GridBox RandomLocation;

            Character PlayerCharacter;
            Character EnemyCharacter;
            List<Character> AllPlayers = new List<Character>();

            int currentTurn = 0;
            int random;
            string name;

            Setup(); 

            void Setup()
            {
                GetPlayerChoices();
            }

            void GetPlayerChoices()
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Name your Character:");
                name = Console.ReadLine();
                Console.Write(Environment.NewLine + Environment.NewLine);

                Console.WriteLine("Choose Between One of this Classes: \n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer ");
                string classChoice = Console.ReadLine();
                Console.Write(Environment.NewLine + Environment.NewLine);

                switch (classChoice)
                {
                    case "1":
                        Confirmation(classChoice);
                        break;
                    case "2":
                        Confirmation(classChoice);
                        break;
                    case "3":
                        Confirmation(classChoice);
                        break;
                    case "4":
                        Confirmation(classChoice);
                        break;
                    default:
                        GetPlayerChoices();
                        break;
                }
            }

            void Confirmation(string classChoice)
            {
                Console.WriteLine($"Name: {name}   Class: {classChoice} \n");
                Console.WriteLine("Confirm? [1] Yes, [2] No");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        BattlefieldSize();
                        CreatePlayerCharacter(Int32.Parse(classChoice));
                        break;
                    case "2":
                        GetPlayerChoices();
                        break;
                    default:
                        GetPlayerChoices();
                        break;
                }
            }

            void BattlefieldSize()
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Write a Battlefield Size: Number of Lines ");
                string lines = Console.ReadLine();
                Console.WriteLine("Write a Battlefield Size: Number of Columns ");
                string columns = Console.ReadLine();

                string size = lines + " x " + columns;
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("The Battlefield Size is " + size + "\n");
                Console.WriteLine("Confirm Battlefield Size? [1] Yes, [2] No");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        grid = new Grid(Int32.Parse(lines), Int32.Parse(columns));
                        break;
                    case "2":
                        BattlefieldSize();
                        break;
                    default:
                        BattlefieldSize();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
                playerCharacterClass = (CharacterClass)classIndex;              
                PlayerCharacter = new Character(playerCharacterClass);               
                PlayerCharacter.PlayerIndex = 0;
                PlayerCharacter.Name = name;
                Console.WriteLine($"{name} Class Choice: {playerCharacterClass}");

                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                int randomInteger = GetRandomInt(1, 4);
                enemyClass = (CharacterClass)randomInteger;
                EnemyCharacter = new Character(enemyClass);
                EnemyCharacter.PlayerIndex = 1;
                EnemyCharacter.Name = "Enemy";
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");

                StartGame();
            }

            void StartGame()
            {
                EnemyCharacter.Target = PlayerCharacter;
                PlayerCharacter.Target = EnemyCharacter;
                AllPlayers.Add(PlayerCharacter);
                AllPlayers.Add(EnemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn(){

                if (currentTurn == 0)
                {
                   // AllPlayers.Sort();  
                }

                foreach(Character character in AllPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(PlayerCharacter.Health == 0)
                {
                    return;
                } 
                else if (EnemyCharacter.Health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.Write("endgame?");
                    Console.Write(Environment.NewLine + Environment.NewLine);            
                    return;
                } else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.Write(Environment.NewLine + Environment.NewLine);             
                    
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
            }

            void AlocatePlayerCharacter()
            {
                random = GetRandomInt(0, grid.numberOfPossibleTiles);
                RandomLocation = grid.grids.ElementAt(random);
                if (!RandomLocation.ocupied)
                {
                    PlayerCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    RandomLocation.character = PlayerCharacter.CurrentCharacter(RandomLocation);
                    grid.grids[random] = RandomLocation;
                    PlayerCharacter.currentBox = grid.grids[random];
                    Console.Write($"{name} Position: {random}\n");
                
                    AlocateEnemyCharacter();
                } else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                random = GetRandomInt(0, grid.numberOfPossibleTiles);
                RandomLocation = grid.grids.ElementAt(random);
                if (!RandomLocation.ocupied)
                {
                    EnemyCurrentLocation = RandomLocation;
                    RandomLocation.ocupied = true;
                    RandomLocation.character = EnemyCharacter.CurrentCharacter(RandomLocation);
                    grid.grids[random] = RandomLocation;
                    EnemyCharacter.currentBox = grid.grids[random];
                    Console.Write($"Enemy Position: {random}\n");
                    grid.UpdateBattlefield();
                }
                else
                {
                    AlocateEnemyCharacter();
                }              
            }
        }
    }
}
