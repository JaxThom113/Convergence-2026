using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
{ 
    // based off structure from youtube video https://www.youtube.com/watch?v=ls5zeiDCfvI
   private List<GameAction> reactions = null; 

   public bool IsPerforming { get; private set;} = false; 

   private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();  
   //When you draw a card, you can have a reaction to the card before being drawn

   private static Dictionary<Type, List<Action<GameAction>>> postSubs = new(); 
   //When you draw a card, you can have a reaction to the card after being drawn

   private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new(); 
   //Called when an action system is performing an action


    //Performers are for the systems 
   //Performers are the how, and responsible for execution 

   //Subscribers listen to the actions, using reactions based on the timing (before or after the action is performed)  
   //allow the storing of multiple reactions for the same action type

   //Reactions are the response to existing actions

   public void Perform(GameAction action, System.Action OnPerformFinished = null) 
   { 
        if(IsPerforming) return;  
        IsPerforming = true;  
        StartCoroutine(Flow(action, () =>  
        {  
            //when flow is done call callback
            IsPerforming = false;  
            OnPerformFinished?.Invoke(); 
        }));
   } 
   public void AddReaction(GameAction gameAction) 
   { 
         reactions?.Add(gameAction); //always adds 3 actions to the current list
   } 
   private IEnumerator Flow(GameAction action, System.Action OnFlowFinished = null) 
   { 
       reactions = action.PreReactions; //set reactions to pre reactions list of our current action 
       //now all actions can react to the current action 
       PerformSubscribers(action, preSubs); 
       yield return PerformReactions(); 

       reactions = action.PerformReactions; 
       yield return PerformPerformer(action); 
       yield return PerformReactions(); 

       reactions = action.PostReactions; 
       PerformSubscribers(action, postSubs); 
       yield return PerformReactions(); 

       OnFlowFinished?.Invoke();
   } 
   private IEnumerator PerformPerformer(GameAction action)  
   { 
          Type type = action.GetType(); 
          if (performers.ContainsKey(type)) 
          { 
            yield return performers[type](action);
          }
   } 
   private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs) 
   {  
    //tells all pre subscribers of this action type that we are performing this action type
        Type type = action.GetType(); 
        if (subs.ContainsKey(type)) 
        { 
            foreach(var sub in subs[type]) 
            { 
                sub(action);
            }
        }
   } 
   private IEnumerator PerformReactions() 
   {   
    foreach(var reaction in reactions)
        {
            yield return Flow(reaction);
        }
   }  
  
   public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction 
   {   
        //IEnumerator as sometimes we want to wait before action is performed
        //this method does the logic of our game action, and attatches it to the action system 
        Type type = typeof(T);  //first get the type of the game action (different classes have different types)
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action); //Convert our performer to an IEnumerator so it can be added to the dictionary
        if (performers.ContainsKey(type)) performers[type] = wrappedPerformer; //then simply add it to the dictionary
        else performers.Add(type, wrappedPerformer); 
        
   } 
   public static void DetachPerformer<T>() where T : GameAction 
   {  
        //Detaches a performer
        Type type = typeof(T); 
        if (performers.ContainsKey(type)) performers.Remove(type); 
   }  
   //Reaction are for the status effects
   public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction 
   { 
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;  //creates a subscription dictionary based on the timing (before or after the action is performed)
        void wrappedReaction(GameAction action) => reaction((T)action); 
        if (subs.ContainsKey(typeof(T))) 
        { 
            subs[typeof(T)].Add(wrappedReaction);
        } 
        else 
        { 
            subs.Add(typeof(T), new()); 
            subs[typeof(T)].Add(wrappedReaction);
        }
   } 
   public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction  
   { 
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;  //creats a subscription dictionary based on the timing (before or after the action is performed)
        if (subs.ContainsKey(typeof(T))) 
        { 
            void wrappedReaction(GameAction action) => reaction((T)action);  //creates a function that wraps the reaction function so it can be removed from the dictionary
            subs[typeof(T)].Remove(wrappedReaction);
        }
   } 
}
