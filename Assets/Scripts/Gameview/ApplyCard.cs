using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ApplyCard : MonoBehaviour
{  
    //All this script does is apply the card data to the UI. 
    //Data is handled in the Card class. 
    //Data originates from the CardSO scriptable object, 
    //and is then fed to the Card class, where it is finally applied here. 

    public Card card {get; set; }  
    public GameObject cardBorder; 
    public GameObject cardIcon;  
    public GameObject cardCost; 
    public GameObject cardDescription; 
    public GameObject cardElement; 
    public GameObject cardName; 
    // Start is called before the first frame update
    public void Setup(Card _card)
    { 
        card = _card; 
        cardBorder.GetComponent<Image>().sprite = card.cardBorder; 
        cardIcon.GetComponent<Image>().sprite = card.cardIcon; 
        cardCost.GetComponent<TextMeshProUGUI>().text = card.cardCost.ToString(); 
        cardDescription.GetComponent<TextMeshProUGUI>().text = card.cardDescription; 
        cardElement.GetComponent<TextMeshProUGUI>().text = card.cardElement.ToString(); 
        cardName.GetComponent<TextMeshProUGUI>().text = card.cardName; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
