using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;



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
}

class DAY13
{

    static int findMirror(List<long> RowOrColumnList, int NbrOfRowOrColumn){
        for(int i = 0; i < NbrOfRowOrColumn; i++ ){
                int previousI = i - 1;
                bool Found = false;
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
                        return i;
                    }               
            }

            return 0;

    }

    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day13.txt");         
        int NbrOfRows = lines.Length;
        int NbrOfColumns = lines[0].Length;

        List<myPattern> Patterns = new();
        bool newPattern = true;
        int ListIndex = 0;
        int startIndex = 1;
        //FindRowValues
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

        //find Column values
        for(int i = 0; i <Patterns.Count; i++){
            for(int colindex = 0; colindex<Patterns.ElementAt(i).nbrOfColumns; colindex++){
                long columnValue = 0;
                for(int j = 0 ; j<Patterns.ElementAt(i).nbrOfRows; j++){
                    columnValue += (lines[Patterns.ElementAt(i).startIndex+j][colindex].Equals('#') ? 1 : 0) << j;
                    
                }
                Patterns.ElementAt(i).ColumnList.Add(columnValue);
            }
            
        }

        long countTotal = 0;
        foreach(var pattern in Patterns){
            countTotal += findMirror(pattern.ColumnList,pattern.nbrOfColumns) + 100*findMirror(pattern.RowList,pattern.nbrOfRows);     
        }      
        Console.WriteLine(countTotal);
    }

    
}