using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public string enemyName => data.enemyName;  
    public Sprite enemyIcon => data.enemyIcon;   

    private EnemySO data; 
    public Enemy(EnemySO dataSO) { 
        data = dataSO; 
        
    }
}
