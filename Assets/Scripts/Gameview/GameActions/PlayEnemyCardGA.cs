using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEnemyCardGA : GameAction
{
   public Card card {get;set;} 
   public PlayEnemyCardGA(Card card) { 
        this.card = card; 
   }
}
