
using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 10f;
    public int playerIndex;
    public Vector2 originalPosition;

    [Header("Input")]
    public KeyCode inputup = KeyCode.I;
    public KeyCode inputdown = KeyCode.K;
    public KeyCode inputleft = KeyCode.J;
    public KeyCode inputright = KeyCode.L;

    [Header("Sprites")]
    public Animated RenderUp;
    public Animated RenderDown;
    public Animated RenderLeft;
    public Animated RenderRight;
    public Animated RenderDeath;
    private Animated  active;

    private void Start()
    {
        originalPosition = transform.position; // Store the original position
        GameManager.Instance.playerLives[playerIndex] = 3;
    }

    public void ResetToAliveState()
    {
        // Reset player state for respawning
        enabled = true; // Re-enable the Movement script
        GetComponent<BombController>().enabled = true; // Re-enable the BombController script

        // Re-enable the default sprite and disable the death sprite
        RenderUp.enabled = false;
        RenderDown.enabled = true; // Assuming the player faces down by default
        RenderLeft.enabled = false;
        RenderRight.enabled = false;
        RenderDeath.enabled = false;

        // Reset any other necessary states or components here
        // e.g., reset animation states, re-enable colliders, etc.
    }

    private IEnumerator RespawnPlayer(GameObject playerGameObject, int playerIndex)
    {
        // Wait for death animation to finish
        yield return new WaitForSeconds(1.25f); // Adjust time as needed for the death animation

        // Set the player's position to the original position
        Movement playerMovement = playerGameObject.GetComponent<Movement>();
        playerGameObject.transform.position = playerMovement.originalPosition;

        // Reactivate the player and reset their state
        playerGameObject.SetActive(true);
        playerMovement.ResetToAliveState();
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        active = RenderDown;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKey(inputup))
        {
            SetDirection(Vector2.up, RenderUp);
        }
        else if (UnityEngine.Input.GetKey(inputdown))
        {
            SetDirection(Vector2.down, RenderDown);
        }
        else if (UnityEngine.Input.GetKey(inputleft))
        {
            SetDirection(Vector2.left, RenderLeft);
        }
        else if (UnityEngine.Input.GetKey(inputright))
        {
            SetDirection(Vector2.right, RenderRight);
        }
        else
        {
            SetDirection(Vector2.zero, active);
        }
    }


    private void SetDirection(Vector2 newDirection, Animated spriteRenderer)
    {
        direction = newDirection;

        RenderUp.enabled = spriteRenderer == RenderUp;
        RenderDown.enabled = spriteRenderer == RenderDown;
        RenderLeft.enabled = spriteRenderer == RenderLeft;
        RenderRight.enabled = spriteRenderer == RenderRight;

        active = spriteRenderer;
        active.idle = direction == Vector2.zero;
    }


    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;

        rigidbody.MovePosition(position + translation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        RenderUp.enabled = false;
        RenderDown.enabled = false;
        RenderLeft.enabled = false;
        RenderRight.enabled = false;
        RenderDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        GameManager.Instance.PlayerHit(playerIndex);
    }

}


