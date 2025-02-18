using UnityEngine;

public class WallMover : MonoBehaviour
{
    private Vector3 positionA;
    private Vector3 positionB;

    public float speed = 0.25f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        positionA = startPosition;
        positionB = startPosition + new Vector3(33, 0, 0); // Puedes cambiar el valor de desplazamiento 
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(positionA, positionB, t);
    }
}
