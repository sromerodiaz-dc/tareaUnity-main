using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, PlayerControls.IPlayerActions
{
    public GameObject player;

    private PlayerControls controls;
    private Vector2 lookInput;
    private bool isFirstPerson = false;
    private bool isForcedFirstPerson = false; // Nueva variable para el evento.

    public float thirdPersonHeight = 10f;
    public float thirdPersonDistance = 10f;
    private Vector3 thirdPersonOffset;

    public float sensitivity = 1.0f;
    public float rotationSpeed = 50f;
    public float firstPersonHeightOffset = 0.5f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        ScoreEventsManager.OnEnemyZone += HandleEnemyZoneFirstPerson; // Suscribirse al evento
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        ScoreEventsManager.OnEnemyZone -= HandleEnemyZoneFirstPerson; // Desuscribirse del evento
    }

    private void Start()
    {
        thirdPersonOffset = new Vector3(0, thirdPersonHeight, -thirdPersonDistance);
    }

    private void Update()
    {
        // Si el modo primera persona está forzado, ignorar cambios manuales
        if (!isForcedFirstPerson && controls.Player.NumberKeys.triggered)
        {
            var control = controls.Player.NumberKeys.activeControl;
            if (control != null)
            {
                string keyPressed = control.displayName;
                Debug.Log($"Tecla presionada: {keyPressed}");

                switch (keyPressed)
                {
                    case "1":
                        isFirstPerson = false; // Cambiar a tercera persona.
                        break;
                    case "2":
                        isFirstPerson = true; // Cambiar a primera persona.
                        break;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (isFirstPerson || isForcedFirstPerson)
        {
            Debug.Log("Updating First Person View");
            UpdateFirstPersonView();
        }
        else
        {
            Debug.Log("Updating Third Person View");
            UpdateThirdPersonView();
        }
    }


    private void UpdateThirdPersonView()
    {
        Vector3 desiredPosition = player.transform.position + thirdPersonOffset;
        transform.position = desiredPosition;
        transform.LookAt(player.transform.position);
    }

    private void UpdateFirstPersonView()
    {
        transform.position = player.transform.position + Vector3.up * firstPersonHeightOffset;

        float adjustedSensitivity = rotationSpeed * sensitivity;
        rotationX = Mathf.Clamp(rotationX - lookInput.y * adjustedSensitivity * Time.deltaTime, -90f, 90f);
        rotationY += lookInput.x * adjustedSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lookInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            lookInput = Vector2.zero;
        }
    }

    public void OnMove(InputAction.CallbackContext context) { }

    public void OnNumberKeys(InputAction.CallbackContext context) { }

    private void HandleEnemyZoneFirstPerson(int enemyTrapDoorPts)
    {
        Debug.Log($"Enemy Zone Triggered - enemyTrapDoorPts: {enemyTrapDoorPts}");
    
        if (enemyTrapDoorPts > 0)
        {
            isForcedFirstPerson = true;
            Debug.Log("Forced First Person: ON");
        }
        else
        {
            isForcedFirstPerson = false;
            Debug.Log("Forced First Person: OFF");
        }
    }

}
