using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds3 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player entered top bound");
            LevelManager3.Instance.NextLevel();
        }
    }
}