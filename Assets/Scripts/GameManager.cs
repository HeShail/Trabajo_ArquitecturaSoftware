using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TerminarJuego()
    {
        Application.Quit();
    }

    public void CargarEscena()
    {
        SceneManager.LoadScene(1);
    }

    public void CargarEscena0()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
