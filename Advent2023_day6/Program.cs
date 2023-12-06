using System;
using System.Reflection.Emit;

class DAY6
{

    static long CountNbrBeatRecord(long Time, long Distance){
        long firstFound = 0;
        for(long iSpeed = 1; iSpeed < Time; iSpeed++){
            long tmpDistance = iSpeed*(Time-iSpeed);
            if(tmpDistance > Distance){
                firstFound = iSpeed;
                break;
            }
        }
        return Time - 2*firstFound+1; //symmetric, only need to find where it starts beating the target (ispeed*(time-ispeed))
        
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day6.txt");
        long[] Times = lines[0].Split(':')[1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray();
        long[] Distance = lines[1].Split(':')[1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(Int64.Parse).ToArray();
        long totalCount = 1;
        for(long i=0; i<Times.Length;i++){
            totalCount *= CountNbrBeatRecord(Times[i],Distance[i]);
        }            
        Console.WriteLine("total part 1: " + totalCount);  

        //part2:
        long TotalTime = Convert.ToInt64(new string(lines[0].Where(c => Char.IsDigit(c)).ToArray()));
        long TotalDistance = Convert.ToInt64(new string(lines[1].Where(c => Char.IsDigit(c)).ToArray()));        
        Console.WriteLine("total part 2: " + CountNbrBeatRecord(TotalTime,TotalDistance));
    }        
}