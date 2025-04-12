using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Sudoku;

namespace WinFormsApp2
{
    internal class Puzzle
    {
        static string[] puzzle1 = new string[16]
        {
            "2", "", "", "2",
            "", "4", "4", "3",
            "4", "", "", "1",
            "", "1", "3", ""
        };
        
        static string[] puzzle2 = new string[16]
        {
            "4", "", "", "2",
            "", "3", "", "3",
            "", "", "2", "1",
            "", "", "2", ""
        };
        
        static string[] puzzle3 = new string[16]
        {
            "1", "2", "", "",
            "", "2", "", "3",
            "", "", "", "1",
            "", "4", "", ""
        };
        
        static string[] puzzle4 = new string[16]
        {
            "1", "3", "", "",
            "", "2", "", "3",
            "", "", "", "1",
            "", "1", "3", ""
        };
        
        static string[] puzzle5 = new string[16]
        {
            "", "", "4", "",
            "", "2", "", "",
            "", "3", "", "",
            "2", "4", "3", ""
        };

        public static string[] GetPuzzle()
        {
            string[][] puzzles =
            {
                puzzle1, 
                puzzle2, 
                puzzle3, 
                puzzle4, 
                puzzle5
            };
            
            Random random = new Random();
            int randomIndex = random.Next(0, puzzles.Length);
            
            return puzzles[randomIndex];
        }
    }
}
