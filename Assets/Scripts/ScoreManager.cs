using UnityEngine;
using System; // Necesario para usar eventos
using System.Collections;
using TMPro;

public class ScoreEventsManager : MonoBehaviour
{
    public static event Action<int> OnPuntuacionActualizada; // Evento que envía el puntaje actual
    public static event Action OnPortalTrigger; // Evento cuando llega a 6 puntos
    public static event Action OnEnemyDoor; // Evento cuando llega a 6 puntos
    public static event Action OnEnemyZone; // Evento cuando llega a 6 puntos

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject enemyDoor;
    [SerializeField]
    private GameObject enemySecondDoor;
    public TextMeshProUGUI portalText;
    public TextMeshProUGUI enemyDoorText;
    public TextMeshProUGUI enemyZoneText;

    private int puntuacion = 0; // Puntuación del jugador
    private int portalPts = 7;
    private int enemyDoorPts = 7;

    void Start()
    {
        setText();
    }

    void Update()
    {
        setText();
    }

    public void AumentarPuntos(String tipo)
    {
        int puntos = 1; 

        if (tipo == "PickUp")
        {
            puntuacion += puntos;
            enemyDoorPts -= puntos;
        }
        else if (tipo == "portal")
        {
            portalPts -= puntos;
        }

        Debug.Log("Puntuación: " + puntuacion);

        // Disparar el evento y enviar la puntuación actual
        OnPuntuacionActualizada?.Invoke(puntuacion);

        if (puntuacion > 6)
        {
            OnEnemyDoor?.Invoke();
            enemyDoor.GetComponent<Animator>().SetBool("isOpen", true);
        }

        if (portalPts == 0)
        {
            OnPortalTrigger?.Invoke();
            door.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }

    void setText() 
    {
        portalText.text = portalPts.ToString();
    }
}
