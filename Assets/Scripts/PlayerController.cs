using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    private Rigidbody rb;
    private Vector2 movementInput;
    private Vector2 lookInput;

    public float speed = 10f;  // Velocidad de movimiento.
    public Camera playerCamera;  // Referencia a la cámara para el cálculo de la dirección de movimiento.

    public TextMeshProUGUI text;

    private ScoreEventsManager scoreManager;
 
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);  // Registra los métodos de entrada.
        rb = GetComponent<Rigidbody>();  // Obtén el Rigidbody de la bola.

        scoreManager = FindObjectOfType<ScoreEventsManager>();  
        
        setCount(0);
    }

    private void OnEnable()
    {
        controls.Player.Enable();  // Habilita las acciones del jugador.
        ScoreEventsManager.OnPuntuacionActualizada += setCount;
        ScoreEventsManager.OnEnemyZone += setPortalCount;
    }

    private void OnDisable()
    {
        controls.Player.Disable();  // Deshabilita las acciones del jugador.
        ScoreEventsManager.OnPuntuacionActualizada -= setCount; // Desuscribirse al evento para evitar errores
        ScoreEventsManager.OnEnemyZone -= setPortalCount;
    }

    // Método llamado cuando se recibe la entrada de movimiento
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();  // Lee la entrada de movimiento.
        }
        else if (context.canceled)
        {
            movementInput = Vector2.zero;  // Resetea la entrada de movimiento cuando se cancela.
        }
    }

    // Método llamado cuando se recibe la entrada del mouse
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lookInput = context.ReadValue<Vector2>();  // Lee el movimiento del mouse.
        }
        else if (context.canceled)
        {
            lookInput = Vector2.zero;  // Resetea la entrada del mouse cuando se cancela.
        }
    }
    
    public void OnNumberKeys(InputAction.CallbackContext context)
    { }

    void setCount(int pts)
    {
        text.text = "Monedas joseadas: " + pts.ToString();
    }

    void setPortalCount(int pts)
    {
        text.text = "Puntos restantes: " + pts.ToString();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            // Calcula la dirección hacia adelante y hacia la derecha basándonos en la cámara.
            Vector3 forward = playerCamera.transform.forward;
            forward.y = 0f;  // Evita que se mueva hacia arriba/abajo.
            forward.Normalize();  // Normaliza la dirección para mantener una velocidad constante.

            Vector3 right = playerCamera.transform.right;
            right.y = 0f;  // Evita que se mueva hacia arriba/abajo.
            right.Normalize();

            // Calcula el vector de movimiento en función de la entrada.
            Vector3 movement = (forward * movementInput.y + right * movementInput.x) * speed;

            // Aplica la fuerza al Rigidbody.
            rb.AddForce(movement, ForceMode.Force);  // Utiliza ForceMode.Force para aplicar una fuerza constante.
        }
    }

    // Detectar las colisiones con los pick-ups
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;

        // Verifica si el objeto tiene uno de los tags permitidos antes de destruirlo
        if (tag == "PickUp" || tag == "OnEnemyZone" || tag == "PortalPts")
        {
            Destroy(other.gameObject);

            if (scoreManager != null)
            {
                scoreManager.AumentarPuntos(tag); // Suma un punto
            }
        }
    }


    // Detectar las colisiones con los pick-ups
    private IEnumerator ApplyTemporaryBoost()
    {
        // Calcula la dirección en la que el jugador se está moviendo basado en su velocidad actual.
        Vector3 movementDirection = rb.velocity.normalized; // Usa la dirección del movimiento actual del jugador.

        // Si el jugador no se está moviendo (velocidad cero), no aplicamos el boost.
        if (rb.velocity.magnitude > 0.1f) // Pequeño umbral para evitar fluctuaciones menores
        {
            // Aumentar la velocidad en la dirección de movimiento actual.
            Vector3 boostForce = movementDirection * 40f; // Fuerza adicional, ajusta la magnitud.
            rb.AddForce(boostForce, ForceMode.Impulse);  // Aplicamos un impulso en la dirección del movimiento.
        }

        yield return new WaitForSeconds(1f);

        // Asegurarse de que la velocidad final no sea menor que la velocidad mínima.
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed; // Mantener la velocidad mínima.
        }
    }
}
