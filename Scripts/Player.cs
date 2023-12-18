using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public int playerIndex; // Index of the player to identify which player this is

    private void Start()
    {
        // Initialize player lives in GameManager
        GameManager.Instance.playerLives[playerIndex] = 3;
    }

    // This method will be called when the player collides with an explosion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Explosion" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            // Tell the GameManager that this player was hit
            GameManager.Instance.PlayerHit(playerIndex);
        }
    }
}
