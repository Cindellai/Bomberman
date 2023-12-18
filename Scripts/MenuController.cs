using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void QuitGame()
    {
        // Log message to Unity Console (useful for debugging, does nothing in built game)
        Debug.Log("Quit game requested.");

        // Quit the application
        Application.Quit();
    }
}

