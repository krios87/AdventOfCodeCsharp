using System;

class DAY13
{


    static bool checkbusespart1(ref string[] bussesString, ref ulong BusID, ref ulong earliestTime, ulong earliest, ulong starttime){
        bool busfound = false;

        foreach(string bus in bussesString){
            if(bus != "x"){
                ulong i = Convert.ToUInt64(bus);
                if(earliest%i == 0){
                    BusID = i;
                    earliestTime = earliest-starttime;
                    busfound = true;
                }
            }else{

            }
        }

        return busfound;

    }

    static void Main(string[] args)
    {
        
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day13.txt");

        ulong earliest = Convert.ToUInt64(lines[0]);

        string[] bussesString = lines[1].Split(',');

    

        Dictionary<ulong,ulong> busesAndTimes = new Dictionary<ulong, ulong>();
        ulong add = 0;
        foreach(string bus in bussesString){
            if(bus != "x"){
                busesAndTimes.Add(Convert.ToUInt64(bus),add);
                //Console.WriteLine(busesAndTimes.LastOrDefault().Key + " " + busesAndTimes.LastOrDefault().Value);
            }
            add++;
        }

        bool busfound = false;

        ulong BusID = 0;
        ulong earliestTime = 0;
        ulong starttime = earliest;
        while(!busfound){            
            busfound = checkbusespart1(ref bussesString, ref BusID, ref earliestTime, earliest, starttime);
            earliest+= 1;
            
        }

        //part2:
        

        ulong time = 0;
        ulong step = 1;

        foreach(var pair in busesAndTimes){
            while((time + pair.Value) % pair.Key != 0){  
                time += step;
            }
            step *= pair.Key; 
            //search first bus, then when it is found search only every modulo of that one,
            // since we know that every modulo of that is a solution. then continue like that, 
            //so we dont need to search much!! So every time you multiply step with the ID which is solution for last one
            //and since we go with steps based on previous modulo of previous busses we know they are the solution!
            
        }
        //part1:
        Console.WriteLine("ID: " + BusID +" time: " + earliestTime + " mult: " + BusID * earliestTime);
        //part2:
        Console.WriteLine(" time: " + time);
    
    }
}