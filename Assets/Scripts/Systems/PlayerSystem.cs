using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : Singleton<PlayerSystem>
{
    public Player player;

    public void Setup(PlayerSO playerData)
    {  
        Debug.Log("PlayerSystem Setup");
        player = new Player();
        player.Setup(playerData);
    }
}
