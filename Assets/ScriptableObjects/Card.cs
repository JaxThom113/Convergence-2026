using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{ 
    // If we want to apply status effects to a card, we cannot apply them to the  ScriptableObject  
    // that would effect the actual card data. and other cards that are being played 
    // Would be a very restrictive architecture.
    // (IE the enemies cards)
    public string cardName => data.cardName; 
    public Sprite cardBorder => data.cardBorder;
    public Sprite cardIcon => data.cardIcon;
    //public Sprite cardTypeIcon => data.cardTypeIcon;
    //public Sprite cardElementIcon => data.cardElementIcon;
    public string cardDescription {get; private set; } 
    public int cardCost {get; private set; }
    public Element cardElement {get; private set; }
    //public CardType cardType {get; private set; }
    private CardSO data;
    public Card(CardSO dataSO) { 
        data = dataSO;
        cardDescription = dataSO.cardDescription;  
        cardCost = dataSO.cardCost;     
        cardElement = dataSO.cardElement;  
        //cardElementIcon = dataSO.cardElementIcon;
        //cardType = dataSO.cardType; 
        //cardTypeIcon = dataSO.cardTypeIcon;
    }
    
}
