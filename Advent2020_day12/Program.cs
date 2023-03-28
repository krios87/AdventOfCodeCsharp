using System;

class DAY9
{

    static void Main(string[] args)
    {
        
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day12.txt");

        int degrees = 0;
        int x = 0;
        int y = 0;

        int WaypointX = 10;
        int WaypointY = 1;
        int x2 = 0;
        int y2 = 0;
        int quadrant = 1;

        foreach(string li in lines){
            char letter = li[0];
            int amount = Convert.ToInt32(li.Substring(1));

            switch(letter){

                case 'F':
                    y += (int) Math.Sin((double) degrees * Math.PI / 180.0) * amount;
                    x += (int) Math.Cos((double) degrees * Math.PI /180.0) * amount; 
                    break;

                case 'L':
                    degrees += amount;
                    break;

                case 'R':
                    degrees -= amount;
                    break;

                case 'N':
                    y += amount;
                    break;

                case 'S':
                    y -= amount;
                    break;

                case 'W':
                    x -= amount;
                    break;

                case 'E':
                    x += amount;
                    break;
            }


            //part 2:


            
            switch(letter){

                case 'F':
                    y2 += WaypointY * amount;
                    x2 += WaypointX * amount;
                    break;                

                case 'N':
                    WaypointY += amount;
                    break;

                case 'S':
                    WaypointY -= amount;
                    break;

                case 'W':
                    WaypointX -= amount;
                    break;

                case 'E':
                    WaypointX += amount;
                    break;

                default: //L or R:

                    if(amount == 180){
                        WaypointX = -1 * Math.Sign(WaypointX)*Math.Abs(WaypointX);
                        WaypointY = -1 * Math.Sign(WaypointY)*Math.Abs(WaypointY);

                    } else {
                        int tmp = WaypointX;
                        int sign = ((amount == 90) && (letter == 'R')) || ((amount == 270) && (letter == 'L')) ? -1 : 1;
                        WaypointX = - sign * WaypointY; //we always rotate 90 degrees and fix relationship is between x and y coordinate
                        WaypointY = sign *tmp; //sin(a + 90) = cos(a), cos(a +90) = - sin(a), sin(a - 90) = - cos(a), cos(a-90) = sin(a)
                    }

                    break;            
            }    
        }
        
        int manhattan = Math.Abs(x)+Math.Abs(y);

        Console.WriteLine(x + " " + y + " manhattan1: " + manhattan);

        int manhattan2 = Math.Abs(x2)+Math.Abs(y2);

        Console.WriteLine(x2 + " " + y2 + " manhattan2: " + manhattan2);
        
    }

}
