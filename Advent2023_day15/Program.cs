using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;



class DAY15
{
    public static long hashAlgoritm(string asciiString){
        long AsciiValue = 0;
        foreach(char asciiChar in asciiString){
            AsciiValue = (AsciiValue + asciiChar)*17%256;
        }
        return AsciiValue;
    }
    static void Main(string[] args)
    {        
        string[] asciiStrings = File.ReadAllLines("../../../Advent/2023_day15.txt")[0].Split(',',StringSplitOptions.RemoveEmptyEntries);        
        long CountSum = 0;
        foreach(string asciiString in asciiStrings){
            CountSum += hashAlgoritm(asciiString);
        }
        Console.WriteLine("count part 1: " + CountSum);


        //part 2      

        OrderedDictionary[] boxes = new OrderedDictionary[256];

        for(int i = 0; i< asciiStrings.Length; i++){
            string label = "";
            int lenseStrength = 0;
            long boxnumber = 0;

            if(asciiStrings[i].Contains('=')){
                label = asciiStrings[i].Split('=')[0];
                lenseStrength = Convert.ToInt32(asciiStrings[i].Split('=')[1]);
                boxnumber = hashAlgoritm(label);
                if(boxes[boxnumber] == null){
                    boxes[boxnumber] = new OrderedDictionary(9);
                }
                if(boxes[boxnumber].Contains(label)){
                    boxes[boxnumber][label] = lenseStrength;
                }
                else{
                    boxes[boxnumber].Add(label,lenseStrength);
                }                
            }
            else if(asciiStrings[i].Contains('-')){
                label = asciiStrings[i].Split('-')[0];
                boxnumber = hashAlgoritm(label);
                if(boxes[boxnumber] == null){
                    boxes[boxnumber] = new OrderedDictionary(9);
                }
                boxes[boxnumber].Remove(label);                
            }
        }

        int count = 0;
         for(int j = 0; j<256; j++){
            if(boxes[j] != null){
                int slot = 1;
                foreach(DictionaryEntry lense in boxes[j]){      
                    count += (j+1)* slot * (int) lense.Value;   
                    slot++;                 
                }
            }
         }

         Console.WriteLine("count part 2: " + count);
    }
}