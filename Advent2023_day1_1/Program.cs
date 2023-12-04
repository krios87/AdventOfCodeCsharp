using System;
using System.Text.RegularExpressions;

class DAY1
{
    static int ConvertStringToInt(string value){

        return value.Length == 1 ? value[0] - '0' : value switch 
        {
            "one" => 1, "two" => 2, "three" =>  3, "four" => 4, "five" => 5,
            "six" =>  6, "seven" => 7, "eight" => 8, "nine" =>  9, _ => -1
        };
    }

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day1.txt");
        Regex reg = new Regex("(?=(1|2|3|4|5|6|7|8|9|one|two|three|four|five|six|seven|eight|nine))");
        int sum = 0;
        foreach(string line in lines){
            MatchCollection matches = reg.Matches(line);
            int FirstNumber = ConvertStringToInt(matches[0].Groups[1].Value);            
            int LastNumber = ConvertStringToInt(matches[matches.Count-1].Groups[1].Value);
            sum += FirstNumber*10 + LastNumber;
        }    
        Console.WriteLine("sum: " + sum);    
    }    
}