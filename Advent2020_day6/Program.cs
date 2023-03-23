//#define Solution1

using System;


  class MyProgram
  {
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day6.txt");

        var charset = new HashSet<char>();
        int count = 0;

        #if Solution1
        foreach(string line in lines){
            if(line.Length <1){
                count += charset.Count;
                charset.Clear();
            }else{
                for(int i = 0; i<line.Length; i++){
                    charset.Add(line[i]);
                }
                
            }
        }
        #else

        var chardict = new Dictionary<char,int>();

        int NbrPassengers = 0;
        foreach(string line in lines){
            if(line.Length <1){
                foreach(var el in chardict){
                    if(el.Value == NbrPassengers){
                        count++;
                    }
                }
                NbrPassengers = 0;
                chardict.Clear();
            }else{
                NbrPassengers++;
                for(int i = 0; i<line.Length; i++){
                    if(chardict.ContainsKey(line[i])){
                        chardict[line[i]]++;
                    }
                    else{
                        chardict.Add(line[i],1);
                    }                    
                }                
            }
        }

        #endif   
        

       Console.WriteLine(count);
    }
  }

