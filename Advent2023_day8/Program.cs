using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.CompilerServices;



class MyNode{

    public string name = "";

    public MyNode? RightNode;

    public MyNode? LeftNode;

    public MyNode(string name){
        this.name = name;
    }
}


class DAY8
{

    public static MyNode GetNode(char leftorRight, MyNode currentNode){
        return leftorRight == 'L' ? currentNode.LeftNode : currentNode.RightNode;
    }

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day8.txt");  

        //create Nodes without pointer to other nodes
        Dictionary<string,MyNode> allNodesDict = new();
        int startIndex = 0;
        List<MyNode> ghostNodes = new();
        for(int i=2; i< lines.Length; i++){
            allNodesDict.Add(lines[i][..3], new MyNode(lines[i][..3])); //do regex instead to find nodes
            if(lines[i][..3].Contains("AAA")){
                startIndex = i-2;
            }
            if(lines[i][2] == 'A'){
                ghostNodes.Add(allNodesDict.Last().Value);
            }
        }      

        //add pointers to Right and left nodes
        for(int i=0; i<allNodesDict.Count;i++){
            string[] TwoNodes = lines[i+2].Substring(lines[i+2].IndexOf('(') + 1,8).Split(',',StringSplitOptions.TrimEntries); 
            string leftNode = TwoNodes[0];
            string rightNode = TwoNodes[1];
            allNodesDict.ElementAt(i).Value.LeftNode = allNodesDict[leftNode];
            allNodesDict.ElementAt(i).Value.RightNode = allNodesDict[rightNode];            
        }
        

        MyNode currentNode = allNodesDict.ElementAt(startIndex).Value;
        int indexNode = 0;
        int steps = 0; 
        while(currentNode.name != "ZZZ"){
            currentNode = GetNode(lines[0][indexNode%lines[0].Length],currentNode);
            indexNode++;
            steps++;
        }

        Console.WriteLine("steps part 1: " + steps);   

        //part 2
        indexNode = 0;
        List<int> stepsList = new();
        
        //calculate for each how many steps to an end, assuming it will be a repeating cycle we can calculate least common multiple for all ghosts (LCM)
        for(int i = 0 ; i< ghostNodes.Count; i++){
            bool foundEnd = false;
            steps = 0;
            while(!foundEnd){                    
                ghostNodes[i] = GetNode(lines[0][indexNode%lines[0].Length],ghostNodes[i]);
                foundEnd = ghostNodes[i].name[2] == 'Z';
                steps++;
                indexNode++;
            }
            stepsList.Add(steps);
        }            
        
        Console.WriteLine("part 2: ");
        foreach (var value in stepsList)
        {
           Console.Write(" " + value);  //calculate LCM for these value (no library in #C, i used: https://www.calculatorsoup.com/calculators/math/lcm.php)
        }     
    }
}