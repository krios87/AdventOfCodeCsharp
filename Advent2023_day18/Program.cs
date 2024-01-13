using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.CompilerServices;



class DAY18
{
    //https://en.wikipedia.org/wiki/Shoelace_formula
    public static long calcAreaSholaceFormula(List<(long,long)> points){

        long area = 0;
        for(int i = 0; i < points.Count; i++){
            
            if(i==(points.Count -1)){
                area += points[i].Item1*points[0].Item2-points[0].Item1*points[i].Item2; // determinants
            }
            else{
                area += points[i].Item1*points[(i+1)].Item2-points[(i+1)].Item1*points[i].Item2; // determinants
            }

        //Console.WriteLine("Point: " + points[i].Item1 + "," + points[i].Item2 + " area: " + area);
        }
        
        return Math.Abs(area/2);
    }
    public static void calculateCoordinateStep1(List<(long,long)> points,string direction,ref int currentX,ref int currentY, ref long exteriorLength, long length){
        

        for(long i = 0; i< length; i ++){
            exteriorLength ++;

            points.Add(new (currentX,currentY));
        
            switch(direction){

                case "R":
                    currentX++;
                    break;

                case "L":
                    currentX--;
                    break;

                case "U":
                    currentY++;
                    break;

                case "D":
                    currentY--;
                    break;
            }
        }
    }

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day18.txt");  

        string[][] linesSplit = new string[lines.Length][];
        const int initialX = 150;
        const int initialY = 150;
        int currentX = initialX;
        int currentY = initialY;

        for(int i=0; i< lines.Length; i++){
            linesSplit[i] = lines[i].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        }      
        
        List<(long,long)> Points = new();

        long exteriorLength = 0;
        for(int i = 0; i< lines.Length; i++){
            calculateCoordinateStep1(Points,linesSplit[i][0],ref currentX,ref currentY, ref exteriorLength, Convert.ToInt64(linesSplit[i][1]));
        }
        long area = calcAreaSholaceFormula(Points);

        //picks theroem (https://en.wikipedia.org/wiki/Pick's_theorem)
        //to calculate i = A - b/2 +1 (this calculates interior points, but we want to include border, b, so we add b) => i + b = A + b/2 +1 
        Console.WriteLine("count part 1: " + (exteriorLength/2 + area +1) + " area: " + area + " edge: " + exteriorLength);

        //part 2:
        List<(long,long)> Points2 = new();
        exteriorLength = 0;
        currentX = initialX;
        currentY = initialY;
        for(int i = 0; i< lines.Length; i++){
            int.TryParse(linesSplit[i][2].Substring(2,5), System.Globalization.NumberStyles.HexNumber, null, out int length); //take 5 digits for length
            string direction = linesSplit[i][2].Substring(7,1) switch //last digit is direction
            {
                string s when s == "0" => "R",
                string s when s == "1" => "D",
                string s when s == "2" => "L",
                string s when s == "3" => "U",
                _ => "E"
            };
            calculateCoordinateStep1(Points2,direction,ref currentX,ref currentY, ref exteriorLength, length);
        }

        area = calcAreaSholaceFormula(Points2);
        


        


        Console.WriteLine("count part 2: " + (exteriorLength/2 + area +1) + " area: " + area + " edge: " + exteriorLength);

    }
}