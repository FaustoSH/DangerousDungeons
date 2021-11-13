using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MikeController : MonoBehaviour
{
    private Animator animator;
    public GameObject Espada;
    public GameObject Escudo;
    public GameObject Hacha;
    public Image barraEstamina;
    public Sprite[] spriteStamina;
    public Image barraVida;
    public Sprite[] spriteVida;
    private int estamina;
    private float estaminaVariable;
    private int vida;
    private float vidaVariable;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();//Eliminar esta linea cuando se vaya a exportar el juego

        estamina = 10;
        estaminaVariable = 0;
        vida = 10;
        vidaVariable = 0;


        
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
            GestionInventario();
        }

    }

    // Update is called once per frame
    void Update()
    {
        float cambioPorSegundoEstamina = 0.5f, cambioPorSegundoVida=0.2f;
        estaminaVariable += cambioPorSegundoEstamina * Time.deltaTime;
        vidaVariable += cambioPorSegundoVida * Time.deltaTime;
        if(estaminaVariable>=1.0)
        {
            if (estamina < 10)
                estamina++;
            estaminaVariable = 0;
        }
        if(vidaVariable>=1.0)
        {
            if (vida < 10)
                vida++;
            vidaVariable = 0;
        }
        Debug.Log("Vida="+vida);


        AtaqueYMovimiento();


        barraEstamina.sprite = spriteStamina[estamina];
        barraVida.sprite = spriteVida[vida];
       
        GestionInventario();
    }
    void AtaqueYMovimiento()
    {
        //Debug.Log(animator.GetInteger("Velocidad") + "-" + animator.GetInteger("Direccion"));
        Vector3 position;
        Quaternion quaternion;
        float giro = 0;
        position = new Vector3(0, 0, 0);
        quaternion = transform.rotation;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Atacando"))
        {
            if (Input.GetKey(KeyCode.UpArrow)&&estamina>=3 && PlayerPrefs.GetInt("Inventario") != 0)
            {
                estamina -= 3;
                animator.SetInteger("Ataque", 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow)&& estamina >= 7 && PlayerPrefs.GetInt("Inventario") != 0)
            {
                estamina -= 7;
                animator.SetInteger("Ataque", 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow)&&estamina >= 2 && PlayerPrefs.GetInt("Inventario") != 0)
            {
                estamina -= 2;
                animator.SetInteger("Ataque", 2);
            }
            else if (Input.GetKey(KeyCode.RightArrow)&&estamina >= 3&&PlayerPrefs.GetInt("Inventario")!=0)
            {
                estamina -= 3;
                animator.SetInteger("Ataque", 3);
            }
            else if (Input.GetKey(KeyCode.W))
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
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
            }

            transform.position += quaternion * position;
            quaternion *= Quaternion.Euler(0, giro, 0);
            transform.rotation = quaternion;

            if (Input.GetKey(KeyCode.Alpha0))
            {
                PlayerPrefs.SetInt("Inventario", 0);
                //Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                PlayerPrefs.SetInt("Inventario", 1);
                //Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                PlayerPrefs.SetInt("Inventario", 2);
                //Debug.Log("Inventario: " + PlayerPrefs.GetInt("Inventario"));
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
