using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageSystem : MonoBehaviour
{ 
    [SerializeField] private GameObject knife;  
    [SerializeField] private Health health;  
    // Start is called before the first frame update
    private void OnEnable() 
    { 
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    } 
    void OnDisable() 
    { 
        ActionSystem.DetachPerformer<DealDamageGA>();
    }  
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA) { 
        int damageAmount = dealDamageGA.Amount; 
        Vector2 knifeStart = knife.transform.position; 
        Tween tween = knife.transform.DOMove(health.transform.position, 0.25f); 
        yield return tween.WaitForCompletion(); 
        knife.transform.DOMove(knifeStart, 0.5f); 
         health.ReduceHealth(damageAmount); 
    }

   
}
