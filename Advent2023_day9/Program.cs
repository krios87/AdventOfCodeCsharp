using System;

class DAY9
{
    public static int calcDiff(int[] prevArray, int prevArraySize, bool part1){
        int currentArraySize = prevArraySize-1;
        int[] currentArray = new int[currentArraySize];
        bool isEqual = true;
        for(int i = 0; i< currentArraySize; i++){
            currentArray[i] = prevArray[i+1]-prevArray[i];
            if(i>0 && currentArray[i] != currentArray[i-1]){
                isEqual = false;
            }
        }
        int arrayElement = part1 ? (currentArraySize-1) : 0; //select last for part1 and first for part2        

        //if equal return value to be added or subtracted for the previous bigger array (level above), otherwise continue to create one difference array with one element less
        return isEqual ? currentArray[arrayElement] : currentArray[arrayElement] + (part1 ? 1 : -1) *calcDiff(currentArray,currentArraySize,part1);      
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