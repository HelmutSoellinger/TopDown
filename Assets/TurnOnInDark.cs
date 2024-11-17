using UnityEngine;

public class TurnOnInDark : MonoBehaviour
{
    public float darknessThreshold = 0.5f; // Threshold to determine "darkness" level
    private Light playerLight;

    void Start()
    {
        playerLight = GetComponent<Light>();
    }

    void Update()
    {
        // Check the ambient intensity to see if it is dark
        if (RenderSettings.ambientIntensity < darknessThreshold)
        {
            playerLight.enabled = true; // Turn on the light if it's dark
        }
        else
        {
            playerLight.enabled = false; // Turn off the light if it's not dark
        }
    }
}
