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
            area += points[i].Item1*points[(i+1)%points.Count].Item2-points[(i+1)%points.Count].Item1*points[i].Item2;
        }
        
        return Math.Abs(area/2);
    }
    public static void calculateCoordinateStep1(List<(long,long)> points,string direction,ref int currentX,ref int currentY, ref long exteriorLength, long length){
        for(long i = 0; i< length; i ++){
            exteriorLength ++;
            points.Add(new (currentX,currentY));
            currentX += direction is "R" or "0" ? 1 : direction is "L" or "2" ? -1 : 0;
            currentY += direction is "U" or "3" ? 1 : direction is "D" or "1" ? -1 : 0;
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
        
        //part 1:
        List<(long,long)> Points = new();
        long boundrary = 0;
        for(int i = 0; i< lines.Length; i++){
            calculateCoordinateStep1(Points,linesSplit[i][0],ref currentX,ref currentY, ref boundrary, Convert.ToInt64(linesSplit[i][1]));
        }
        long area = calcAreaSholaceFormula(Points);

        //picks theroem (https://en.wikipedia.org/wiki/Pick's_theorem)
        //to calculate i = A - b/2 +1 (this calculates interior points, but we want to include border, b, so we add b) => i + b = A + b/2 +1 
        Console.WriteLine("count part 1: " + (boundrary/2 + area +1));

        //part 2:
        List<(long,long)> Points2 = new();
        boundrary = 0;
        currentX = initialX;
        currentY = initialY;
        for(int i = 0; i< lines.Length; i++){
            int.TryParse(linesSplit[i][2].Substring(2,5), System.Globalization.NumberStyles.HexNumber, null, out int length); //take 5 digits for length
            calculateCoordinateStep1(Points2,linesSplit[i][2].Substring(7,1),ref currentX,ref currentY, ref boundrary, length); //last digit is direction
        }

        area = calcAreaSholaceFormula(Points2);

        Console.WriteLine("count part 2: " + (boundrary/2 + area +1));
    }
}