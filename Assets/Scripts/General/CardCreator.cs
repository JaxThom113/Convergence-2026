using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardCreator : Singleton<CardCreator>
{ 
    public Canvas canvas;
    public ApplyCard applyCardPrefab;
    public ApplyCard CreateCard(Card card, Vector3 position, Quaternion rotation)
    {
        ApplyCard applyCard = Instantiate(applyCardPrefab, canvas.transform); 
        applyCard.transform.position = position; 
        applyCard.transform.rotation = rotation;  
        applyCard.Setup(card);
        applyCard.transform.DOScale(Vector3.one*0.3f, 0.15f); 
        return applyCard; 
    }
}
