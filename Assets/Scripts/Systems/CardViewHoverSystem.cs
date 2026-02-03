using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{ 
    public ApplyCard ApplyCardHover;
    // Start is called before the first frame update
    public void Show(Card card, Vector3 position)
    {
        ApplyCardHover.gameObject.SetActive(true);  
        ApplyCardHover.transform.DOScale(0.4f, 0.3f).SetEase(Ease.OutBack); 
        ApplyCardHover.transform.DOLocalMoveY(100f, 0.3f).SetEase(Ease.OutBack);
        // Move to top of hierarchy so it renders on top of other UI elements
       
        
        ApplyCardHover.Setup(card); 
        ApplyCardHover.transform.position = position; 
        
    }

    public void Hide()
    {
        ApplyCardHover.gameObject.SetActive(false); 
        ApplyCardHover.transform.DOScale(0.2f, 0.3f).SetEase(Ease.InBack);
        ApplyCardHover.transform.DOLocalMoveY(0f, 0.3f).SetEase(Ease.InBack);
    }
}
