using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
