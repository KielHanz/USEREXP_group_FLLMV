using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScript : MonoBehaviour
{
    [SerializeField] private Canvas menuCanva;
    [SerializeField] private Canvas CreditCanvas;
    public float creditTime = 6f;
    public float creditTimer = 6f;

    private void Start()
    {
        menuCanva.gameObject.SetActive(false);
        CreditCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        creditTimer = Mathf.Clamp(creditTimer, 0, creditTime);
        creditTimer -= Time.deltaTime;
        if(creditTimer <= 0){
            menuCanva.gameObject.SetActive(true);
            CreditCanvas.gameObject.SetActive(false);
        }
    }
}
