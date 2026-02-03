using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class LevelBounds : MonoBehaviour
{
    public GameObject transitionScreen;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered TOP bound");
            StartCoroutine(LevelTransition());
        }
    }

    IEnumerator LevelTransition()
    {
        // have player continue moving upward

        // transition swipe effect

        // DOTween movement & animation
        transitionScreen.SetActive(true);
        transitionScreen.transform.DOMoveY(0, 0.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1f);


        // generate new level
        //GenerateLevel();

        // transition swipe out and reset position
        transitionScreen.transform.DOMoveY(40, 0.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1f);
        transitionScreen.SetActive(false);
        transitionScreen.transform.position = new Vector3(0, -40, 0);

        // tp player to bottom of the screen, have player move upward to starting cell

        // return control to the player  

        yield return null;
    }
}