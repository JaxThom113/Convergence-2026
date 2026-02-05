using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardSystem : Singleton<CardSystem>
{ 
     //Hold the LOGIC, takes GA's as input for data
    

    /* 
    DrawCardGA -> DrawCardPerformer -> DrawCard() -> AddCard() -> ApplyCard()
    DiscardCardGA -> DiscardCardPerformer -> DiscardCard() -> RemoveCard() -> ApplyCard()
    PlayCardGA -> PlayCardPerformer -> PlayCard() -> RemoveCard() -> ApplyCard()
    EnemyTurnGA -> EnemyTurnPreReaction -> DiscardCardGA -> AddReaction()
    EnemyTurnGA -> EnemyTurnPostReaction -> DrawCardGA -> AddReaction()
    */     
    
    // Start is called before the first frame update 
    [SerializeField] private CardSO cardSO; 
    
    [SerializeField] private Transform drawPileTransform;
    [SerializeField] private Transform discardPileTransform;
   
    
    private List<Card> drawPile = new(); 
    private List<Card> discardPile = new();  
    private List<Card> hand = new();   
    // Action System Setup
    private void OnEnable() 
    {  
        //Attach Performer to add to dictionary so we wont get an error when performperformer/performsubscriber
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
        ActionSystem.AttachPerformer<DiscardCardGA>(DiscardCardPerformer);  
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE); //same thing as above, but if prereaction or postreaction we call subscribe reaction instead of attach perfomer
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST); 
        
     } 
    private void OnDisable() 
    {  
        UnityEngine.Debug.Log("CardSystem Disabled");
        ActionSystem.DetachPerformer<DrawCardGA>(); //remove from dictionary so we wont get an error when detaching performer
        ActionSystem.DetachPerformer<DiscardCardGA>(); 
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE); 
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST); 
        //Remove from dictionary so we wont get an error when unsubscribing reaction
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
    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.card); 
        discardPile.Add(playCardGA.card);
        ApplyCard applyCard = HandView.Instance.RemoveCard(playCardGA.card);
        yield return DiscardCard(applyCard); 
       foreach(var effect in playCardGA.card.effects) { 
            PerformEffectGA performEffectGA = new(effect);
            ActionSystem.Instance.AddReaction(performEffectGA); //add to subscriber list, since we cant call a perfomer in a performer  
            //This is protected in the IsPerforming check at the start of the perform method
       }
    }
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
