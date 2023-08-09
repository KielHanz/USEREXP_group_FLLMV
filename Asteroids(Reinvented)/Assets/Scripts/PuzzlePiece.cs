using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private List<Sprite> _puzzleSprites;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject activateObject;

    private SpriteRenderer alpha;
    private Color objectAlpha;
    private BoxCollider2D objectCollider;

    private AudioSource audioSource;
    private SoundManager soundManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<PlayerScript>() != null)
        {
            PlayerScript playerScript = collision.GetComponent<PlayerScript>();
            playerScript.UpdatePuzzlePiecesCollected();
            if (playerScript._puzzlePiecesCollected >= GameManager.Instance._puzzlePieces.Count)
            {
                ObjectiveHolder objective = playerScript.GetComponent<ObjectiveHolder>();
                objective.ChangeObjectiveColor();
                if (activateObject != null)
                {
                    OpenDoor();
                }
            }
            soundManager.audioSource.PlayOneShot(soundManager.componentPickupSfx);
            Destroy(gameObject);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.Instance.GetComponent<SoundManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _puzzleSprites[Random.Range(0, 6)];
        GameManager.Instance._puzzlePieces.Add(this.gameObject);

        //For Gate
        if (activateObject != null)
        {
            alpha = activateObject.GetComponent<SpriteRenderer>();
            objectCollider = activateObject.GetComponent<BoxCollider2D>();
            audioSource = activateObject.GetComponent<AudioSource>();
        }
    }

    private void OpenDoor()
    {
        objectAlpha = alpha.color;
        objectAlpha.a = 0.25f;
        alpha.color = objectAlpha;
        objectCollider.enabled = false;
        audioSource.PlayOneShot(SoundManager.Instance.doorSfx);
    }
}
