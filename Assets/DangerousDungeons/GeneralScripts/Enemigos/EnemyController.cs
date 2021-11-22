using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int vida;
    private Animator animator;
    private float tiempoMuerto;
    private int vidaAnterior;
    private GameObject Mike;
    private float Distancia;
    public float DistanciaMinima;
    private float velocidad;
    public RaycastHit rayo;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        //La inicialización de la vida se hace mediante la interfaz ya que los diferentes enemigos tendrán diferentes vidas iniciales
        animator = this.gameObject.GetComponent<Animator>();
        tiempoMuerto = 0;
        vidaAnterior = vida;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Mike = GameObject.Find("Mike");
    }

    private void Update()
    {
        animator.SetInteger("Vida", vida);
        if (vida <= 0)
        {
            tiempoMuerto += 1.0f * Time.deltaTime;
            if(tiempoMuerto>=7)
            {
                //Después de 7 segundos muerto se elimina al enemigo para que no gaste memoria
                Destroy(this.gameObject);
            }
        }else if(vida<vidaAnterior)
        {
            animator.SetTrigger("Golpe");
            vidaAnterior = vida;
        }else
        {
            Vector3 posicionFinal = new Vector3(Mike.transform.position.x + 0.5f, Mike.transform.position.y, Mike.transform.position.z + 0.5f);
            navMeshAgent.destination = posicionFinal;
            Debug.Log(navMeshAgent.remainingDistance);
            if(navMeshAgent.remainingDistance<=0.7)
            {
                animator.SetBool("Andar", false);
            }
            else
            {
                animator.SetBool("Andar", true);
            }
               
        }
    }
}
