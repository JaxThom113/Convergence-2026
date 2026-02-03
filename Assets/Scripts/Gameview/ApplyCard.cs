using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; 
using DG.Tweening;
public class ApplyCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{  
    //All this script does is apply the card data to the UI. 
    //Data is handled in the Card class. 
    //Data originates from the CardSO scriptable object, 
    //and is then fed to the Card class, where it is finally applied here. 
    public Card card;
    public Sprite cardBorder; 
    public Sprite cardIcon;  
    public int cardCost; 
    public string cardDescription; 
    public Element cardElement; 
    public string cardName;   
    public GameObject wrapper; 

    private bool tweening = true;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    //ALL TYPES MUST BE THE SAME
    // Start is called before the first frame update
    public void Setup(Card card)
    { 
        this.card = card; 
        cardBorder = card.cardBorder; 
        cardIcon = card.cardIcon; 
        cardCost = card.cardCost; 
        cardDescription = card.cardDescription; 
        cardElement = card.cardElement; 
        cardName = card.cardName;   
    }
    void OnEnable()
    {
        StartCoroutine(TweeningCooldown());
    }
    private IEnumerator TweeningCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        //tweening = false;
    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    // UI Event System - works for UI objects (Image, Button, etc.)
    public void OnPointerEnter(PointerEventData eventData)
    {  
        //if(tweening) return;
        if(!Interactions.Instance.PlayerCanHover()) return;
        wrapper.SetActive(false);
        
        transform.SetAsLastSibling();
        CardViewHoverSystem.Instance.Show(card, transform.position);
    } 
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;

        Interactions.Instance.PlayerIsDragging = true;
        
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
        transform.rotation = Quaternion.identity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Interactions.Instance.PlayerIsDragging) return;

        Vector3 worldPoint; 
        wrapper.SetActive(true);
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)transform.parent, 
            eventData.position, 
            eventData.pressEventCamera, 
            out worldPoint))
        {
            transform.position = new Vector3(worldPoint.x, worldPoint.y, worldPoint.z - 0.1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Interactions.Instance.PlayerIsDragging = false;
        
        transform.DOMove(initialPosition, 0.2f);
        transform.DORotateQuaternion(initialRotation, 0.2f);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
       //if(tweening) return;
        if(!Interactions.Instance.PlayerCanHover()) return;
        wrapper.SetActive(true);
        transform.SetAsFirstSibling();
        CardViewHoverSystem.Instance.Hide();
    }
    
    // Note: OnMouseEnter/Exit only work for 3D/2D objects with colliders, not UI
}
