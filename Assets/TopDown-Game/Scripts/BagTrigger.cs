using UnityEngine;

public class BagTrigger : MonoBehaviour
{
    private Light playerLight; // Reference to the player's light

    void Start()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        // Debug log to check if the player is exiting the trigger
        Debug.Log("OnTriggerExit called, collider: " + other.gameObject.name);

        // Check if the object that exited has the "LightPlayer" tag
        if (other.CompareTag("Player1")|| other.CompareTag("Player2"))
        {
            Debug.Log("Player with LightPlayer tag exited trigger");
            playerLight = other.GetComponentInChildren<Light>();


            if (playerLight != null)
            {
                playerLight.enabled = true; // Turn on the player's light when exiting the trigger
            }
            else
            {
                Debug.LogWarning("No Light component found in the player's children.");
            }

        }
    }
}
