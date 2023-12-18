using UnityEngine;

public class Pickups : MonoBehaviour
{
    // ItemType and audio clip remain unchanged
    public enum ItemType
    {
        BombPickup,
        BlastPickup,
        SpeedPickup,
    }

    [Header("Audio")]
    public AudioClip pickupSound;

    public ItemType type;

    // This method is called when the player collides with the pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPickupEffect(other.gameObject);
            PlayPickupSound();
            SelfDestruct();
        }
    }

    // Applies the effect of the pickup based on its type
    private void ApplyPickupEffect(GameObject player)
    {
        switch (type)
        {
            case ItemType.BombPickup:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastPickup:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedPickup:
                player.GetComponent<Movement>().speed += 2;
                break;
        }
    }

    // Plays the pickup sound at the pickup's position
    private void PlayPickupSound()
    {
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    }

    // Destroys the pickup game object
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
