using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendManaGA : GameAction
{
    public int manaAmount; 

    public SpendManaGA(int manaAmount)
    {
        this.manaAmount = manaAmount;
    }
}
