using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private List<Sprite> _puzzleSprites;
    private SpriteRenderer _spriteRenderer;

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
            }
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _puzzleSprites[Random.Range(0, 6)];
        GameManager.Instance._puzzlePieces.Add(this.gameObject);
    }
}
