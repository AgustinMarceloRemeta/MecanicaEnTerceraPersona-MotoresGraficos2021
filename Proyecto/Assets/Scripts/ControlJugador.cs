using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    public Rigidbody rb;
    public int rapidez;
    public Text textoCantidadRecolectados;
    public Text textoGanaste;
    public Text TextoMuerte;
    private int cont;
    private int Cont2;
    public LayerMask capaPiso;
    public float magnitudSalto;
    public SphereCollider col;
    public Text TextoBienvenido;
    private int Cont3;
    private int Cont4;
    public Text TextoFinal;
    
 



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        cont = 0;
        textoGanaste.text = "";
        TextoMuerte.text = "";
        TextoBienvenido.text = "";
        setearTextos();
        Cont2 = 0;
        Cont3 = 0;
        Cont4 = 0;
        TextoFinal.text = "";
        



    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(5f, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(0, 0, -5f);

        }
        if (Input.GetKey(KeyCode.A))
        {

            rb.AddForce(0, 0, 5f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-2f, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.R))
        { SceneManager.LoadScene("SampleScene"); }
    }
    private void ReiniciarEscena()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void CambioEscena ()
    {
        SceneManager.LoadScene("EscenaVictoria");
    }
    private void setearTextos()
    {
        textoCantidadRecolectados.text = "Objetos recolectados: " + cont.ToString();
        TextoBienvenido.text = "Bienvenido a este prototipo de juego tu objetivo es conseguir 7 objetos, suerte crack.";
       
        if (Cont2 >= 1)
        {
            TextoMuerte.text = "Has muerto";
            InvokeRepeating("ReiniciarEscena", 2.0f,0);
        }


        if (Cont3 >= 1)
        {
            TextoBienvenido.text = "";
        }
        if (Cont4 >= 1)
        {
            if (cont >= 7)
            {
                textoGanaste.text = "Ganaste!";
                InvokeRepeating("CambioEscena", 3.0f, 0);

            }
            if (cont<= 0)
            {
                SceneManager.LoadScene("FinalSecreto");
            }
            if (cont >=0 && cont<= 7)
            {
                TextoFinal.text = "Te caiste a pedazos, toca R para reiniciar.";
                
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coleccionable") == true)
        {
            cont = cont + 1;
            setearTextos();
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("muerte") == true)
        {
            Cont2 = Cont2 + 1;
            setearTextos();
            other.gameObject.SetActive(false);
            

        }
        if (other.gameObject.CompareTag("comienzo") == true)
        {
            Cont3 = Cont3 + 1;
            setearTextos();
            other.gameObject.SetActive(false);


        }
        if (other.gameObject.CompareTag("final") == true)
        {
            Cont4 = Cont4 + 1;
            setearTextos();
            other.gameObject.SetActive(false);


        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso())
        {
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
        }
    }

        private bool EstaEnPiso()
        {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .9f, capaPiso);
        }

    }

