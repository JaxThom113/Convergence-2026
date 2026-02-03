using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
     
    private void Start(){  
        CardSystem.Instance.Setup(Inventory.Instance.cards); 
        DrawCardGA drawCardGA = new(5);  
        ActionSystem.Instance.Perform(drawCardGA);
    }
}
