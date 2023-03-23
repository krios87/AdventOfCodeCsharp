using System;

namespace HelloWorld
{
  class Hello
  {
    static void Main(string[] args)
    {

        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day5.txt");
        

        var IDs = new List<int>();
        foreach (string li in lines){
            int IDnew = 0;
            for(int i = 0; i< 10; i++){
                IDnew += (li[i] == 'B' || li[i] == 'R') ? (int)Math.Pow(2,7-i+2) : 0;
            }
         
        IDs.Add(IDnew);
        }

        IDs.Sort();

        //part1:
        Console.WriteLine(IDs.Last());
         
       
       //part 2:
       for(int i=0; i< IDs.Count;i++){
        if(IDs[i+1] != (IDs[i]+1)){
            Console.WriteLine("part2: " + (IDs[i] + 1));
            break;
        }
       }
            
    }
  }
}