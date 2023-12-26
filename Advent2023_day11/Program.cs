using System;

class DAY11

{
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day11.txt"); 
        long sum1 = 0;
        long sum2 = 0;
        List<int> expandColumnsIndices = new List<int>();
        List<int> expandRowsIndices = new List<int>();
        const int expansionmultiplicator = 1000000 -1;
        List<Tuple<int,int>> galaxies = new List<Tuple<int, int>>(); //tuple instead of own galaxy class

        //search galaxies and empty rows/column step by step, column by column
        for(int i= 0; i <lines[0].Length; i++){            
            bool noGalaxyColumn = true;
            for(int j = 0; j<lines.Length; j++){
                if(i== 0 && !lines[j].Contains('#')){
                    expandRowsIndices.Add(j); //add column index to list
                }
                if(lines[j][i] == '#'){
                    noGalaxyColumn = false;
                    galaxies.Add(new Tuple<int,int>(j,i));
                }
            }
            if(noGalaxyColumn){//add row index to list
                expandColumnsIndices.Add(i);
            }
        }

        for(int i = 0; i<(galaxies.Count-1); i++){
            for(int j = i+1; j<galaxies.Count; j++){     
                //Item1 is x-coordinate and Item2 y-coordinate
                long distance = Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
                //count number of columns between galaxies that expands
                long xExpansion = expandRowsIndices.Count(x => (x < galaxies[i].Item1 && x >galaxies[j].Item1) || (x < galaxies[j].Item1 && x >galaxies[i].Item1));
                long yExpansion = expandColumnsIndices.Count(x => (x < galaxies[i].Item2 && x >galaxies[j].Item2) || (x < galaxies[j].Item2 && x >galaxies[i].Item2));
                sum1 += distance + (xExpansion + yExpansion);
                sum2 += distance + expansionmultiplicator*(xExpansion + yExpansion); 
            }
        }
        Console.WriteLine("sum part 1: " + sum1);
        Console.WriteLine("sum part 2: " + sum2);
    }

    
}