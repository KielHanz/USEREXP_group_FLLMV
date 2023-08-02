using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private BulletScript bullet;
    [SerializeField] private GameObject activateObject;

    private SpriteRenderer alpha;
    private SpriteRenderer lever;
    private Color objectAlpha;
    private BoxCollider2D objectCollider;

    private void Start()
    {
        alpha = activateObject.GetComponent<SpriteRenderer>();
        lever = this.GetComponent<SpriteRenderer>();
        objectCollider = activateObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bullet = collision.GetComponent<BulletScript>();

        if (bullet != null)
        {
            lever.flipX = true;
            OpenDoor();

            ObjectiveHolder objective = this.GetComponent<ObjectiveHolder>();
            objective.ChangeObjectiveColor();

        }
    }

    private void OpenDoor()
    {
        objectAlpha = alpha.color;
        objectAlpha.a = 0.25f;
        alpha.color = objectAlpha;

        objectCollider.enabled = false;

    }

}
