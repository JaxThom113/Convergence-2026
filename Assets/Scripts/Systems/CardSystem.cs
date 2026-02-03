using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardSystem : Singleton<CardSystem>
{
    // Start is called before the first frame update 
    public CardSO cardSO; 
    
    public Transform drawPileTransform;
    public Transform discardPileTransform;
   
    
    private List<Card> drawPile = new(); 
    private List<Card> discardPile = new();  
    private List<Card> hand = new();   
    // Action System Setup
    private void OnEnable() 
    { 
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
        ActionSystem.AttachPerformer<DiscardCardGA>(DiscardCardPerformer); 
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE); 
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
     } 
    private void OnDisable() 
    {  
        UnityEngine.Debug.Log("CardSystem Disabled");
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DiscardCardGA>(); 
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE); 
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }  
    //Public Methods 
    public void Setup(List<CardSO> cardSOs)  
    { 
        foreach(var cardSO in cardSOs) {  
            Card card = new Card(cardSO);
            drawPile.Add(card);
          
        }
        //drawPile.Shuffle();
    }

    //Performers
    private IEnumerator DrawCardPerformer(DrawCardGA drawCardGA)
    { 
        int cardAmount = Mathf.Min(drawCardGA.Amount, drawPile.Count); 
        int notDrawnAmount = drawCardGA.Amount - cardAmount; 
        for (int i = 0; i < cardAmount; i++) 
        { 
            yield return DrawCard();
        }
        if (notDrawnAmount > 0) { 
            RefillDeck(); 
            for(int i = 0; i < notDrawnAmount; i++) { 
                yield return DrawCard();
            }
        }
        Card card = new Card(cardSO);  
    } 
    private IEnumerator DiscardCardPerformer(DiscardCardGA discardCardGA)
    { 
        foreach(var card in hand) { 
            discardPile.Add(card); 
            ApplyCard applyCard = HandView.Instance.RemoveCard(card); 
            yield return DiscardCard(applyCard);
        }
        hand.Clear();
    }
      
    // Reactions 
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA){ 
         DiscardCardGA discardCardGA = new(); 
         ActionSystem.Instance.AddReaction(discardCardGA);

    } 
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA) 
    {  
        DrawCardGA drawCardGA = new(5); 
        ActionSystem.Instance.AddReaction(drawCardGA);
    }
    //Helper Methods
    private IEnumerator DrawCard() 
    { 
        Card card = drawPile.Draw(); 
        hand.Add(card);
        ApplyCard applyCard = CardCreator.Instance.CreateCard(card, drawPileTransform.position, drawPileTransform.rotation);                   
        yield return  StartCoroutine(HandView.Instance.AddCard(applyCard));
    }  
    private void RefillDeck() 
    { 
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        
    } 

    private IEnumerator DiscardCard(ApplyCard applyCard) 
    {  
        applyCard.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = applyCard.transform.DOMove(discardPileTransform.position, 0.15f); 
        yield return tween.WaitForCompletion();
        Destroy(applyCard.gameObject);
        
    }

    // Update is called once per frame
    
}
