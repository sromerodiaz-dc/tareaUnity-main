using UnityEngine;
using System; // Necesario para usar eventos
using TMPro;

public class ScoreEventsManager : MonoBehaviour
{
    /**
    Prompt para ChatGPT

    Quiero que el codigo pase tanto portalPts como enemyTrapDoorPts por evento para que luego playerController (suscrito a dichos eventos) 
    pueda modificar su UI y que aparezcan ambas puntuaciones hasta que player recoja todos los puntos (portalPts y enemyTrapDoorPts), 
    momento en el que se restablecería el puntaje anterior
    */
    // Eventos para actualizar la puntuación y activar acciones específicas
    public static event Action<int> OnPuntuacionActualizada;
    public static event Action OnPortalTrigger;
    public static event Action<int> OnEnemyZone;
    public static event Action<int> OnPortalPts;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject enemyEntryDoor;
    [SerializeField] private GameObject enemyTrapDoor;

    [SerializeField] private TextMeshProUGUI portalText;
    [SerializeField] private TextMeshProUGUI enemyDoorText;
    [SerializeField] private TextMeshProUGUI enemyTrapDoorText;

    private int puntuacion = 0; // Puntuación del jugador
    private int portalPts = 3;
    private int enemyDoorPts = 7;
    private int enemyTrapDoorPts = 9;

    private const int puntosPorAccion = 1; // Puntos que se suman/restan en cada evento

    private void Start()
    {
        ActualizarUI();
    }

    private void Update()
    {
        ActualizarUI();
    }

    /// <summary>
    /// Aumenta puntos en función del tipo de objeto con el que colisiona.
    /// </summary>
    public void AumentarPuntos(string tipo)
    {
        switch (tipo)
        {
            case "PickUp":
                puntuacion += puntosPorAccion;
                enemyDoorPts -= puntosPorAccion;
                OnPuntuacionActualizada?.Invoke(puntuacion);
                Debug.Log($"Puntuación: {puntuacion}");

                if (puntuacion > 6)
                {
                    enemyEntryDoor.GetComponent<Animator>().SetBool("isOpen", true);
                }
                break;

            case "OnEnemyZone":
                enemyTrapDoorPts -= puntosPorAccion;
                enemyTrapDoor.GetComponent<Animator>().SetBool("isOpen", enemyTrapDoorPts > 0);
                OnEnemyZone?.Invoke(enemyTrapDoorPts);
                break;

            case "PortalPts":
                portalPts -= puntosPorAccion;
                OnPortalPts?.Invoke(portalPts);
                if (portalPts == 0)
                {
                    OnPortalTrigger?.Invoke();
                    door.GetComponent<Animator>().SetBool("isOpen", true);
                }
                break;
        }
    }

    /// <summary>
    /// Actualiza los textos de la interfaz de usuario con los valores actuales.
    /// </summary>
    private void ActualizarUI()
    {
        portalText.text = portalPts.ToString();
        enemyDoorText.text = enemyDoorPts.ToString();
        enemyTrapDoorText.text = enemyTrapDoorPts.ToString();
    }
}

