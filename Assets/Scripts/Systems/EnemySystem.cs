using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{ 
    [SerializeField] public Enemy enemy {get; private set;}   
    [SerializeField] public int enemyTurnCount {get;  set;} = 0;
    public void Setup(EnemySO enemyData)
    {
        enemy = new Enemy();
        enemy.Setup(enemyData); 
       
    }
    //Performers are created in the system
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    } 

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA) 
    {  
   
        Debug.Log("Enemy Turn");
          
        foreach(var card in EnemyHandView.Instance.GetShownCards()) {  
              
            Debug.Log("Playing Enemy Card: " + card.cardName);
            PlayEnemyCardGA playEnemyCardGA = new PlayEnemyCardGA(card); 
            ActionSystem.Instance.AddReaction(playEnemyCardGA);  
            foreach(var effect in card.effects) { 
                effect.isPlayer = false;
                PerformEffectGA performEffectGA = new(effect);
                ActionSystem.Instance.AddReaction(performEffectGA); //add to subscriber list, since we cant call a perfomer in a performer  
                //This is protected in the IsPerforming check at the start of the perform method
            } 
            yield return EnemyHandView.Instance.RemoveEnemyCard(card); 
            
        } 
          yield return new WaitForSeconds(1f);
        

    }  
    public List<CardSO> GetCurrentEnemyHand()
    {
        return enemy.enemyDeck[enemyTurnCount].enemyHand;
    } 
    public int GetDrawAmount() { 
        if(enemyTurnCount >= enemy.enemyDeck.Count ) {
            enemyTurnCount = 0;
        } else {
            enemyTurnCount++;
        }
        return enemy.enemyDeck[enemyTurnCount].enemyHand.Count;
    }

}
