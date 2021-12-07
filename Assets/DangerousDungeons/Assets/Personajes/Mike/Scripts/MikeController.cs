/*
    Autor: Fausto Sánchez Hoya
    Descripción: este código se encarga de las acciones principales que desarrolla Mike y su control de vida, estamina, núméro de muertos, etc.
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MikeController : MonoBehaviour
{
    private Animator animator;

    //GameObjects del juego que deberán ser ocultados o mostrados 
    public GameObject Espada;
    public GameObject Escudo;
    public GameObject Hacha;
    public GameObject EscudoMagico;
    public GameObject zonaCuracion;
    
    //La barra de estamina y la de vida que deberán ser actualizadas en función de la vida de Mike
    public Image barraEstamina;
    public Sprite[] spriteStamina;
    public Image barraVida;
    public Sprite[] spriteVida;
    //Los contadores de vida y estamina 
    private int estamina;
    private float contadorEstamina;
    public int vida;
    private float contadorVida;

    //Variables para evitar la interrupción de habilidades o el control de estas
    public int ataqueEnCurso;
    private float enfriamiemtoHabilidades;
    public bool invulnerabilidad;

    //Contador de zombies matados
    public int zombiesMuertos;

    //Clips de audio que reproducir
    public AudioClip andar;
    public AudioClip correr;

    void Start()
    {
        PlayerPrefs.DeleteAll();//En un primer momento la parte del inventario se iba a mantener entre escenas pero ya que el objetivo del juego cambió cerca del final del desarrollo se ha tenido que añadir el borrado de todos los player prefs
                                //Se podrían usar variables normales en su lugar pero su funcionamiento sigue siendo el mismo.

        //Se inicializan las variables
        estamina = 10;
        contadorEstamina = 0;
        vida = 10;
        contadorVida = 0;
        ataqueEnCurso = -1;
        enfriamiemtoHabilidades = 0;
        invulnerabilidad = false;
        zombiesMuertos = 0;


        //Si no se han creado los player prefs que ontrolan el inventario se crean y se inicializan
        animator= GetComponent<Animator>();
        if (!PlayerPrefs.HasKey("Espada"))
        {
            PlayerPrefs.SetInt("Espada", 0);
        }
        if (!PlayerPrefs.HasKey("Hacha"))
        {
            PlayerPrefs.SetInt("Hacha", 0);
        }
        if (!PlayerPrefs.HasKey("Inventario"))
        {
            PlayerPrefs.SetInt("Inventario", 0);
        }else
        {
            GestionInventario(); //Se visualiza el inventario por primera vez
        }

    }

    void Update()
    {
        //Contador
        float cambioPorSegundoEstamina = 0.35f, cambioPorSegundoVida=0.2f;
        contadorEstamina += cambioPorSegundoEstamina * Time.deltaTime;
        contadorVida += cambioPorSegundoVida * Time.deltaTime;
        if (enfriamiemtoHabilidades > 0)
            enfriamiemtoHabilidades -= 1.0f * Time.deltaTime;
        if (contadorEstamina>=1.0) //Cuando se llega a la unidad se aumenta la variable
        {
            if (estamina < 10)
                estamina++;
            contadorEstamina = 0;
        }
        if(contadorVida>=1.0)
        {
            if (vida < 10)
                vida++;
            contadorVida = 0;
        }


        AtaqueYMovimiento(); //Se checkea si se han pulsado alguna tecla de ataque o de movimiento y se realiza la acción correspondiente

        //Después de los cambios que pueden haberse dado en la función AtaqueYMovimiento se actualizan la vida y la estamina
        barraEstamina.sprite = spriteStamina[estamina]; 
        if(vida>=0&&vida<11)
            barraVida.sprite = spriteVida[vida];
       
        GestionInventario(); //Si se ha modificado el arma seleccionada se actualiza la visualización en el inventario
    }



    void AtaqueYMovimiento()
    {
        //Variables locales para el desplazamiento
        Vector3 position;
        Quaternion quaternion;
        float giro = 0;
        position = new Vector3(0, 0, 0);
        quaternion = transform.rotation;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Atacando")) //Si no se está realizando una animación de combate se comprueba si se quiere hacer algún ataque o moverse
        {

            //Teclas de ataques
            if (Input.GetKeyDown(KeyCode.UpArrow)&&estamina>=3 && PlayerPrefs.GetInt("Inventario") != 0 && enfriamiemtoHabilidades<=0) //El enfriamiento de habilidades se ha implementado para evitar un error por intentar lanzar varias habilidades a la vez
            {
                estamina -= 3;
                animator.SetInteger("Ataque", 0);
                ataqueEnCurso = 0;
                if (PlayerPrefs.GetInt("Inventario") == 1)//Como esta habilidad es diferente para cada uno entonces hacemos una diferencia
                {
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    EscudoMagico.SetActive(true);
                    invulnerabilidad = true;
                }
                else if(PlayerPrefs.GetInt("Inventario") == 2)
                {
                    zonaCuracion.SetActive(true);
                    if (vida + 3 <= 10)
                        vida += 3;
                    else
                        vida = 10;
                    
                }
                    enfriamiemtoHabilidades = 1.0f;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)&& estamina >= 7 && PlayerPrefs.GetInt("Inventario") != 0 && enfriamiemtoHabilidades <= 0)
            {
                estamina -= 7;
                ataqueEnCurso = 1;
                if(PlayerPrefs.GetInt("Inventario") ==1)
                {
                    Espada.GetComponent<WeaponsController>().multiplicador = 2;
                    Espada.GetComponent<WeaponsController>().contador = 0;
                }
                animator.SetInteger("Ataque", 1);
                enfriamiemtoHabilidades = 1.0f;

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)&&estamina >= 2 && PlayerPrefs.GetInt("Inventario") != 0 && enfriamiemtoHabilidades <= 0)
            {
                estamina -= 2;
                ataqueEnCurso = 2;
                animator.SetInteger("Ataque", 2);
                enfriamiemtoHabilidades = 1.0f;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)&&estamina >= 3&&PlayerPrefs.GetInt("Inventario")!=0 && enfriamiemtoHabilidades <= 0)
            {
                estamina -= 3;
                ataqueEnCurso = 3;
                animator.SetInteger("Ataque", 3);
                enfriamiemtoHabilidades = 1.0f;

            }

            //Teclas de movimiento
            else if (Input.GetKey(KeyCode.W))
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (!gameObject.GetComponent<AudioSource>().isPlaying||gameObject.GetComponent<AudioSource>().clip!=correr)
                    {
                        gameObject.GetComponent<AudioSource>().clip = correr;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                        
                    position.z = 4 * Time.deltaTime;
                    animator.SetInteger("Velocidad", 2);
                    if (Input.GetKey(KeyCode.D))
                    {
                        giro = (180) * Time.deltaTime;
                        animator.SetInteger("Direccion", 2);
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        giro = (-180) * Time.deltaTime;
                        animator.SetInteger("Direccion", 1);
                    }
                    else
                    {
                        animator.SetInteger("Direccion", 0);
                    }
                }
                else
                {
                    if (!gameObject.GetComponent<AudioSource>().isPlaying || gameObject.GetComponent<AudioSource>().clip != andar)
                    {
                        gameObject.GetComponent<AudioSource>().clip = andar;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                    position.z = 2 * Time.deltaTime;
                    animator.SetInteger("Velocidad", 1);
                    if (Input.GetKey(KeyCode.D))
                    {
                        giro = (90) * Time.deltaTime;
                        animator.SetInteger("Direccion", 2);
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        giro = (-90) * Time.deltaTime;
                        animator.SetInteger("Direccion", 1);

                    }
                    else
                    {
                        animator.SetInteger("Direccion", 0);
                    }
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetInteger("Velocidad", 0);
                animator.SetInteger("Direccion", 1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetInteger("Velocidad", 0);
                animator.SetInteger("Direccion", 2);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetInteger("Velocidad", 0);
                animator.SetInteger("Direccion", 3);
            }
            else
            {
                animator.SetInteger("Velocidad", 0);
                animator.SetInteger("Direccion", 0);                    
                gameObject.GetComponent<AudioSource>().Pause();
            }
            if (enfriamiemtoHabilidades <= 0)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                EscudoMagico.SetActive(false);
                zonaCuracion.SetActive(false);
                invulnerabilidad = false;
            }
            //Se actualiza la posición
            transform.position += quaternion * position;
            quaternion *= Quaternion.Euler(0, giro, 0);
            transform.rotation = quaternion;

            //Teclas de inventario
            if (Input.GetKey(KeyCode.Alpha0))
            {
                PlayerPrefs.SetInt("Inventario", 0);
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                PlayerPrefs.SetInt("Inventario", 1);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                PlayerPrefs.SetInt("Inventario", 2);
            }
        }
    }

    void GestionInventario()
    {
        switch (PlayerPrefs.GetInt("Inventario"))
        {
            case 0:
                Espada.SetActive(false);
                Escudo.SetActive(false);
                Hacha.SetActive(false);
                animator.SetInteger("Arma", 0);
                break;
            case 1:
                Hacha.SetActive(false);
                if (PlayerPrefs.GetInt("Espada") == 1)
                {
                    Espada.SetActive(true);
                    Escudo.SetActive(true);
                    animator.SetInteger("Arma", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Inventario", 0);
                    animator.SetInteger("Arma", 0);
                }
                break;
            case 2:
                Espada.SetActive(false);
                Escudo.SetActive(false);
                if (PlayerPrefs.GetInt("Hacha") == 1)
                {
                    Hacha.SetActive(true);
                    animator.SetInteger("Arma", 2);
                }
                else
                {
                    PlayerPrefs.SetInt("Inventario", 0);
                    animator.SetInteger("Arma", 0);
                }
                break;
            default:
                PlayerPrefs.SetInt("Inventario", 0);
                break;
        }
    }
}
