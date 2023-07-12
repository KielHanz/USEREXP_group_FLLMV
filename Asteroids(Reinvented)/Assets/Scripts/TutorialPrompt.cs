using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompt : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject tutorialCanvas;

    private void Start()
    {
        tutorialCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>() != null)
        {
            playerCanvas.SetActive(false);
            tutorialCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>() != null)
        {
            playerCanvas.SetActive(true);
            tutorialCanvas.SetActive(false);
        }
    }
}