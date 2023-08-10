using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeText : MonoBehaviour
{
    [SerializeField] private Text fadeAwayText;
    public float fadeTime = 6f;

    private void Start()
    {
        fadeAwayText = GetComponent<Text>();
    }

    private void Update()
    {
        if(fadeTime > 0){
            fadeTime -= Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
        }
    }
}
