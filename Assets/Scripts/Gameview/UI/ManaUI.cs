using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ManaUI : MonoBehaviour
{ 
    [SerializeField] public TextMeshProUGUI manaText;
    // Start is called before the first frame update
    public void UpdateManaText(int manaAmount)
    {
        manaText.text = manaAmount.ToString();
    } 
    void Update(){ 
        if(ManaSystem.Instance != null) {
            Debug.Log("ManaSystem.Instance: ONLINE");
        }
    }

}
