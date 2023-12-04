using System;
using System.Reflection.Emit;

class DAY4
{
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day4.txt");
        string[][] cards = new string[lines.Length][];
        int[][] winningNumbers = new int[lines.Length][];
        int[][] myNumbers = new int[lines.Length][];

        for(int i = 0; i< lines.Length; i++){
            cards[i] = lines[i].Split(':')[1].Split('|');
            winningNumbers[i] = cards[i][0].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
            myNumbers[i] = cards[i][1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
        }
        
        int[] countwinners = new int[lines.Length];
        //count part 1
        double totalcount = 0;
        for(int i = 0; i< lines.Length; i++){
            countwinners[i] = 0;
            foreach(int winning in winningNumbers[i]){
                countwinners[i] += myNumbers[i].Contains(winning) ? 1 : 0;
            }
            totalcount += countwinners[i] > 0 ? Math.Pow(2,countwinners[i]-1) : 0;            
        }
            
        Console.WriteLine("total part 1: " + totalcount);

        //Count cards part 2:
        int[] multipliers = Enumerable.Repeat(1, lines.Length).ToArray();

        for(int i = 0; i< lines.Length; i++){
            for(int j=0; j< multipliers[i]; j++){
                if(countwinners[i]>0){                
                    for(int iCopies=1; iCopies<=countwinners[i]; iCopies++){                    
                        multipliers[i+iCopies]+= iCopies < lines.Length ? 1 : 0;               
                    }     
                }      
            }            
        }
        Console.WriteLine("part2 sum cards: "+ multipliers.Sum());
    }    

    
}