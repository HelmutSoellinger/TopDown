using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    public GameObject player1Prefab;  // Drag and drop Player 1 prefab here
    public GameObject player2Prefab;  // Drag and drop Player 2 prefab here

    private GameObject player1Instance;
    private GameObject player2Instance;

    public Camera _P1Cam;
    public Camera _P2Cam;

    void Start()
    {
        // Instantiate each player at a specific spawn point
        player1Instance = Instantiate(player1Prefab);
        player2Instance = Instantiate(player2Prefab);

        // Optionally, you can get references to the cameras in the scene at runtime
        if (_P1Cam == null)
            _P1Cam = GameObject.Find("P1Cam").GetComponent<Camera>(); ;
        if (_P2Cam == null)
            _P2Cam = GameObject.Find("P2Cam").GetComponent<Camera>(); ;

    AssignControllersAndCameras();
        
    }

    private void AssignControllersAndCameras()
    {
        if (Gamepad.all.Count < 2)
        {
            Debug.LogWarning("Not enough controllers connected. Connect two gamepads.");
            return;
        }

        // Access the PlayerInput component on each player instance
        PlayerInput player1Input = player1Instance.GetComponent<PlayerInput>();
        PlayerInput player2Input = player2Instance.GetComponent<PlayerInput>();

        // Access the ThirdPersonController component
        ThirdPersonController player1Controller = player1Instance.GetComponent<ThirdPersonController>();
        ThirdPersonController player2Controller = player2Instance.GetComponent<ThirdPersonController>();

        // Assign specific gamepads to each player’s PlayerInput
        player1Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
        // Player 1: Assign Player 1's camera
        player1Input.camera = _P1Cam;
        player2Input.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        // Player 2: Assign Player 2's camera
        player2Input.camera = _P2Cam;

        // Assign the cameras to the player's ThirdPersonController
        player1Controller._P1Cam = _P1Cam;  // Assign camera for Player 1
        player2Controller._P2Cam = _P2Cam;  // Assign camera for Player 2


        Debug.Log("Controllers assigned: Gamepad 0 to Player 1, Gamepad 1 to Player 2");
    }
}
