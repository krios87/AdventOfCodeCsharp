using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;



class DAY18
{
    public static void calculateCoordinateStep1(string direction,ref int[,] matrix,ref int currentX,ref int currentY, int length){
        

        for(int i = 0; i< length; i ++){
            matrix[currentX,currentY] = 1;
        
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
    public static void fillInterior(ref int[,] matrix, int currentX, int currentY){
        if(matrix[currentX,currentY] == 1 || matrix[currentX,currentY] == 2){
            return;
        }
        else{
            matrix[currentX,currentY] = 2;
            fillInterior(ref matrix,currentX +1, currentY);
            fillInterior(ref matrix,currentX-1,currentY);
            fillInterior(ref matrix,currentX, currentY+1);            
            fillInterior(ref matrix, currentX, currentY-1);
        }
        
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day18.txt");  

        string[][] linesSplit = new string[lines.Length][];

        int[,] coordinates = new int[500,500];
        const int initialX = 150;
        const int initialY = 150;
        int currentX = initialX;
        int currentY = initialY;

        for(int i=0; i< lines.Length; i++){
            linesSplit[i] = lines[i].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        }      
        

        int maxY = 0;
        int minY = 500;
        int startX = 0;
        for(int i = 0; i< lines.Length; i++){
            calculateCoordinateStep1(linesSplit[i][0],ref coordinates,ref currentX,ref currentY, Convert.ToInt32(linesSplit[i][1]));
            maxY = currentY > maxY ? currentY : maxY;
            if(currentY < minY){
                minY = currentY;
                startX = currentX;
            }
        }

        int count = 0;
        fillInterior(ref coordinates,startX,minY+10);
        for(int i= maxY; i>=minY;i--){
            for(int j= 0; j<500;j++){
                if(coordinates[j,i]==1 || coordinates[j,i] ==2){
                 count++;
                }
            }     
        }

        Console.WriteLine("count part 1: " + count);

    }
}