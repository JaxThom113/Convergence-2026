using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element 
{ 
    None, 
    Fire, 
    Cold, 
    Lightning, 
    Poison
} 
public enum CardType
{
    Attaction, 
    Action,
    Runes,
    Terrunes
}

public class CardSO : ScriptableObject
{
    public string cardName; 
    public int cardCost; 
    public Sprite cardBorder; 
    public Sprite cardIcon; 
    public string cardDescription;  
    public Element cardElement;  
    public Sprite cardElementIcon; 
    [SerializeReference] public List<Actions> actions; 
    public CardType cardType; 
    public Sprite cardTypeIcon;
}
