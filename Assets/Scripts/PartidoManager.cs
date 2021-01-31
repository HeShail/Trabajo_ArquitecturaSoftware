using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartidoManager : MonoBehaviour
{
    public Animator tiempoDescanso;
    public Animator VictoriaP1;
    public Animator VictoriaP2;
    public AudioSource golSound;
    public AudioSource pitidoT;
    public AudioSource pitidoFin;
    public TextMeshProUGUI textCrono;
    public TextMeshProUGUI textMarcador;
    [Range(-20.0f,20.0f)] public float escalaTiempo;
    private bool primeraParte;
    private string cosa;
    private float startTime;
    private bool pausado;
    public Transform posicionBalon;
    public Transform posicionP1;
    public Transform posicionP2;
    private int puntosP1;
    private int puntosP2;
    public GameObject player1;
    public GameObject player2;
    public GameObject balon;
    public CHUTAR scriptChutar;
    public CHUTAR scriptChutar2;
    public TextMeshProUGUI textoPosesionP1;
    public TextMeshProUGUI textoPosesionP2;
    private bool relocando;
    private float tiempoPosesion;
    public Animator golAnim;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        tiempoPosesion = scriptChutar.getTiempoTotal() + scriptChutar2.getTiempoTotal();
        textoPosesionP1.text = (scriptChutar.getTiempoP1() / tiempoPosesion* 100).ToString("00") + " %";
        textoPosesionP2.text = ((tiempoPosesion-scriptChutar.getTiempoP1()) / tiempoPosesion * 100).ToString("00") + " %";


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
                
                Invoke("SalirAMenu", 4f);
            }
            textCrono.text = cosa;
            if (minutos == 19f)
            {
                pitidoFin.Play();
            }
            if (minutos== 22f) Time.timeScale = 0.22f;
        }
    }

    public void SalirAMenu()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().CargarEscena0();
    }
    public void GOL()
    {
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

    public void DevolverTrail()
    {
        pausado = false;
        balon.GetComponent<TrailRenderer>().time = 0.2f;
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
