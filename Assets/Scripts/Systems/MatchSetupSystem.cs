using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{ 

    [SerializeField] public PlayerSO playerData;
    [SerializeField] public EnemySO enemyData; 
    [SerializeField] public PlayerView playerView;
    [SerializeField] public EnemyView enemyView;
    private void OnEnable(){  
        
        SetupViews();
        StartCoroutine(SetupCards()); 

    }   
    private void SetupViews(){ 
        playerView.Setup(playerData);
        enemyView.Setup(enemyData); 
        PlayerSystem.Instance.Setup(playerData);
        EnemySystem.Instance.Setup(enemyData); 
        DamageSystem.Instance.Setup(playerView, enemyView);
    }
 

    private IEnumerator SetupCards(){  
        yield return new WaitForSeconds(1f); 
        
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
