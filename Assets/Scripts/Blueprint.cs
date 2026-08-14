using UnityEngine;

public class Blueprint 
{
    public string itemName;
    public string Req1;
    public string Req2;

    public int Req1Amount;
    public int Req2Amount;

    public int numOfRequirements;

    public Blueprint(string name, int reqnum, string R1,  int R1Amount, string R2, int R2Amount)
    {
        itemName = name;
        numOfRequirements = reqnum;
        Req1 = R1; Req2 = R2;
        Req1Amount = R1Amount;
        Req2Amount = R2Amount;
        
    }




}
