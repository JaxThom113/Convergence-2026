using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    public Camera mainCamera;
    public float speed;

    [Header("Top/Bottom Bound References")]
    public GameObject bottomBound;
    public GameObject topBound;

    // collider of top/bottom bounds
    private Collider bottomBoundCollider;
    private Collider topBoundCollider;

    // current level & area
    private int currentLevel;
    private int currentArea;

    void Start()
    {
        currentLevel = 1;
        currentArea = 1;

        bottomBoundCollider = bottomBound.GetComponent<Collider>();
        topBoundCollider = topBound.GetComponent<Collider>();   
    }

    // see if top or bottom bound is entered by player

    // if top, start coroutine to move player up a level

    void OnTriggerEnter(Collider other)
    {
        // only react to player
        if (other.CompareTag("Player"))
        {
            if (other.gameObject == bottomBound)
            {
                Debug.Log("Player entered BOTTOM bound");
                currentArea--;
            }
            else if (other.gameObject == topBound)
            {
                Debug.Log("Player entered TOP bound");
                currentArea++;
            }
        }
    }
}