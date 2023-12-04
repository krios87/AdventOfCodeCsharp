using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

class myNumber{

    private int numbervalue = 0;
    private int index = 0;
    private bool iscountedAndAdjecent = false;

    public int numberValue
    {
        get { return numbervalue; } 
        set { numbervalue = value; }
    }

    public bool isCountedAndAdjecent
    {
        get { return iscountedAndAdjecent; } 
        set { iscountedAndAdjecent = value; }
    }

    public myNumber(int myNumber, int myIndex){
        index = myIndex;
        numbervalue = myNumber;
    }

    public bool checkisAdjacent(int xCoordinate){
        bool tmpIsAdjecent = false;
        int tmpIndex = index;
        
        for(int i = 1; i < 4; i++){
            tmpIsAdjecent= (xCoordinate == tmpIndex) || (xCoordinate == tmpIndex - 1) || (xCoordinate == tmpIndex +1);
            if(tmpIsAdjecent || numberValue < Math.Pow(10,i)){
                break;
            }
            tmpIndex += 1;
        }
        return tmpIsAdjecent;         
    }
}

class DAY3
{
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day3.txt");

        List<myNumber>[] RowArrayList = new List<myNumber>[lines.Length];
        List<KeyValuePair<string, int>>[] RowArraySpecialList = new List<KeyValuePair<string, int>>[lines.Length];

        Regex reg = new Regex("\\d+"); //find numbers (several digits in row)
        Regex specialReg = new Regex("[-/&=#@$£%+*]"); //find single special signs

        
        //find numbers and special signs and save them
        for(int i = 0; i< lines.Length; i++){
            MatchCollection matches = reg.Matches(lines[i]);       
            RowArrayList[i] = new List<myNumber>();  
            foreach(Match match in matches){
                RowArrayList[i].Add(new myNumber(Convert.ToInt32(match.Value),match.Index));
            }

            MatchCollection specialMatches = specialReg.Matches(lines[i]);
            RowArraySpecialList[i] = new List<KeyValuePair<string, int>>();
            foreach(Match match in specialMatches){
                RowArraySpecialList[i].Add(new KeyValuePair<string, int>(match.Value,match.Index));
            }            
            
        }    

        //check adjecent numbers (part one) and exactly 2 adjacent with special sign '*' (gearratio part 2)
        int sum = 0;
        int gearratio = 0;
        for(int i = 0; i<RowArraySpecialList.Length; i++){
            foreach(var specialSign in RowArraySpecialList[i]){
                int countIfGear = 0;
                int tmpGearRatio = 1;
                for(int j=i-1;j<=i+1;j++){
                    if(j>=0 && j <=(RowArraySpecialList.Length -1)){
                        foreach(var myNumber in RowArrayList[j]){
                            bool isAdjecent = myNumber.checkisAdjacent(specialSign.Value);
                            
                            int value =isAdjecent ? myNumber.numberValue : 0;
                            sum += myNumber.isCountedAndAdjecent ? 0 : value;
                            myNumber.isCountedAndAdjecent = isAdjecent;

                            if(specialSign.Key == "*" && isAdjecent){
                                tmpGearRatio *= myNumber.numberValue;
                                countIfGear+=1;
                            }
                        }
                    }
                }
                if(countIfGear == 2){
                    gearratio += tmpGearRatio;
                }                
            }
        }
        
        Console.WriteLine("sum: " + sum);    
        Console.WriteLine("Gearratios: " + gearratio);
    }    
}