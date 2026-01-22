using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    // Start is called before the first frame update 
    public HandView handView;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
            
            ApplyCard card = CardCreator.Instance.CreateCard(transform.position, transform.rotation); 
            StartCoroutine(handView.AddCard(card));
        }
    }
}
