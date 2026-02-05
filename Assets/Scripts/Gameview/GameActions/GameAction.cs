using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//GA's simply hold data, no logic
public abstract class  GameAction  
//Abstract base class that actionsystem needs in its input
{
    public List<GameAction> PreReactions { get; private set;} = new();  

    public List<GameAction> PerformReactions { get; private set;} = new();  

    public List<GameAction> PostReactions { get; private set;} = new();  

    
}
