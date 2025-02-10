using UnityEngine;
using System; // Necesario para usar eventos
using System.Collections;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static event Action<int> OnPuntuacionActualizada; // Evento que envía el puntaje actual
    public static event Action OnSeisPuntos; // Evento cuando llega a 6 puntos

    [SerializeField]
    private GameObject door;
    public TextMeshProUGUI text;

    private int puntuacion = 0; // Puntuación del jugador
    private float totalPts = 7;

    void Start()
    {
        setText();
    }

    void Update()
    {
        setText();
    }

    public void AumentarPuntos(int puntos)
    {
        puntuacion += puntos;
        totalPts -= puntos;
        Debug.Log("Puntuación: " + puntuacion);

        // Disparar el evento y enviar la puntuación actual
        OnPuntuacionActualizada?.Invoke(puntuacion);

        // Si el jugador llega a 6 puntos, activamos otro evento
        if (puntuacion > 6)
        {
            OnSeisPuntos?.Invoke();
            door.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }

    void setText() 
    {
        text.text = totalPts.ToString();
    }
}
