using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa;
    public Button botonInicial;
    public Button botonSecundario;
    public bool token;
    // Start is called before the first frame update
    void Start()
    {
        token = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            
            Menu();
        }
    }

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

    public bool getToken()
    {
        return token;
    }
}
