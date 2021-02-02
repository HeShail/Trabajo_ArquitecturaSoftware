using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script que controla el grueso de la secuencia de ejecución del juego.
//Incluye la mecánica de gol, recolocación de personajes, animación de 
//mensajes y activación de audioclips y textos.
public class PartidoManager : MonoBehaviour
{
    #region Atributos
    [Header("Scripts de jugadores")]
    public CHUTAR scriptChutar;
    public CHUTAR scriptChutar2;

    [Header("Posiciones iniciales")]
    public Transform posicionBalon;
    public Transform posicionP1;
    public Transform posicionP2;

    [Header("Gameobjects")]
    public GameObject player1;
    public GameObject player2;
    public GameObject balon;

    [Header("Animators de mensajes")]
    public Animator tiempoDescanso;
    public Animator VictoriaP1;
    public Animator VictoriaP2;
    public Animator golAnim;

    [Header("Audio Sources")]
    public AudioSource golSound;
    public AudioSource pitidoT;
    public AudioSource pitidoFin;

    [Header("Textos")]
    public TextMeshProUGUI textCrono;
    public TextMeshProUGUI textMarcador;
    public TextMeshProUGUI textoPosesionP1;
    public TextMeshProUGUI textoPosesionP2;

    [Header("Velocidad de partido")]
    [Range(-20.0f, 20.0f)] public float escalaTiempo;
    private float startTime;
    private float tiempoPosesion;

    private bool pausado;
    private bool relocando;
    private bool primeraParte;
    private int puntosP1;
    private int puntosP2;
    private string cosa;

    [Header("Singleton")]
    public static PartidoManager manager;

    #endregion


    //Tratamiento del singleton de PartidoManager
    void Awake()
    {

        if (manager == null)
        {

            manager = this;
            DontDestroyOnLoad(this.gameObject);


        }
        else
        {
            Destroy(this);
        }
    }

    //Inicialización de atributos.
    void Start()
    {
        Time.timeScale = 1f;
        puntosP1 = 0;
        puntosP2 = 0;
        primeraParte = true;
        pausado = false;
        startTime = Time.time;
        tiempoPosesion = 0.2f;
    }


    void Update()
    {
        tiempoPosesion = scriptChutar.getTiempoTotal() + scriptChutar2.getTiempoTotal();
        textoPosesionP1.text = (scriptChutar.getTiempoP1() / tiempoPosesion* 100).ToString("00") + " %";
        textoPosesionP2.text = ((tiempoPosesion-scriptChutar.getTiempoP1()) / tiempoPosesion * 100).ToString("00") + " %";

        //Bloque de código que actualiza el marcador de tiempo, interrumpiendo el tiempo
        //cuando se anota gol o termina la primera parte del juego.
        if (!pausado)
        {
            float t = (Time.time - startTime)*escalaTiempo;
            int minutos = (int)t / 60;
            int segundos = (int)t % 60;
            if ((minutos < 20))
            {
                cosa = minutos.ToString("00") + ":" + segundos.ToString("00");
                
            }else if (minutos == 20)
            {
                if (segundos == 00)
                {
                    cosa = minutos.ToString("00") + ":" + segundos.ToString("00");
                }
            }

            //Secuencia de código que trata el término del primer tiempo.
            //Libera el balón, ralentiza el tiempo e invoca una recolocación.
            if ((minutos == 10)&& primeraParte)
            {
                pitidoT.Play();
                pausado = true;
                relocando = true;
                scriptChutar.DejarBalon();
                scriptChutar2.DejarBalon();
                Time.timeScale = 0.22f;
                Invoke("Relocar", 1.0f);
                Invoke("DevolverTrail", 1.2f);
                balon.GetComponent<Rigidbody>().drag = 5f;
                primeraParte = false;
                tiempoDescanso.SetTrigger("tiempo");
            }else if ((minutos == 21) && !primeraParte)
            {
                if (puntosP1 > puntosP2) VictoriaP1.SetTrigger("victoria"); else VictoriaP2.SetTrigger("victoria");
                
                Invoke("CargarJuego", 4f);
            }
            textCrono.text = cosa;
            if (minutos == 19f)
            {
                pitidoFin.Play();
            }
            if (minutos== 22f) Time.timeScale = 0.22f;
        }
    }

    //Función que tramita el gol. Anota y ejecuta el audioclip de gol,
    //ralentiza el tiempo, libera el balón y llama a la función de recolocación
    //de los jugadores y esférico.
    public void GOL()
    {
        GameObject.Find("FireworksPooler").GetComponent<FireworksPooler>().Descarga();
        golSound.Play();
        pausado = true;
        relocando = true;
        scriptChutar.DejarBalon();
        scriptChutar2.DejarBalon();
        Time.timeScale = 0.22f;
        Invoke("Relocar", 1.0f);
        Invoke("DevolverTrail", 2f);
        golAnim.SetTrigger("gol");
        balon.GetComponent<Rigidbody>().drag = 5f;
        textMarcador.text = puntosP1.ToString() + " - " + puntosP2.ToString();
    }

    //Método que devuelve a los personajes de los jugadores y al balón
    //a las posiciones iniciales del inicio del encuentro. Para ello necesita detener el balón
    //lo antes posible(drag), desactivar los character controller y modificar los vectores
    //de posición y rotación.
    public void Relocar()
    {

        balon.GetComponent<TrailRenderer>().time = 0f;
        player1.GetComponent<CharacterController>().enabled = false;
        player2.GetComponent<CharacterController>().enabled = false;
        player1.transform.position = posicionP1.position;
        player2.transform.position = posicionP2.position;
        balon.transform.position = posicionBalon.position;

        player1.transform.rotation = posicionP1.rotation;
        player2.transform.rotation = posicionP2.rotation;
        balon.transform.rotation = posicionBalon.rotation;
        player1.GetComponent<CharacterController>().enabled = true;
        player2.GetComponent<CharacterController>().enabled = true;
        Time.timeScale = 1f;
        relocando = false;
        balon.GetComponent<Rigidbody>().drag = 0.5f;
    }

    //Función que devuelve el trail al balón en un lapso de tiempo
    //para no presentar un latigazo en el campo tras su reubicación. 
    public void DevolverTrail()
    {
        pausado = false;
        balon.GetComponent<TrailRenderer>().time = 0.2f;
    }

    //Llamada a singleton de GameManager para cargar el juego de nuevo.
    public void CargarJuego()
    {
        GameManager.sceneManager.CargarEscena();
    }

    public bool relocado()
    {
        return relocando;
    }
    
    public void PuntuaP1()
    {
        puntosP1++;
    }
    public void PuntuaP2()
    {
        puntosP2++;
    }

    public int getPuntosP1()
    {
        return puntosP1;
    }
    public int getPuntosP2()
    {
        return puntosP2;
    }

}
