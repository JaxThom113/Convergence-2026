using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldSystem : MonoBehaviour
{ 
    public EnemyView enemyView;  
    public EnemySO enemyData; 
    private void Start() { 
        SetupEnemy();
    }
    private void SetupEnemy() { 
        enemyView.Setup(enemyData);
    }
}
