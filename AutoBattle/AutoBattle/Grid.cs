using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;      
        public int numberOfPossibleTiles;

        GridBox initialBox;
        GridBox currentgrid;

        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;           

            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    initialBox = new GridBox(i, j, false, Columns * i + j, "");
                    grids.Add(initialBox);
                }
            }
            numberOfPossibleTiles = grids.Count;
        }

        public void UpdateBattlefield() // prints the matrix that indicates the tiles of the battlefield
        {
            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    currentgrid = new GridBox(i, j, false, yLength * i + j, "");

                    if (i == 0)
                    {
                        currentgrid.ocupied = grids[j].ocupied;
                        if (currentgrid.ocupied)
                        {
                            currentgrid.character = grids[j].character;
                            Console.Write($"[{currentgrid.character}]\t");
                        }
                        else
                        {
                            Console.Write("[ ]\t");
                        }
                    }
                    else
                    {
                        int aa = i + i;
                        if (currentgrid.Index >= yLength && currentgrid.Index < yLength * aa)
                        {
                            if (grids[currentgrid.Index].ocupied)
                            {
                                currentgrid.character = grids[currentgrid.Index].character;
                                Console.Write($"[{currentgrid.character}]\t");
                            }
                            else
                            {
                                Console.Write($"[ ]\t");
                            }
                        }                                                          
                    }                                  
                }                 
                Console.Write(Environment.NewLine + Environment.NewLine);
            }          
        }
    }
}
