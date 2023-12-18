using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{
    // Public variables remain unchanged
    public Animated start;
    public Animated middle;
    public Animated end;

    // Activates the appropriate renderer and deactivates others
    public void SetActiveRenderer(Animated rendererToActivate)
    {
        start.enabled = (rendererToActivate == start);
        middle.enabled = (rendererToActivate == middle);
        end.enabled = (rendererToActivate == end);
    }

    // Sets the explosion direction based on the given vector direction
    public void SetDirection(Vector2 direction)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, direction));
    }

    // Schedules the destruction of this explosion object
    public void DestroyAfter(float delay)
    {
        StartCoroutine(ScheduleDestruction(delay));
    }

    // Coroutine to destroy this GameObject after a delay
    private IEnumerator ScheduleDestruction(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}


