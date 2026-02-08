using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : Singleton<PlayerSystem>
{
    [SerializeField] public Player player {get; private set;} 

    public void Setup(PlayerSO playerData)
    { 
        player = new Player();
        player.Setup(playerData);
    }
}
