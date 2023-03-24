using System;

  class MyProgram
  { 
    class MyCommand{
        public string operation;
        public char sign;
        public int number;

        public MyCommand(string operation, char sign, int number){
            this.operation = operation;
            this.sign = sign;
            this.number = number;
        }
    } 

    static int countAcc(ref List<MyCommand> commandlist, ref bool success){
        int currentIndex = 0;
        int acc = 0;
        success = true;
        bool [] executedArray = new bool[commandlist.Count];  //note are initialized to false already
        while(!(currentIndex >= commandlist.Count)){
            
            if(executedArray[currentIndex]){
                success = false;
                
                break;
            }
            executedArray[currentIndex] = true;
            
            switch(commandlist[currentIndex].operation){

                case "nop":
                    currentIndex++;
                break; 

                case "acc":
                   acc = acc + (commandlist[currentIndex].sign == '+' ? 1: -1)*commandlist[currentIndex].number;
                   currentIndex++;
                   break;

                case "jmp":
                    currentIndex = currentIndex + (commandlist[currentIndex].sign == '+' ? 1: -1)*commandlist[currentIndex].number;
                break;
            }
        }
        return acc;
    }

    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day8.txt");

        var commandlist = new List<MyCommand>();
        foreach(string line in lines){
            string[] parts = line.Split(' ');
            string operation = parts[0];
            char sign = parts[1][0];
            int number = Convert.ToInt32(parts[1].Substring(1));
            commandlist.Add(new MyCommand(operation,sign,number));
        }

        
        bool success = false;
        int accCount = 0;
        
        //part1:
        //accCount = countAcc(ref commandlist,ref success);
        //end part1

        //part 2:
        for(int i = 0; i< commandlist.Count(); i++){ //try changing jmp and nop for each other to get success

            if(commandlist[i].operation == "jmp" || commandlist[i].operation == "nop"){
                commandlist[i].operation = commandlist[i].operation == "jmp" ? "nop" : "jmp";
                accCount = countAcc(ref commandlist,ref success);
                if(success){
                    break;
                }
                commandlist[i].operation = commandlist[i].operation == "jmp" ? "nop" : "jmp";

            }
        }
        //end part 2

        Console.WriteLine(accCount);
        Console.WriteLine("success: " + success);
    }

    

  }