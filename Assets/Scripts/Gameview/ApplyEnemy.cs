using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEnemy : MonoBehaviour
{ 
    public Enemy enemy;  
    public Sprite enemyIcon;   
    void Setup(Enemy enemy) { 
        this.enemy = enemy; 
        enemyIcon = enemy.enemyIcon; 
        gameObject.name = enemy.enemyName; 
    }
}
