using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStatsGA : GameAction
{
    [SerializeField] private Enemy Target; 
    
    [SerializeField] private int AttackIncreaseAmount; 
    
    [SerializeField] private int HealthIncreaseAmount;

    public IncreaseStatsGA(Enemy target, int attackIncreaseAmount, int healthIncreaseAmount) 
    { 
        Target = target; 
        AttackIncreaseAmount = attackIncreaseAmount;  
        HealthIncreaseAmount = healthIncreaseAmount; 
    }   
}
