using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;


public class CustomComparer : IComparer<KeyValuePair<string, long>> {

    public int Compare(KeyValuePair<string,long> handA, KeyValuePair<string,long> handB) {

        //get type of hand:
        int handtypeA = getCardHandType(handA.Key);
        int handtypeB = getCardHandType(handB.Key);

        if(handtypeA == handtypeB){
            return getcardValue(handA.Key,handB.Key);
        }
        else{
            return handtypeA < handtypeB ? 1 : -1;
        }
    }
    private string pattern5 =@"(.)(.*\1){4}";
    private string pattern4 =@"(.)(.*\1){3}";
    private string pattern3 =@"(.)(.*\1){2}";
    private string pattern2 =@"(.)(.*\1){1}";

    private string patternNewpair = @"([^X])(.*\1){1}";//replace X with something

    private int getCardHandType(string ?hand){
        
        Regex rex5 = new Regex(pattern5);
        Match match5 = rex5.Match(hand ?? "");

        if(match5.Success){
            return 6;
        }

        Regex rex4 = new Regex(pattern4);
        Match match4 = rex4.Match(hand ?? "");

        if(match4.Success){
            return 5;
        }

        Regex rex3 = new Regex(pattern3);
        Match match3 = rex3.Match(hand ?? "");

        if(match3.Success){
            char match3char = match3.Value[0];
            string patternFullHouse = patternNewpair.Replace('X',match3char);
            Regex rexFull = new Regex(patternFullHouse);
            Match matchFull = rexFull.Match(hand ?? "");            
            return matchFull.Success ? 4 : 3;
        }

        Regex rex2 = new Regex(pattern2);
        Match match2 = rex2.Match(hand ?? "");

        if(match2.Success){
            char match2char = match2.Value[0];
            string pattern2pair = patternNewpair.Replace('X',match2char);
            Regex rex2pair = new Regex(pattern2pair);
            Match match2pair = rex2pair.Match(hand ?? "");
            return match2pair.Success ? 2 : 1;
        }


        return 0; 
    }

    private int getcardValue(string ?handA, string ?handB)
    {
        for(int i =0; i<handA.Length;i++){
            if(getcard(handA[i]) > getcard(handB[i])){
                return -1;
            }
            else if(getcard(handA[i]) < getcard(handB[i])){
                return 1;
            }
        }
        return 0;
    }

    private int getcard(char c){

        return c switch 
        {
            'A' => 14, 'K' => 13, 'Q' =>  12, 'J' => 11, 'T' => 10,
            '9' =>  9, '8' => 8, '7' => 7, '6' =>  6, '5' =>  5, '4' =>  4, '3' =>  3, '2' =>  2, _ => -1
        };
    }

}


class DAY7
{
    static void Main(string[] args)
    {        
        string[] lines = File.ReadAllLines("../../../Advent/2023_day7.txt");       

        List<KeyValuePair<string,long>> hands = new List<KeyValuePair<string,long>>();

        foreach(string line in lines){
            hands.Add(new KeyValuePair<string, long>(line.Split(' ')[0],Convert.ToInt64(line.Split(' ')[1])));
        }

        IComparer<KeyValuePair<string, long>> customComparer = new CustomComparer();
        
        // Use List.Sort with the custom comparer
        hands.Sort(customComparer);

        long count = 0;
        long index = hands.Count;
        foreach(var hand in hands){
            count += index * hand.Value;
            index--;
        }

        Console.WriteLine("count: " + count);
    }

        
}