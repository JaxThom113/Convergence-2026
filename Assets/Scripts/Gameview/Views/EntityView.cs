using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EntityView : MonoBehaviour
{
    [SerializeField] public TMP_Text healthText; 

    public int maxHealth;
    public int currentHealth; 

    protected void SetupBase(EntitySO entityData) 
    { 
      maxHealth = currentHealth = entityData.entityHealth;    
      UpdateHealthText();
    } 
    private void UpdateHealthText() { 
        if(healthText != null) { 
             healthText.text = $"{currentHealth}/{maxHealth}"; 
        }
    }
    public void ReduceHealth(int amount) { 
        currentHealth -= amount;
        UpdateHealthText();
    }
}
