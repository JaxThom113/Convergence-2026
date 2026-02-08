using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Entities/Player")]
public class PlayerSO : EntitySO
{
    public List<CardSO> playerDeck;
}
