using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject[] players;
    public int[] playerLives; // Array to keep track of the lives of each player

    
    private string winner; // To store the name of the winner


    [Header("Audio")]
    public AudioClip lifeLostSound;
    public AudioClip gameEndSound;

    private AudioSource audioSource;


    public TextMeshProUGUI winnerText; 
   

    private void Awake()
    {
        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize player lives
            playerLives = new int[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                playerLives[i] = 3; // Set initial lives to 3 for each player
            }
        }
    }

    // Call this method when a player gets hit by an explosion
    public void PlayerHit(int playerIndex)
    {
        playerLives[playerIndex]--;
        if (playerLives[playerIndex] <= 0)
        {
            // Player has no lives left, deactivate them and check if the game should end
            players[playerIndex].SetActive(false);
            CheckForLastPlayer(); // This function will handle if the game should end
        }
        else
        {
            // Player still has lives left, respawn them
            StartCoroutine(RespawnPlayer(players[playerIndex], playerIndex));

            // Play the life lost sound
            audioSource.PlayOneShot(lifeLostSound);
        }
    }


    private void CheckForLastPlayer()
    {
        int aliveCount = 0;
        GameObject lastPlayerAlive = null;
        foreach (var player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
                lastPlayerAlive = player;
            }
        }

        if (aliveCount <= 1) // <= 1 in case all players died at the same time
        {
            winner = aliveCount == 1 ? lastPlayerAlive.name : "No one"; // Set the winner's name or "No one"
            LoadGameOverScene();
        }
    }

    private IEnumerator RespawnPlayer(GameObject player, int playerIndex)
    {
        // Wait for death animation to finish, then respawn the player
        yield return new WaitForSeconds(1.25f); 

        // Respawn the player at their original position
        player.transform.position = player.GetComponent<Movement>().originalPosition;
        player.SetActive(true);

        // Ensure the player's Movement and other components are re-enabled
        player.GetComponent<Movement>().ResetToAliveState();
    }


    // Load the Game Over scene and display the winner
    private void LoadGameOverScene()
        {
        // Play the game end sound
        audioSource.PlayOneShot(gameEndSound);

        // Set the winner information before loading the scene
        PlayerPrefs.SetString("WinnerName", winner);
            SceneManager.LoadScene("GameOver");
        }

    // This method would be in the Game Over scene's script
    private void DisplayWinner()
    {
        // Get the winner's name that was set before loading the scene
        string winnerName = PlayerPrefs.GetString("WinnerName", "Unknown");

        
        TextMeshProUGUI winnerText = GameObject.Find("WinnerText").GetComponent<TextMeshProUGUI>();
        if (winnerText != null)
        {
            winnerText.text = winnerName + " Wins!";
        }
    }
}