using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;



class DAY15
{
    public static int hashAlgoritm(string asciiString){
        int AsciiValue = 0;
        foreach(char asciiChar in asciiString){
            AsciiValue = (AsciiValue + asciiChar)*17%256;
        }
        return AsciiValue;
    }
    static void Main(string[] args)
    {        
        string[] asciiStrings = File.ReadAllLines("../../../Advent/2023_day15.txt")[0].Split(',',StringSplitOptions.RemoveEmptyEntries);        
        int CountSum1 = 0;
        foreach(string asciiString in asciiStrings){
            CountSum1 += hashAlgoritm(asciiString);
        }
        Console.WriteLine("count part 1: " + CountSum1);

        //part 2      
        OrderedDictionary[] boxes = new OrderedDictionary[256];

        for(int i = 0; i< asciiStrings.Length; i++){
            if(asciiStrings[i].Contains('=')){
                string label = asciiStrings[i].Split('=')[0];
                int lenseStrength = Convert.ToInt32(asciiStrings[i].Split('=')[1]);
                int boxnumber = hashAlgoritm(label);
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
            else{
                string label = asciiStrings[i].Split('-')[0];
                int boxnumber = hashAlgoritm(label);
                boxes[boxnumber]?.Remove(label);
            }
        }

        int count2 = 0;
         for(int j = 0; j<boxes.Length; j++){
            if(boxes[j] != null){
                int slot = 1;
                foreach(DictionaryEntry lense in boxes[j]){
                    if(lense.Value is not null)
                        count2 += (j + 1) * slot * (int)lense.Value;
                    slot++;                 
                }
            }
         }

         Console.WriteLine("count part 2: " + count2);
    }
}