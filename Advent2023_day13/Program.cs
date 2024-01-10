using System;

class myPattern{
    public int startIndex = 0;
    public int nbrOfRows = 0;
    public int nbrOfColumns = 0;
    public List<long> RowList = new();
    public List<long> ColumnList = new();
    public myPattern(int columns, int startind){
        nbrOfColumns = columns;
        startIndex = startind;
    }
    public int reflectionLineRow = 0;
    public int reflectionLineColumn = 0;
}

class DAY13
{
    static int findMirror(List<long> RowOrColumnList, int NbrOfRowOrColumn, int previousvalue){
        bool Found = false;
        for(int i = 0; i < NbrOfRowOrColumn; i++ ){
            int previousI = i - 1;
            if(previousI >= 0 && RowOrColumnList.ElementAt(i) == RowOrColumnList.ElementAt(previousI)){
                int nextI = i + 1;
                Found = true;
                while(previousI > 0 && nextI < NbrOfRowOrColumn){
                    previousI--;
                    Found = Found && (RowOrColumnList.ElementAt(nextI) == RowOrColumnList.ElementAt(previousI));
                    nextI++;
                    if(!Found){
                        break;
                    }
                }                        
            }
            if(Found){
                if(previousvalue == 0 || i != previousvalue)
                {
                    return i;
                }
                else{
                    Found = false;
                }
                
            }               
        }            
        return 0;
    }

    static int FindMirrowPart2(List<long> RowOrColumnList, int NbrOfRowOrColumn, int NbrOfBitShifts, int previousvalue){
            for(int i= 0; i < NbrOfRowOrColumn; i++){
                for(int k = 0; k < NbrOfBitShifts; k++){                    
                    RowOrColumnList[i] = RowOrColumnList[i] ^ (0x1 << k);
                    int valueRoworColumn = findMirror(RowOrColumnList,NbrOfRowOrColumn,previousvalue);                    
                    RowOrColumnList[i] = RowOrColumnList[i] ^ (0x1 << k);
                    if(valueRoworColumn != 0){
                        return valueRoworColumn;
                    }                    
                }           
            }
            return 0;
    }

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day13.txt");         

        List<myPattern> Patterns = new();
        bool newPattern = true;
        int ListIndex = 0;
        int startIndex = 1;
        //add row values (represent # as 1 and . as 0 in binary for one value per row)
        for(int i= 0; i < lines.Length; i++){
            if(lines[i]==""){
                newPattern = true;
                Patterns.ElementAt(ListIndex).nbrOfRows = i-startIndex;
                ListIndex++;
                continue;
            }            
            if(newPattern){
                startIndex = i;
                Patterns.Add(new myPattern(columns:lines[i].Length,startind:startIndex));                
                newPattern = false;
            }

            long rowValue = 0;
            for(int j=0; j < lines[i].Length; j++){
                rowValue += (lines[i][j].Equals('#') ? 1 : 0) << j;
            }
            Patterns.ElementAt(ListIndex).RowList.Add(rowValue);
        }

        //for last one:
        Patterns.ElementAt(ListIndex).nbrOfRows = lines.Length-startIndex;
        ListIndex++;

        //add Column values
        for(int i = 0; i <Patterns.Count; i++){
            for(int colindex = 0; colindex<Patterns.ElementAt(i).nbrOfColumns; colindex++){
                long columnValue = 0;
                for(int j = 0 ; j<Patterns.ElementAt(i).nbrOfRows; j++){
                    columnValue += (lines[Patterns.ElementAt(i).startIndex+j][colindex].Equals('#') ? 1 : 0) << j;                    
                }
                Patterns.ElementAt(i).ColumnList.Add(columnValue);
            }            
        }

        int countTotal = 0;
        foreach(var pattern in Patterns){
            pattern.reflectionLineColumn = findMirror(pattern.ColumnList,pattern.nbrOfColumns,0);
            pattern.reflectionLineRow = findMirror(pattern.RowList,pattern.nbrOfRows,0);
            countTotal += pattern.reflectionLineColumn + 100*pattern.reflectionLineRow;     
        }      
        Console.WriteLine("part 1: " + countTotal);

        //part2:
        countTotal = 0;
        foreach(var pattern in Patterns){
            countTotal += 100*FindMirrowPart2(pattern.RowList, pattern.nbrOfRows, pattern.nbrOfColumns, pattern.reflectionLineRow) +
                          FindMirrowPart2(pattern.ColumnList, pattern.nbrOfColumns, pattern.nbrOfRows, pattern.reflectionLineColumn);
        }      
        
        Console.WriteLine("part 2: " + countTotal);
    }

    
}