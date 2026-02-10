using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EntityView : MonoBehaviour
{
    [SerializeField] public TMP_Text healthText; 
    [SerializeField] public SpriteRenderer spriteRenderer; 

    public int maxHealth;
    public int currentHealth; 

    protected void SetupBase(EntitySO entityData) 
    { 
      maxHealth = currentHealth = entityData.entityHealth;    
      UpdateHealthText();
      spriteRenderer.sprite = entityData.entityIcon;
    } 
    private void UpdateHealthText() { 
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
    public void ReduceHealth(int amount) { 
        currentHealth -= amount;
        UpdateHealthText();
    }
}
