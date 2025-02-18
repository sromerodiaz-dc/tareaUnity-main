using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject endPanel;

    [SerializeField] GameObject panelPlayer;


    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGame()
    {
        endPanel.SetActive(true);
        panelPlayer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Estás suspenso XD";
    }

    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win";
    }
}
