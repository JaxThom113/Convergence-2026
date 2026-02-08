using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{ 

    [SerializeField] public PlayerSO playerData;
    [SerializeField] public EnemySO enemyData; 
    [SerializeField] public PlayerView playerView;
    [SerializeField] public EnemyView enemyView;
    private void Start(){   
        SetupEntities();
        SetupCards();
    }  
    private void SetupEntities(){ 
        PlayerSystem.Instance.Setup(playerData); 
        EnemySystem.Instance.Setup(enemyData);  
        playerView.Setup(playerData);
        enemyView.Setup(enemyData);
    }

    private void SetupCards(){ 
        List<CardSO> playerDeck = PlayerSystem.Instance.player.playerDeck;  
        List<CardSOList> enemyDeck = EnemySystem.Instance.enemy.enemyDeck; 
        Inventory.Instance.Setup(playerDeck);
        CardSystem.Instance.Setup(playerDeck, enemyDeck); 
        DrawEnemyCardGA drawEnemyCardGA = new(EnemySystem.Instance.GetDrawAmount()); 
        
        DrawCardGA drawCardGA = new(5);   
        ActionSystem.Instance.Perform(drawEnemyCardGA, ()=> {
            ActionSystem.Instance.Perform(drawCardGA); 
        }); 
    }
}
