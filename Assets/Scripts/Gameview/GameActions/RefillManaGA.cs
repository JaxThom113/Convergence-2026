using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillManaGA : GameAction
{
    public int manaAmount; 
    public RefillManaGA(int manaAmount)
    {
        this.manaAmount = manaAmount;
    }
}
