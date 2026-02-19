using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionSystem : Singleton<SceneTransitionSystem>
{
    public PlayerSO playerData; 
    public EnemySO enemyData {get; set;}
}
