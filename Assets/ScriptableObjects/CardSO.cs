using System.Collections;
using System.Collections.Generic;
using SerializeReferenceEditor;
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
    public CardType cardType; 
    public Sprite cardTypeIcon;
    [SerializeReference, SR(typeof(Effect))]
    public List<Effect> effects = new List<Effect>(); 
    
}
