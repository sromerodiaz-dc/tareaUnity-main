using System.Collections;
using UnityEngine;

public class SpherePortalController : MonoBehaviour
{
    public Vector3 tama�oMaximo = new Vector3(1f, 1f, 1f); // Tama�o final
    public float velocidadCrecimiento = 0.02f; // Crecimiento gradual

    private Vector3 tama�oInicial = Vector3.zero;
    private bool activado = false;

    void Start()
    {
        transform.localScale = tama�oInicial;  // Mantener invisible al inicio
    }

    private void OnEnable()
    {
        ScoreManager.OnSeisPuntos += ActivarEsfera;
    }

    private void OnDisable()
    {
        ScoreManager.OnSeisPuntos -= ActivarEsfera;
    }

    public void ActivarEsfera()
    {
        if (!activado)
        {
            activado = true;
            StartCoroutine(AparecerGradualmente());
        }
    }

    private IEnumerator AparecerGradualmente()
    {
        float progreso = 0f;
        
        while (progreso < 1f)
        {
            progreso += Time.deltaTime * velocidadCrecimiento;
            transform.localScale = Vector3.Lerp(tama�oInicial, tama�oMaximo, progreso);
            yield return null;
        }
    }
}