using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class RoundEndScript : MonoBehaviour
{
    // UI-Elemente für das Overlay
    public TMP_Text roundWonText;
    public TMP_Text counterText;

    // Zeit bis zum Neustart
    public float restartDelay = 10f;

    private float countdown;
    private bool roundWon = false;

    private GameObject winner;
    private GameObject loser;

    public int player1Wins = 0;
    public int player2Wins = 0;
    public int roundsToWin = 3;

    public float speedBoostPercentage;
    // Singleton instance of GoalTriggerObject
    public static RoundEndScript Instance
    {get; private set;}

    void Awake()
    {
        // Ensure that there is only one instance of GoalTriggerObject
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persist between scene loads
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances if already exists
        }
    }

    void Start()
    {
        // Initialisiert das Overlay und setzt den Countdown
        countdown = restartDelay;
        roundWonText.gameObject.SetActive(false);
        counterText.gameObject.SetActive(false);
    }

    // Methode, um den Trigger auszulösen, wenn der Spieler den BoxCollider erreicht
    private void OnTriggerEnter(Collider other)
    {
        if (roundWon) return;

        if (winner == null)
        {
            roundWon = true;

            // Determine which player won and which lost
            if (other.CompareTag("Player1"))
            {
                winner = GameObject.FindGameObjectWithTag("Player1");
                loser = GameObject.FindGameObjectWithTag("Player2");
                player1Wins++;
            }
            else
            {
                winner = GameObject.FindGameObjectWithTag("Player2");
                loser = GameObject.FindGameObjectWithTag("Player1");
                player2Wins++;
            }

            // Display round result
            roundWonText.text = winner.tag + " wins this round!";
            roundWonText.gameObject.SetActive(true);

            // Increase the losing player’s speed if the component is found
            ThirdPersonController loserController = loser.GetComponent<ThirdPersonController>();
            if (loserController != null)
            {
                loserController.IncreaseSpeed(speedBoostPercentage);
            }
            else
            {
                Debug.LogError("ThirdPersonController not found on losing player!");
            }

            // Check if a player has won the game
            if (player1Wins >= roundsToWin || player2Wins >= roundsToWin)
            {
                StartCoroutine(EndGame(winner.tag));
            }
            else
            {
                // Otherwise, continue to next round after countdown
                StartCoroutine(RestartRound());
            }
        }
    }

    // Coroutine, die das Overlay zeigt, den Countdown startet und die Szene neu lädt
    private IEnumerator RestartRound()
    {
        countdown = restartDelay;
        counterText.gameObject.SetActive(true);

        while (countdown > 0)
        {
            counterText.text = "Restarting in: " + Mathf.Ceil(countdown) + "s";
            countdown -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EndGame(string winningPlayer)
    {
        roundWonText.text = winningPlayer + " wins the game!";
        counterText.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        // Exit game or load a main menu scene, etc.
        SceneManager.LoadScene("Menu"); // Or replace with SceneManager.LoadScene("MainMenu");
    }
}

