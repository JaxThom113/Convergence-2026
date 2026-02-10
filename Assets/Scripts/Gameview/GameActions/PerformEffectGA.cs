using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA : GameAction
{ 
    public Effect effect {get;  set;}
    public PerformEffectGA(Effect effect)
    {
        this.effect = effect;
    }
}
