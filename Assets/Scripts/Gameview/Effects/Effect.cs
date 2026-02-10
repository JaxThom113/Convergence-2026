using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Effect 
{
        [SerializeField] public bool isPlayer;
        public abstract GameAction GetGameAction();
}
