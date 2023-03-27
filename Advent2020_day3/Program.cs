using System;

class DAY9
{

    static int findTrees(ref bool[,] map, int rows,int columns, int stepDown, int stepRight){
        int coordX=0;
        int coordY=0;
        int count = 0;

        while(coordY< rows){
            count += map[coordY,coordX % columns] ? 1 : 0;
            coordX +=stepRight;
            coordY +=stepDown;            
        }

        return count;

    }

    static void Main(string[] args)
    {
        
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day3.txt");

        bool[,] map = new bool[lines.Count(),lines[0].Length];
    
        int row = 0;
        foreach(string li in lines){
            for(int i = 0; i< li.Length; i++){
                map[row,i] = (li[i] == '#');
            }
            row++;
        }


        int count1 = findTrees(ref map, lines.Count(), lines[0].Length, 1,1);
        int count2 = findTrees(ref map, lines.Count(), lines[0].Length, 1,3);
        int count3 = findTrees(ref map, lines.Count(), lines[0].Length, 1,5);
        int count4 = findTrees(ref map, lines.Count(), lines[0].Length, 1,7);
        int count5 = findTrees(ref map, lines.Count(), lines[0].Length, 2,1);
        
        long product = count1*count2*count3*count4*count5;
        Console.WriteLine(product);
    }

}