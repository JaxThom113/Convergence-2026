using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStatsGA : GameAction
{
    public Enemy Target; 

    public int AttackIncreaseAmount; 

    public int HealthIncreaseAmount; 

    public IncreaseStatsGA(Enemy target, int attackIncreaseAmount, int healthIncreaseAmount) 
    { 
        Target = target; 
        AttackIncreaseAmount = attackIncreaseAmount;  
        HealthIncreaseAmount = healthIncreaseAmount; 
    }   
}
