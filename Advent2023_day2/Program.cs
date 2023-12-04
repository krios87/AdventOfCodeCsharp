using System;

public class Bag {
    private int minNbrOfBlue = 0;
    private int minNbrOfRed = 0;
    private int minNbrOfGreen = 0;

    public void checkBag(int nbrOfBlue, int nbrOfRed, int nbrOfGreen){
        minNbrOfBlue = nbrOfBlue > minNbrOfBlue ? nbrOfBlue : minNbrOfBlue;
        minNbrOfRed = nbrOfRed > minNbrOfRed ? nbrOfRed : minNbrOfRed;
        minNbrOfGreen = nbrOfGreen > minNbrOfGreen ? nbrOfGreen : minNbrOfGreen;
    }

    public bool gamePossible(int nbrOfBlue, int nbrOfRed, int nbrOfGreen){
        return (nbrOfBlue >= minNbrOfBlue) && (nbrOfRed >= minNbrOfRed) && (nbrOfGreen >= minNbrOfGreen);
    }

    public int igotThePower(){
        return minNbrOfBlue * minNbrOfGreen * minNbrOfRed;
    }
}
class DAY2
{
    static int numberOfColour(string randomCheck, string colour){
        int index = randomCheck.IndexOf(colour);
        return (index > 0) ? Convert.ToInt32(randomCheck.Substring(index-3,3)) : 0;
    }
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day2.txt");
        Bag[] bag = new Bag[100];
        int sum = 0;
        int power = 0;
        for(int i=0; i< lines.Length;i++){
            string[] randomChecks = lines[i].Substring(7).Split(';');
            bag[i] = new Bag();            
            foreach(string randomCheck in randomChecks){
                int nbrOfBlue = numberOfColour(randomCheck, "blue"); 
                int nbrOfRed = numberOfColour(randomCheck, "red"); 
                int nbrOfGreen = numberOfColour(randomCheck, "green");
                bag[i].checkBag(nbrOfBlue,nbrOfRed,nbrOfGreen);                
            }
            sum += bag[i].gamePossible(14,12,13) ? (i+1) : 0;
            power += bag[i].igotThePower();
        }
        //part1:
        Console.WriteLine("summa: " + sum );    
        //part2:
        Console.WriteLine("power: " + power);    
    }    
}