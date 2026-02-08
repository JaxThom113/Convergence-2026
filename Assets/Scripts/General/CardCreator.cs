using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardCreator : Singleton<CardCreator>
{  
    //Both enemy and player can use 
    [SerializeField] private Canvas canvas;
    [SerializeField] private ApplyCard applyCardPrefab;
    public ApplyCard CreateCard(Card card, Vector3 position, Quaternion rotation, bool isEnemy)
    {
        ApplyCard applyCard = Instantiate(applyCardPrefab, canvas.transform); 
        applyCard.transform.position = position; 
        applyCard.transform.rotation = rotation;  
        applyCard.Setup(card); 
        applyCard.InventoryCard = isEnemy;
        applyCard.transform.DOScale(Vector3.one*0.3f, 0.15f); 
        return applyCard; 
    }
}
