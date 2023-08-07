using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timeText;

    private BulletScript bullet;
    [SerializeField] private GameObject activateObject;
    [SerializeField] Sprite[] spriteChange;

    private SpriteRenderer targetSprite;
    private SpriteRenderer spriteRenderer;
    private Color objectAlpha;
    private BoxCollider2D objectCollider;

    private bool isDoorOpen;
    private float currentTime = 0;
    [SerializeField] private float timer;

    private ObjectiveHolder objectiveHolder;
    //public TMP_Text Objective;

    private AudioSource audioSource;

    private void Start()
    {
        timeText.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetSprite = activateObject.GetComponent<SpriteRenderer>();
        objectCollider = activateObject.GetComponent<BoxCollider2D>();
        objectiveHolder = GetComponent<ObjectiveHolder>();
        audioSource = activateObject.GetComponent<AudioSource>();
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
            if (objectiveHolder != null)
            {
                objectiveHolder.ChangeObjectiveColor();
            }
        }
    }

    private void CloseDoor()
    {
        spriteRenderer.sprite = spriteChange[0];
        isDoorOpen = false;
        objectAlpha = targetSprite.color;
        objectAlpha.a = 1f;
        targetSprite.color = objectAlpha;
        timeText.enabled = false;

        objectCollider.enabled = true;
        audioSource.PlayOneShot(SoundManager.Instance.doorSfx);
    }

    private void OpenDoor()
    {
        spriteRenderer.sprite = spriteChange[1];
        isDoorOpen = true;
        objectAlpha = targetSprite.color;
        objectAlpha.a = 0.25f;
        targetSprite.color = objectAlpha;

        objectCollider.enabled = false;

        StartCoroutine(DoorTime(timer));
        audioSource.PlayOneShot(SoundManager.Instance.doorSfx);
    }

    private void Timer(float time)
    {
        float maxTime = time;
        timeText.text = "" + (int)currentTime;

        //Debug.Log(currentTime);
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
