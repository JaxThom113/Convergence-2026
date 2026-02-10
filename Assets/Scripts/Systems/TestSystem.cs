using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    [SerializeField] public List<CardSO> cardSOs; 
    private void Update(){  
        if (Input.GetKeyDown(KeyCode.Space)){
            CardSystem.Instance.Setup(cardSOs); 
        }
    }
}
