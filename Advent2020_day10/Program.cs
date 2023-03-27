using System;

class DAY10
{
    static void Main(string[] args)
    {
        
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day10.txt");

        SortedSet<int> myset = new SortedSet<int>();

        foreach(string line in lines){
            myset.Add(Convert.ToInt32(line));
        }

        int count1 = 0;
        int count2 = 0;
        int count3 = 0;

        myset.Add(0); //add outlet
        myset.Add(myset.ElementAt(myset.Count()-1)+3); //adding my adapter

        for(int i = 1; i< myset.Count(); i++){
            int diff = myset.ElementAt(i)-myset.ElementAt(i-1);
            count1 += diff == 1 ? 1 : 0;
            count2 += diff == 2 ? 1 : 0;
            count3 += diff == 3 ? 1 : 0;
        }


        for(int i = 1; i< myset.Count();i++){
            int diff = myset.ElementAt(i)-myset.ElementAt(i-1);
        }

        Dictionary<int,long> mydict = new Dictionary<int, long>();

        mydict[0] = 1; //1 way to get to first 0.
        for(int i= 1; i< myset.Count(); i++){
            int adapter = myset.ElementAt(i);
            mydict[adapter] = mydict.GetValueOrDefault(adapter-3,0) +mydict.GetValueOrDefault(adapter-2,0) + mydict.GetValueOrDefault(adapter-1,0);

        }
        Console.WriteLine(count1 + " " + count3 + " mult:" + (count1*count3) + " for: " + myset.ElementAt(myset.Count()-1) + " possible ways: " + mydict[myset.ElementAt(myset.Count()-1)]);
    
    
    
        //using combination //math:
        //no...
        //(m^(n-1)) - ((m^(n-4)) * (m-1))
        //Determine the number of elements in the set. Let's call this number "n".
        //Count the number of "jumps" between adjacent values that are allowed. For example, 
        //if the difference between adjacent values can only be 1 or 2, 
        //there are two allowed jumps. Let's call this number "m".
        //(m^(n-4)), represents the number of subsets that violate the condition (2 for 1 condition, 3 for 2)

//ach term in the series is of the form
// ((m^(n-k)) * (m-1)^k), where k is the number of pairs of adjacent elements with a difference of "d+k".

/*
F(0) = 1
F(1) = m
F(2) = m + (m-1)
F(n) = F(n-1) + (m-1) * F(n-2) for n > 2
m=2 and n=5.
*/
    
    }



}