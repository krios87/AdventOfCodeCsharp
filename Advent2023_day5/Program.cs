using System;
using System.Reflection.Emit;


class myConversion{
    private long rangeStart = 0;
    private long rangeStop = 0;
    private long subtractValue = 0;

    public long RangeStart
    {
        get { return rangeStart; } 
        set { rangeStart = value; }
    }

    public long RangeStop
    {
        get { return rangeStop; } 
        set { rangeStop = value; }
    }

    public long SubtractValue
    {
        get { return subtractValue; } 
        set { subtractValue = value; }
    }

    public myConversion(long Start, long Stop, long Subtract){
        rangeStart = Start;
        rangeStop = Stop;
        subtractValue = Subtract;
    }


}
class myMap{

    List<myConversion> conversions = new();

    public void AddConversion(myConversion conv){
        conversions.Add(conv);
    }

    public long calcConversion(long number){
        long returnval = number;
        foreach(var conv in conversions){
            if(number >= conv.RangeStart && number <= conv.RangeStop){
                returnval -= conv.SubtractValue;
                break;
            }
        }
        return returnval;
    }


}
class DAY5
{

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day5.txt");
        long[] seeds = lines[0].Split(':')[1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray();
        List<myMap> maps = new();
        for(int i=1; i< lines.Length; i++){
            if(lines[i].Contains(':')){
                maps.Add(new myMap());
            }            
            else if(lines[i] != "" && Char.IsDigit(lines[i][0])){
                long[] convertValues = lines[i].Split(' ').Select(Int64.Parse).ToArray();
                maps.Last().AddConversion(new myConversion(convertValues[1],convertValues[1]+convertValues[2],convertValues[1]-convertValues[0]));
            }
        }

        long[] destinations = new long[seeds.Length];

        for(int i = 0; i< seeds.Length; i++){
            destinations[i] = seeds[i]; //start destination
            foreach(var map in maps){
                destinations[i] = map.calcConversion(destinations[i]); //update destination through the maps
            }
        }
    
        Console.WriteLine("part 1: " + destinations.Min());
    }        
}