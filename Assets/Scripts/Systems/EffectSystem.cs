using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : Singleton<EffectSystem>
{
    //Hold perfomer for performeffecr game action 

    void OnEnable() 
    { 
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    } 
    void OnDisable() 
    { 
        ActionSystem.DetachPerformer<PerformEffectGA>();
    } 
    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    { 
        GameAction effectAction = performEffectGA.effect.GetGameAction(); 
        ActionSystem.Instance.AddReaction(effectAction);  
        yield return null;
    } 
}
