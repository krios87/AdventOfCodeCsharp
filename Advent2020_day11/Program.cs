using System;

class DAY11
{

    static int countel(ref char[,] map, char character, int i, int j, int maxRow, int maxColumn){
        int count=0;

        if(i < 0 || i > (maxRow-1) || j < 0 || j> (maxColumn-1)){
            count = 0;
        } else{
            count += (map[i,j] == character) ? 1 : 0;
        }
        return count;

    }


    static int countAlladjacentSolution1(ref char[,] map, char character, int i, int j, int maxRow, int maxColumn){
        int count = 0;
        for(int dirx = -1; dirx<2; dirx++){
            for(int diry= -1; diry<2; diry++){
                if(!(dirx==0 & diry== 0)){
                    count += countel(ref map,character,i+dirx,j+diry,maxRow,maxColumn);
                }
            }
        }

        return count;
    }

    static int counteldirection(ref char[,] map, char character, int i, int j, int maxRow, int maxColumn, int dirX, int dirY){
        int count=0;

        if(i < 0 || i > (maxRow-1) || j < 0 || j> (maxColumn-1)){
            count = 0;
        } else{
            if(map[i,j] == character){
                return 1;
            }
            else if(map[i,j] == 'L'){   //if empty it cannot see occupied!
                return 0;
            }else{
                count = counteldirection(ref map, character, i+dirX, j+dirY, maxRow, maxColumn, dirX, dirY);
            }
        }
        return count;

    }


    static int countAlladjacentSolution2(ref char[,] map, char character, int i, int j, int maxRow, int maxColumn){
        int count =  0;
        
        for(int dirx = -1; dirx<2; dirx++){
            for(int diry= -1; diry<2; diry++){
                if(!(dirx==0 & diry== 0)){
                    count += counteldirection(ref map,character,i+dirx,j+diry,maxRow,maxColumn,dirx,diry);
                }
            }
        }

        return count;
    }

    static void Main(string[] args)
    {
        
        //new line etc. is not included so all strings are the same length!!
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day11.txt");
        
        int NbrOfRows = lines.Count();
        int NbrOFColumns = lines[0].Length;
        
        
        char[,] map = new char[NbrOfRows,NbrOFColumns];
        char[,] nextmap = new char[NbrOfRows,NbrOFColumns]; //if we do nextmap = map we get shallow copy and copye references!
        
       for(int i = 0; i < NbrOfRows; i++){

            for(int j = 0; j< NbrOFColumns; j++){
                map[i,j] = lines[i].ElementAt(j);
                nextmap[i,j] = lines[i].ElementAt(j);
            }
        }

        nextmap = map.Clone() as char[,];  //copy copies values, if we have pointers this is not deep enough if we want the objects.
        
        bool WasAChange = true;

        while(WasAChange){
            WasAChange = false;
            for(int i = 0; i < NbrOfRows; i++){

                for(int j = 0; j< NbrOFColumns; j++){
                    
                    switch(map[i,j]){

                        case 'L': //available                            
                            if(countAlladjacentSolution2(ref map,'#',i,j,NbrOfRows,NbrOFColumns) == 0){
                                nextmap[i,j] = '#'; 
                                WasAChange = true;
                            } 
                        break;

                        case '#': //occopied
                            if(countAlladjacentSolution2(ref map,'#',i,j,NbrOfRows,NbrOFColumns) >= 5){ //4 for solution 1, 5 for 2
                                nextmap[i,j] = 'L'; 
                                WasAChange = true;
                            }       
                        break;
                    }                    
                }
            }
             map = nextmap.Clone() as char[,];
        }

        long counter = 0;
        for(int i = 0; i < NbrOfRows; i++){

            for(int j = 0; j< NbrOFColumns; j++){
                if(map[i,j] == '#'){                    
                    counter++;
                }
                //Console.Write(map[i,j]);
            }
           // Console.Write('\n');
        }

        Console.WriteLine(counter);
    }


}