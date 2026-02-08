using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawEnemyCardGA : GameAction
{ 
    public int Amount {get; set;}  
    public DrawEnemyCardGA(int amount)
    {
        Amount = amount;
    }
}
