
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText; 

    void Start()
    {
        DisplayWinner();
    }

    private void DisplayWinner()
    {
        string winnerName = PlayerPrefs.GetString("WinnerName", "Unknown");
        winnerText.text = winnerName + " Wins!";
    }
}
