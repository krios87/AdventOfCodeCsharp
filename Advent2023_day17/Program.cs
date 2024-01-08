using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


//I made Dijkstras algorithm, but failed to add constraint, then i instead got inspired by another solution on the internet, but dont work yet..
enum Direction{

    DIRECTION_UNDEFINED,
    DIRECTION_UP,
    DIRECTION_DOWN,
    DIRECTION_LEFT,
    DIRECTION_RIGHT
  };

class Position{


    public int row = 0;

    public int column = 0;

    public Position(int row, int column){
        this.row = row;
        this.column = column;
    }
}
class Node{

    public Node(Direction dir, Position pos, int steps, int distance){
        this.currentDirection = dir;
        this.nodePosition = pos;
        this.stepsOneDir = steps;
        this.distanceToStart = distance;
    }

    public bool visited = false;

    public int distanceToStart = int.MaxValue;

    public Position nodePosition = new(0,0);

    public int Row{
        get {return nodePosition.row;}
        set { nodePosition.row = value;}
    }

    public int Col{
        get {return nodePosition.column;}
        set { nodePosition.column = value;}
    }

    public Direction currentDirection = Direction.DIRECTION_UNDEFINED;

    public int stepsOneDir = 0;


}



class DAY17
{
    public static bool PointInGrid(Position startpoint, bool ccw, Direction dir, int[,] grid, out Node newNode, int steps, bool forward){
        newNode = new Node(Direction.DIRECTION_UNDEFINED,new Position(startpoint.row,startpoint.column),steps,0);

        bool inGrid = false;

        switch(dir){

            case Direction.DIRECTION_UP:     
            if(forward){
                newNode.Row--;
            }   
            else{
                newNode.Col += (ccw ? -1 : 1);
                newNode.currentDirection = ccw ? Direction.DIRECTION_LEFT : Direction.DIRECTION_RIGHT;                
            }        
                
                break;

            case Direction.DIRECTION_DOWN:
            if(forward){
                newNode.Row++;
            }   
            else{
                newNode.Col += (ccw ? 1 : -1);
                newNode.currentDirection = ccw ? Direction.DIRECTION_RIGHT : Direction.DIRECTION_LEFT;
            }
                break;

            case Direction.DIRECTION_LEFT:
            if(forward){
                newNode.Col--;
            }   
            else{
                newNode.Row += (ccw ? 1 : -1);
                newNode.currentDirection = ccw ? Direction.DIRECTION_DOWN : Direction.DIRECTION_UP;
            }
                break;

            case Direction.DIRECTION_RIGHT:
            if(forward){
                newNode.Col++;
            }   
            else{
                newNode.Row += (ccw ? -1 : 1);
                newNode.currentDirection = ccw ? Direction.DIRECTION_UP : Direction.DIRECTION_DOWN;
            }
                break;

        }
        inGrid = (newNode.Col >= 0 && newNode.Col < grid.GetLength(1)) && (newNode.Row >= 0 && newNode.Row < grid.GetLength(0));

        if(inGrid){
            Console.WriteLine("row: "+ newNode.Row +" Col: "+ newNode.Col);
            newNode.distanceToStart = grid[newNode.Row,newNode.Col];  
        }
        return inGrid;
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day17.txt");         
        int NbrOfRows = lines.Length;
        int NbrOfColumns = lines[0].Length;

        int[,] heatMap = new int[NbrOfRows,NbrOfColumns];

        PriorityQueue<Node,int> nodeQueue = new PriorityQueue<Node,int>(); 


        for(int i = 0; i< NbrOfRows; i++){
            for(int j = 0; j< NbrOfColumns; j++){
                heatMap[i,j] = Convert.ToInt32(lines[i][j]-'0');
            }
        }

        //https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

        int countCost = 0;
        nodeQueue.Enqueue(new Node(Direction.DIRECTION_DOWN,new Position(0,0),0,0),0);
        nodeQueue.Enqueue(new Node(Direction.DIRECTION_RIGHT,new Position(0,0),0,0),0);
        Console.WriteLine("nodecount:  " + nodeQueue.Count);
    
        HashSet<(int,int, int)> visitedNodes = new HashSet<(int,int,int)>();
        
        while(nodeQueue.Count != 0){
            Console.WriteLine("nodecount:  " + nodeQueue.Count);
            Node currentNode = nodeQueue.Dequeue();
            if(currentNode.Row == NbrOfRows-1 && currentNode.Col == NbrOfColumns -1){
                countCost = currentNode.distanceToStart;
                break;
            }

            if(visitedNodes.Contains((currentNode.nodePosition.row,currentNode.nodePosition.column,currentNode.stepsOneDir))){
                continue;
            }
            //Console.WriteLine("Node:  "+ currentNode.nodePosition.row + ", " + currentNode.nodePosition.column + " steps: " + currentNode.stepsOneDir);
            visitedNodes.Add((currentNode.nodePosition.row,currentNode.nodePosition.column,currentNode.stepsOneDir));
            
            //if CCW add in queue
            if(PointInGrid(currentNode.nodePosition,true,currentNode.currentDirection,heatMap, out Node newNode,1,false)){
                newNode.distanceToStart += currentNode.distanceToStart;
                nodeQueue.Enqueue(newNode,newNode.distanceToStart);
            }                

            //if CW add in queue
            if(PointInGrid(currentNode.nodePosition,false,currentNode.currentDirection,heatMap, out newNode,1,false)){
                newNode.distanceToStart += currentNode.distanceToStart;
                nodeQueue.Enqueue(newNode,newNode.distanceToStart);
            }

            if(currentNode.stepsOneDir < 3 && PointInGrid(currentNode.nodePosition,false,currentNode.currentDirection,heatMap, out newNode,currentNode.stepsOneDir + 1,true))
            {
                newNode.distanceToStart += currentNode.distanceToStart;
                nodeQueue.Enqueue(newNode,newNode.distanceToStart);
            }      
            
        }

        Console.WriteLine("countCost: " + countCost);
    }
}