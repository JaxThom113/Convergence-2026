using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public List<CardSO> playerDeck;

    public void Setup(PlayerSO playerData)
    {
        SetupBase(playerData); 
        playerDeck = playerData.playerDeck;
    }
}
