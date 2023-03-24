using System;

class DAY9
{

    static bool isSum(ref Queue<UInt64> myQueue, UInt64 value){

        for(int i = 0; i< (myQueue.Count()-1); i++){

            for(int j=i+1; j< myQueue.Count(); j++){
                if(myQueue.ElementAt(i) + myQueue.ElementAt(j) == value){
                    return true;
                }
            }
        }

        return false;

    }

    static void Main(string[] args)
    {
        const int preemble = 25;
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day9.txt");

        UInt64[] intlines = new UInt64[lines.Count()];
        Queue<UInt64> QueueNumbers = new Queue<UInt64>();
    
        for(int i = 0; i< lines.Count(); i++){
            intlines[i] = Convert.ToUInt64(lines[i]);

        }
        ulong invalidNumber = 0;
        int invalidIndex = 0;
        for(int i = 0; i< lines.Count(); i++){
            if(i<preemble){
                QueueNumbers.Enqueue(intlines[i]);
            }else{

                if(!isSum(ref QueueNumbers,intlines[i])){
                    invalidNumber = intlines[i];
                    invalidIndex = i;
                    break;
                }
                QueueNumbers.Dequeue();
                QueueNumbers.Enqueue(intlines[i]);
            }
        }
        
        Console.WriteLine("part1: " + invalidNumber);

        List<ulong> newlist = new List<ulong>();

        newlist = intlines.ToList<ulong>();

        ulong sum= 0;
        for(int i = 0; i<invalidIndex-1; i++){

            for(int j=i+1; j<invalidIndex;j++){
                
                for(int k=i; k<=j;k++){
                    sum += intlines[k];
                }
                if(sum==invalidNumber){
                    
                    List<ulong> smalllist = newlist.GetRange(i,j-i);                    

                    Console.WriteLine("part2: " + (smalllist.Max() + smalllist.Min()));
                }
                sum = 0;
            }
        }
    }

}
        