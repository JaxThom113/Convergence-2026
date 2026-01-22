using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardCreator : Singleton<CardCreator>
{ 
    public Canvas canvas;
    public ApplyCard applyCardPrefab;
    public ApplyCard CreateCard(Vector3 position, Quaternion rotation)
    {
        ApplyCard card = Instantiate(applyCardPrefab, canvas.transform); 
        card.transform.position = position; 
        card.transform.rotation = rotation;  

        card.transform.DOScale(Vector3.one*0.3f, 0.15f); 
        return card; 
    }
}
