using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script que controla el comportamiento del menú de pausa ingame.
public class PauseMenu : MonoBehaviour
{
    [Header("Gameobject referenciado")]
    public GameObject menuPausa;

    [Header("Botones del menú de pausa:")]
    public Button botonInicial;
    public Button botonSecundario;

    private bool token;

    void Start()
    {
        token = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Menu();
        }
    }

    //Método encargado de mostrar u ocultar alternativamente el
    //menú de pausa, de acuerdo al valor del token.
    public void Menu()
    {
        if (!token)
        {
            menuPausa.SetActive(true);
            botonInicial.Select();
            botonSecundario.Select();
            token = true;
            Time.timeScale = 0;
        }
        else if (token)
        {
            Time.timeScale = 1;
            menuPausa.SetActive(false);
            token = false;
        }

    }


    //Función visible para el resto de clases. Se encarga de
    //indicar si esta la partida en pausa.
    public bool getToken()
    {
        return token;
    }
}
