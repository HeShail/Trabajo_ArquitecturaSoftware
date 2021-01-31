using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    private int ajuste;
    public GameObject flechaS;
    public GameObject subflechaS;
    public GameObject subflechaI;
    public GameObject flechaI;
    public Slider efectos;
    public Slider brillo;
    public Slider graficos;
    public Button ventana;
    public GameObject apartadoVolumen;
    public GameObject apartadoImagen;
    public GameObject apartadoPantalla;
    // Start is called before the first frame update
    void Start()
    {
        ajuste = 0;   
    }

    public void Volver()
    {
        ajuste = 0;
    }

    public void SiguienteAjuste()
    {
        if (ajuste < 2) ajuste++;
        if (ajuste == 2) ActivarPantalla();
    }

    public void AjusteAnterior()
    {
        if (ajuste > 0) ajuste--;
    }

    private void ActivarVolumen()
    {
        subflechaS.SetActive(false);
        subflechaI.SetActive(true);
        flechaS.SetActive(false);
        apartadoVolumen.SetActive(true);
        apartadoImagen.SetActive(false);
        apartadoPantalla.SetActive(false);
        flechaI.SetActive(true);
    }

    private void ActivarImagen()
    {
        subflechaS.SetActive(true);
        subflechaI.SetActive(true);
        flechaS.SetActive(true);
        apartadoVolumen.SetActive(false);
        apartadoImagen.SetActive(true);
        apartadoPantalla.SetActive(false);
        flechaI.SetActive(true);
    }

    private void ActivarPantalla()
    {
        subflechaS.SetActive(true);
        subflechaI.SetActive(false);
        flechaS.SetActive(true);
        flechaI.SetActive(false);
        apartadoVolumen.SetActive(false);
        apartadoImagen.SetActive(false);
        apartadoPantalla.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (ajuste == 0) ActivarVolumen();
        if (ajuste == 1) ActivarImagen();
        if (ajuste == 2) ActivarPantalla();
    }
}
