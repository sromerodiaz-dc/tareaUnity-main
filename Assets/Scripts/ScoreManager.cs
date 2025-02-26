using System;
using TMPro;
using UnityEngine;

public class ScoreEventsManager : MonoBehaviour
{
    public static event Action<int> OnPuntuacionActualizada;
    public static event Action OnPortalTrigger;
    public static event Action<int> OnEnemyZone;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject enemyEntryDoor;
    [SerializeField] private GameObject enemyTrapDoor;

    [SerializeField] private TextMeshProUGUI portalText;
    [SerializeField] private TextMeshProUGUI enemyDoorText;
    [SerializeField] private TextMeshProUGUI enemyTrapDoorText;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Light playerLight;

    private int puntuacion = 0;
    private int enemyDoorPts = 7;
    private int enemyTrapDoorPts = 12;

    private const int PUNTOS_POR_ACCION = 1;
    private const int PUNTUACION_LIMITE = 6;

    private Animator enemyEntryDoorAnimator;
    private Animator enemyTrapDoorAnimator;
    private Animator doorAnimator;

    private void Start()
    {
        enemyEntryDoorAnimator = enemyEntryDoor.GetComponent<Animator>();
        enemyTrapDoorAnimator = enemyTrapDoor.GetComponent<Animator>();
        doorAnimator = door.GetComponent<Animator>();

        if (playerLight == null)
        {
            playerLight = GameObject.FindWithTag("PlayerLight")?.GetComponent<Light>();
            if (playerLight == null)
            {
                Debug.LogError("playerLight no fue encontrado. Asegurate de que la luz esta asignada en el Inspector o con la etiqueta correcta.");
            }
        }

        ActualizarUI();
        enemy.SetActive(false);
    }

    public void AumentarPuntos(string tipo)
    {
        puntuacion += PUNTOS_POR_ACCION;
        OnPuntuacionActualizada?.Invoke(puntuacion);

        if (tipo == "PickUp")
        {
            enemyDoorPts -= PUNTOS_POR_ACCION;
            if (puntuacion > PUNTUACION_LIMITE)
            {
                enemyEntryDoorAnimator.SetBool("isOpen", true);
            }
        }
        else if (tipo == "OnEnemyZone" || tipo == "PortalPts")
        {
            enemyTrapDoorPts -= PUNTOS_POR_ACCION;
            enemyTrapDoorAnimator.SetBool("isOpen", enemyTrapDoorPts > 0);

            if (enemyTrapDoorPts > 0)
            {
                if (!enemy.activeSelf)
                {
                    enemy.SetActive(true);
                }

                playerLight.range = 10f;
            }
            else
            {
                if (enemy.activeSelf)
                {
                    enemy.SetActive(false);
                }

                playerLight.range = 25f;
            }

            OnEnemyZone?.Invoke(enemyTrapDoorPts);
            OnPortalTrigger?.Invoke();  
            doorAnimator.SetBool("isOpen", true);
        }

        ActualizarUI();
    }

    private void ActualizarUI()
    {
        enemyDoorText.text = enemyDoorPts.ToString();
        enemyTrapDoorText.text = enemyTrapDoorPts.ToString();
        portalText.text = enemyTrapDoorPts.ToString(); // O actualiza seg�n corresponda
    }
}
