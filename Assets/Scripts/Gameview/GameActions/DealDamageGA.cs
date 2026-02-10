using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int Amount; 
    public bool isPlayer;
    public DealDamageGA(int amount, bool isPlayer) { 
        Amount = amount;
        this.isPlayer = isPlayer;
    }
}
