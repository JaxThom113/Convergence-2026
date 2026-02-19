using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-100)]  // Run before other scripts so Instance is set before anyone accesses it
public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] public ManaUI manaUI; 

    public int maxMana = 10;
    private int currentMana;   

    private void Start() 
    {  
        
        currentMana = maxMana; 
        manaUI.UpdateManaText(currentMana);
    } 
    private void OnEnable(){ 
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer); 
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST); 
        //ActionSystem.Performer<ChildofGameAction>(FunctionName)
    }  
    private void OnDisable(){ 
        ActionSystem.DetachPerformer<RefillManaGA>(); 
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    public bool HasEnoughMana(int manaAmount) { 
        return currentMana >= manaAmount;
    } 
    public void TestMana()
    {
        
    }

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.manaAmount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    } 
    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana = refillManaGA.manaAmount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    } 
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA) 
    { 
        RefillManaGA refillManaGA = new(maxMana);  
        ActionSystem.Instance.AddReaction(refillManaGA); 
         //Flow will find the performer for RefillManaGA in mana system and call it 
        //since we attached the performer for RefillManaGA in mana system
    }

}
