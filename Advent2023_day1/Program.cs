using System;
using System.Reflection.Metadata.Ecma335;

class DAY1
{

   static int findFirstOrLastDigitWritten(string substring, bool findFirst = true){
    string[] stringdigits = {"none", "one", "two" ,"three", "four", "five", "six", "seven", "eight", "nine"};
    int bestindex = findFirst ? 10000 :-1;
    int returnval = -1;

    
    for(int i = 1; i <stringdigits.Length; i++){
        int digit  = substring.IndexOf(stringdigits[i]);
        //Console.WriteLine(digits[i]);
        if(digit >=0){
            if(findFirst){
                if(digit < bestindex){
                    bestindex = digit;
                    returnval = i;
                }
            }
            else{
                if(digit > bestindex){
                    bestindex = digit;
                    returnval = i;
                }
            }
            
        }
        
    }
//Console.WriteLine(" ");
    


    return returnval;
    } 

    static void Main(string[] args)
    {
        
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2023_day1.txt");

        int[] calibrationNumbers = new int[lines.Length];


        for(int i=0; i< lines.Length;i++){
            bool firstDigitFound = false;
            bool lastDigitFound = false;
            int firstDigit = 0;
            int lastDigit = 0;
            int firstDigitPos = 0;
            int lastDigitPos = 0;
            string rowString = lines[i];
            int rowLength = rowString.Length;
            for(int j=0;j<rowLength;j++){
                
                if(!firstDigitFound){
                    if(Char.IsDigit(rowString[j])){
                        firstDigit =  rowString[j]-'0';
                        firstDigitPos = j;
                        firstDigitFound = true;
                    }
                    
                }
                if(!lastDigitFound){
                    int jReverse = rowLength-j-1;
                    if(Char.IsDigit(rowString[jReverse])){
                        lastDigit = rowString[jReverse]-'0';
                        lastDigitPos = jReverse;
                        lastDigitFound = true;
                    }
                }
                if((firstDigitFound && lastDigitFound) || (j == (rowLength-1))){
                    //lastDigitPos = lastDigitPos == 0 ? rowLength -1 : lastDigitPos;
                    Console.WriteLine("first before: " + firstDigit + " and " + lastDigit);
                    Console.WriteLine(rowString);
                    int firstDigitText = findFirstOrLastDigitWritten(rowString.Substring(0,firstDigitFound ? firstDigitPos : (j+1)));
                    firstDigit = firstDigitText >= 0 ? firstDigitText : firstDigit;
                    //Console.WriteLine("VAL " + lastDigitPos);
                    int lastDigitText = findFirstOrLastDigitWritten(rowString.Substring(lastDigitFound ? lastDigitPos : 0),false);
                    lastDigit = lastDigitText >= 0 ? lastDigitText : lastDigit;
                    Console.WriteLine("first after: " + firstDigit + " and " + lastDigit);
                    break;
                }
            }
            calibrationNumbers[i] = firstDigit*10 + lastDigit;
            //Console.WriteLine("sum i: " + calibrationNumbers[i]);
        }

        //part1:
        Console.WriteLine("summa: " + calibrationNumbers.Sum());
    
    Console.WriteLine("lines: " + lines.Length);
    
    }

    //regex: (?=(0|1|2|3|4|5|6|7|8|9|one|two|three|four|five|six|seven|eight|nine))


    
}