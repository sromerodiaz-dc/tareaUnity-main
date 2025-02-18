    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        // AI de Unity
        public Transform player;
        public float speed = 0.1f;

        private NavMeshAgent Agent;

        // Final de juego
        public GameStateManager manager;

        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        void OnEnable()
        {
            Debug.Log($"{gameObject.name} ha sido activado.");
            StartCoroutine(Follow());
        }


        IEnumerator Follow()
        {
            WaitForSeconds wait = new WaitForSeconds(speed);

            while (enabled)
            {
                if (Agent.isActiveAndEnabled) 
                {
                    Debug.Log($"{gameObject.name} moviéndose hacia {player.position}");
                    Agent.SetDestination(player.position);
                }
                else
                {
                    Debug.LogWarning($"{gameObject.name} tiene NavMeshAgent deshabilitado.");
                }
                yield return wait;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))  // Si el objeto tiene la etiqueta "PickUp"
            {
                other.gameObject.SetActive(false);
                manager.LoseGame();
            }
        }
    }
