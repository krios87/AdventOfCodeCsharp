using System;

class DAY9
{
    public static int calcDiff(int[] prevArray, int prevArraySize, bool part1){
        int[] currentDiffArray = prevArray.Skip(1).Select((x, index) => x - prevArray[index]).ToArray(); //create array from difference of array element from input
        bool isEqual = currentDiffArray[currentDiffArray.Length-1] == currentDiffArray[currentDiffArray.Length-2]; //enough to check the last 2 then all are equal
        int arrayElement = part1 ? (currentDiffArray.Length-1) : 0; //select last for part1 and first for part2
        //if equal return value to be added or subtracted for the previous bigger array (level above), otherwise continue to create one difference array with one element less
        return isEqual ? currentDiffArray[arrayElement] : currentDiffArray[arrayElement] + (part1 ? 1 : -1) *calcDiff(currentDiffArray,currentDiffArray.Length,part1);      
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day9.txt"); 
        int sum1 = 0;
        int sum2 = 0;
        foreach(string line in lines){
            int[] numbers = line.Split(' ').Select(int.Parse).ToArray();
            sum1 += numbers[numbers.Length-1] + calcDiff(numbers,numbers.Length,true);
            sum2 += numbers[0] - calcDiff(numbers,numbers.Length,false);            
        }

        Console.WriteLine("sum part 1: " + sum1);
        Console.WriteLine("sum part 2: " + sum2);
    }

    
}