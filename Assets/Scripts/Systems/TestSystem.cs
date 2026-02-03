using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    public List<CardSO> cardSOs; 
    private void Start(){ 
        CardSystem.Instance.Setup(cardSOs);
    }
}
