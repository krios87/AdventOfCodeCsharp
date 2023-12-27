using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

class hailStone{
    public long A = 0;

    public long B = 0;

    public long C = 0;

    public long px = 0;

    public hailStone(long x, long y, long z, long velX, long velY, long velZ){
        /*
        we have line on this equation:
        x = x0 + it 
        y = y0 + jt
        ax + by = c ->
        a= i, b = -j, c = i*y0-j*x0
        i*y -j*x +y0*i-j*x0 = 0
        */
            A = -velY;
            B = velX;
            C = velX*y-velY*x;
            px = x;
    }
}

class DAY18
{
  
  static long minValue2 = 200000000000000;//7;//200000000000000;
  static long maxValue2 = 400000000000000;//27;//400000000000000;
 
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day24.txt");  
        List<hailStone> hailStones = new();

        foreach(var line in lines){

            long[] startcoord = line.Split('@')[0].Split(',').Select(Int64.Parse).ToArray();
            long[] speed = line.Split('@')[1].Split(',').Select(Int64.Parse).ToArray();
            hailStones.Add(new hailStone(startcoord[0],startcoord[1],startcoord[2],speed[0],speed[1],speed[2]));
        }


        long count = 0;
        for(int i = 0; i< hailStones.Count-1; i++){
            for(int j=i+1; j<hailStones.Count; j++){
                double A1 = hailStones[i].A;
                double B2 = hailStones[j].B;
                double A2 = hailStones[j].A;
                double B1 = hailStones[i].B;
                double C1 = hailStones[i].C;
                double C2 = hailStones[j].C;
                double Determinant = A1*B2 - A2*B1;

                //intersection of two lines if determinant != 0
                if(Determinant != 0){
                    double xPoint = (B2*C1-B1*C2)/ (double)Determinant;
                    double yPoint = (A1*C2-A2*C1)/ (double)Determinant;
                    
                    if(xPoint > minValue2 && xPoint < maxValue2 && yPoint > minValue2 && yPoint < maxValue2){
                        count++;                        
                        //t = (x-x0)/i
                        double time1 =  (xPoint-hailStones[i].px)/(double)hailStones[i].B;
                        double time2 =  (xPoint-hailStones[j].px)/(double)hailStones[j].B;
                        if(time1 <= 0 || time2 <= 0){
                            count--;
                        }
                    }
                }

            }
        }
        Console.WriteLine("count: " + count);
    }
}