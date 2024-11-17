using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Name oder Index der Spielszene (ersetzen Sie "GameScene" durch den Namen Ihrer Spielszene)
    public string gameSceneName = "GameScene";

    void Start()
    {
       /* // Zeigt den Cursor an und deaktiviert das Locking, wenn das Menü angezeigt wird
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        */
    }

    // Funktion, um die Szene zu wechseln
    public void OnButtonStart()
    {
        Debug.Log("StartGame-Funktion aufgerufen");
        // Prüfen, ob die Spielszene korrekt eingestellt ist
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            // Lädt die Spielszene
            SceneManager.LoadScene(gameSceneName);
            Debug.Log("Wechsel zu Szene: " + gameSceneName);
        }
        else
        {
            Debug.LogError("Spielszene ist nicht angegeben.");
        }
    }

    // Funktion, um das Spiel zu beenden
    public void OnButtonQuitGame()
    {
        Debug.Log("Spiel beenden...");
        Application.Quit();
    }
}
