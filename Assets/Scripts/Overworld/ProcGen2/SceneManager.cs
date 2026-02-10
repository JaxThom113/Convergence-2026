using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneManager : Singleton<SceneManager>
{
    [Header("Transition Screen")]
    public GameObject transitionScreen;

    [Header("Script References")]
    public PlayerMovement2 playerMovement;

    IEnumerator BattleScreenTransition()
    {
        // take control from player, have player continue moving upward
        playerMovement.enabled = false;

        // have a camera zoom in on the enemy/player collision

        // transition swipe effect
        transitionScreen.SetActive(true);
        transitionScreen.transform.DOMoveY(0, 0.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1f);

        // generate new level
        // SceneManager.LoadScene("CardSetup");

        // transition swipe out and reset position
        transitionScreen.transform.DOMoveY(40, 0.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1f);
        transitionScreen.SetActive(false);
        transitionScreen.transform.position = new Vector3(0, -40, 0);

        // tp player to bottom of the screen, have player move upward to starting cell
        playerMovement.TeleportToBottom();
        playerMovement.ContinueUp();

        // return control to the player  
        playerMovement.enabled = true;
        Debug.Log("Battle scene transition complete");



        yield return null;
    }
}
