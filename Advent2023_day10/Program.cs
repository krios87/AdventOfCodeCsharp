using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.CompilerServices;


enum directionMove{
    DIRECTION_UP,
    DIRECTION_DOWN,
    DIRECTION_LEFT,
    DIRECTION_RIGHT
} ;

class DAY10
{
    public static directionMove getNewDirection(directionMove direction,char currentChar){
        return direction switch
        {
            directionMove.DIRECTION_UP => currentChar == '7' ? directionMove.DIRECTION_LEFT : currentChar == 'F' ? directionMove.DIRECTION_RIGHT : directionMove.DIRECTION_UP,
            directionMove.DIRECTION_DOWN => currentChar == 'J' ? directionMove.DIRECTION_LEFT : currentChar == 'L' ? directionMove.DIRECTION_RIGHT : directionMove.DIRECTION_DOWN,
            directionMove.DIRECTION_LEFT => currentChar == 'F' ? directionMove.DIRECTION_DOWN : currentChar == 'L' ? directionMove.DIRECTION_UP : directionMove.DIRECTION_LEFT,
            directionMove.DIRECTION_RIGHT => currentChar == '7' ? directionMove.DIRECTION_DOWN : currentChar == 'J' ? directionMove.DIRECTION_UP : directionMove.DIRECTION_RIGHT,
            _ => directionMove.DIRECTION_UP,
        };
    }

    public static (int,int) getNewCoord((int,int) coord, directionMove direction){
        return direction switch{
            directionMove.DIRECTION_UP => (coord.Item1,coord.Item2+1),
            directionMove.DIRECTION_DOWN => (coord.Item1,coord.Item2-1),
            directionMove.DIRECTION_LEFT => (coord.Item1-1,coord.Item2),
            directionMove.DIRECTION_RIGHT => (coord.Item1+1,coord.Item2),
            _ => (coord.Item1,coord.Item2),
        };
    }

    public static int countSteps(List<(int,int)> points,char[,] map, int startX, int startY, directionMove startDirection){
        int count = 1;
        points.Add(new (startX,startY)); //add start coord
        (int currentX,int currentY) = getNewCoord((startX,startY),startDirection);
        directionMove currentDirection = getNewDirection(startDirection,map[currentX,currentY]);
        while(!(currentX == startX && currentY == startY)){
            count++;
            (currentX,currentY) = getNewCoord((currentX,currentY),currentDirection);            
            currentDirection = getNewDirection(currentDirection,map[currentX,currentY]);
            points.Add(new (currentX,currentY)); //add point for part 2
        }
        return count;
    }
    //https://en.wikipedia.org/wiki/Shoelace_formula
    public static long calcAreaSholaceFormula(List<(int,int)> points){
        long area = 0;
        for(int i = 0; i < points.Count; i++){
            area += points[i].Item1*points[(i+1)%points.Count].Item2-points[(i+1)%points.Count].Item1*points[i].Item2;
        }        
        return Math.Abs(area/2);
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day10.txt");  
        int nbrOfXcoord = lines[0].Length;
        int nbrOfYcoord = lines.Length;
        char[,] map = new char[nbrOfXcoord,nbrOfYcoord];
        int startX = 0;
        int startY = 0;

        for(int i=0; i< nbrOfYcoord; i++){
            for(int j=0; j< nbrOfXcoord; j++){
                int Ycoord = nbrOfYcoord-i-1;////make sure coordinate 0,0 is in bottom left so invert Y since row is read from above
                map[j,Ycoord] = lines[i].ElementAt(j); 
                if(map[j,Ycoord] == 'S'){
                    startX = j;
                    startY = Ycoord;
                }
            }
        }      
        List<(int,int)> points = new(); //points for part 2
        //start stepping up
        int count = countSteps(points, map, startX, startY, directionMove.DIRECTION_UP);        
        Console.WriteLine("count:" + count/2); //half steps around is the solution

        //part2:
        long area = calcAreaSholaceFormula(points);
        //picks theroem (https://en.wikipedia.org/wiki/Pick's_theorem) i = A - b/2 +1 (this calculates interior points)
        Console.WriteLine("count part 2: " + (area - count/2 +1));
        
    }
}