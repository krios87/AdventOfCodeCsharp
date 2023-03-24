using System;

class BagRule{
    private string Bagname;
    private Dictionary<string,int> Bags;

    private bool containBag = false;

    public BagRule(string NewBagName,Dictionary<string,int> NewBags){

        Bagname = NewBagName;
        Bags = NewBags;
    }

    public Dictionary<string,int> getDict(){

        return Bags;
    }

    public void setContain(){
        containBag = true;
    }

    public bool getContain(){
        return containBag;
    }

    public string GetName(){
        return Bagname;
    }
}

  class MyProgram
  {
    public static int countinDict(string searchString, ref List<BagRule> RuleList, string ruleName){
        int Count = 0;

        foreach(var rule in RuleList){ 
            if(rule.GetName() == ruleName){
                if(rule.getContain()){  //searching in bag we already know contains (from searching before in another rule)
                    Count++;
                }else{
                    foreach(var dict in rule.getDict()){
                        string nextBag = dict.Key;
                        if(nextBag == searchString){  //found in one of the bag
                            rule.setContain();
                            Count++;
                            break;
                        }else{
                            int tmpcount = 0;
                            tmpcount =  countinDict(searchString,ref RuleList,nextBag); 
                            if(tmpcount!=0){ //we need to break if we found so we dont count several times!
                                Count += tmpcount;
                                break;
                            }
                            
                        }
                    }
                }
            } 
        }  

        return Count;
            

    }

    public static long countNbrOfTotalBags(ref List<BagRule> RuleList, string ruleName){
        long Count = 0;
        foreach(var rule in RuleList){ 
            if(rule.GetName() == ruleName){
                foreach(var dict in rule.getDict()){
                    if(dict.Value != 0){
                        Count += dict.Value;
                        Count += dict.Value * countNbrOfTotalBags(ref RuleList, dict.Key);
                    }
                }
            }
        }
        return Count;
    }

    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("C:/Users/Kristofer Rosquist/Desktop/HoT/Training/Advent/2020_day7.txt");

        var bagruleList = new List<BagRule>();

        foreach(string line in lines){

            string searchstring = "contain";
            int endfirst =line.IndexOf(searchstring);

            string bagruleName = line.Substring(0,endfirst-2); //minus space and s in bags
            string containingBags = line.Substring(endfirst + searchstring.Length);

            string[] baglist = containingBags.Split(',');

            //remove new line of last element
            baglist[baglist.Length-1] = baglist[baglist.Length-1].Substring(0,baglist[baglist.Length-1].Length-1);

            var bagtypeslist = new Dictionary<string,int>();
            for(int i = 0; i< baglist.Length; i++){
                int searchspace = baglist[i].IndexOf(' ',1); //need good format to convert to int, so skip first space and last around number
                string numberstring = baglist[i].Substring(1,searchspace);
                string bagtype = baglist[i].Substring(searchspace+1,baglist[i].Length-(searchspace+1));                

                int nbrOfBags;
                if(Char.IsDigit(numberstring[0])){
                    nbrOfBags = Convert.ToInt32(numberstring);
                    
                    if(nbrOfBags>1){
                        bagtype = bagtype.Substring(0,bagtype.Length-1); //remove the s for plural bag
                    }
                    
                }else{
                    nbrOfBags = 0;
                }

                bagtypeslist.Add(bagtype,nbrOfBags);

            }

            bagruleList.Add(new BagRule(bagruleName,bagtypeslist));

        }

        string searchString = "shiny gold bag";

        int count = 0;
        foreach(var barul in bagruleList){
            count += countinDict(searchString, ref bagruleList, barul.GetName());        
        }

        Console.WriteLine("counts: " + count);

        long count2 = countNbrOfTotalBags(ref bagruleList, searchString);

        Console.WriteLine("counts2: " + count2);

    }

  }

  