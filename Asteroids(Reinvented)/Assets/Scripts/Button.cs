using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timeText;

    private BulletScript bullet;
    [SerializeField] private GameObject activateObject;

    private SpriteRenderer alpha;
    private Color objectAlpha;
    private BoxCollider2D objectCollider;

    private bool isDoorOpen;
    private float currentTime = 0;
    [SerializeField] private float timer;
    private void Start()
    {
        timeText.enabled = false;
        alpha = activateObject.GetComponent<SpriteRenderer>();
        objectCollider = activateObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isDoorOpen)
        {
            Timer(timer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bullet = collision.GetComponent<BulletScript>();

        if (bullet != null)
        {
            OpenDoor();

        }
    }

    private void CloseDoor()
    {
        isDoorOpen = false;
        objectAlpha = alpha.color;
        objectAlpha.a = 1f;
        alpha.color = objectAlpha;
        timeText.enabled = false;

        objectCollider.enabled = true;
    }

    private void OpenDoor()
    {
        isDoorOpen = true;
        objectAlpha = alpha.color;
        objectAlpha.a = 0.25f;
        alpha.color = objectAlpha;

        objectCollider.enabled = false;

        StartCoroutine(DoorTime(timer));

    }

    private void Timer(float time)
    {
        float maxTime = time;
        timeText.text = "" + (int)currentTime;

        Debug.Log(currentTime);
        if (currentTime > 0)
        {
            timeText.enabled = true;
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0)
        {
            timeText.enabled = false;
            currentTime = maxTime;
        }

    }

    private IEnumerator DoorTime(float time)
    {
      
        yield return new WaitForSeconds(time);
        CloseDoor();
    }
}
