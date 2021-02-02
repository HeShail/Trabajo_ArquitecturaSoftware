using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase que administra el control de escenas y salida del juego.
public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager sceneManager;

    void Awake()
    {

        if (sceneManager == null)
        {

            sceneManager = this;
            DontDestroyOnLoad(this.gameObject);


        }
        else
        {
            Destroy(this);
        }
    }

    //Función encargada de finalizar la ejecución de unity.
    public void TerminarJuego()
    {
        Application.Quit();
    }

    public void CargarEscena()
    {
        SceneManager.LoadScene(0);
    }
}
