using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsAction : Actions
{
    [SerializeField] public int drawAmount; 

    public override GameAction GetGameAction()
    { 
        DrawCardGA drawCardGA = new(drawAmount);
        return drawCardGA;
    }
}
