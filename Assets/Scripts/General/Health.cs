using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 10;  
    public void ReduceHealth(int amount) 
    { 
        health -= amount; 
        if(health <= 0) 
        { 
            Destroy(gameObject); 
        }
    }
}
