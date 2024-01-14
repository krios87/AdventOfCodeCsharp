using System;

class MyNode{
    public string ?Name { get; set; }
    public MyNode ?RightNode { get; set; }
    public MyNode ?LeftNode { get; set; }
    public MyNode(string name) => Name = name;
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
        List<MyNode> allNodesList = new();
        int startIndex = 0;
        List<MyNode> ghostNodes = new();
        for(int i=2; i< lines.Length; i++){
            allNodesList.Add(new MyNode(lines[i][..3])); //do regex instead to find nodes
            if(lines[i][..3].Contains("AAA")){
                startIndex = i-2;
            }
            if(lines[i][2] == 'A'){
                ghostNodes.Add(allNodesList.Last());
            }
        }      

        //add pointers to Right and left nodes
        for(int i=0; i<allNodesList.Count;i++){
            string[] TwoNodes = lines[i+2].Substring(lines[i+2].IndexOf('(') + 1,8).Split(',',StringSplitOptions.TrimEntries); 
            string leftNode = TwoNodes[0];
            string rightNode = TwoNodes[1];
            allNodesList.ElementAt(i).LeftNode = allNodesList.ElementAt(allNodesList.FindIndex(x => x.Name == leftNode));
            allNodesList.ElementAt(i).RightNode = allNodesList.ElementAt(allNodesList.FindIndex(x => x.Name == rightNode));            
        }        

        //part 1
        MyNode currentNode = allNodesList.ElementAt(startIndex);
        int indexNode = 0;
        int steps = 0; 
        while(currentNode.Name != "ZZZ"){
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
                foundEnd = ghostNodes[i].Name[2] == 'Z';
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