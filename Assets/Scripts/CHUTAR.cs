﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

//Clase que gestiona la mayor parte de la jugabilidad del personaje
//a excepción del movimiento, centrada en chutar balonazos.
public class CHUTAR : MonoBehaviour {

	#region Atributos
	[Header("Gameobjects implicados")]
	public GameObject esliderP1;
	public GameObject esliderP2;
	public GameObject balon;

	[Header("Textos")]
	public TextMeshProUGUI textochutesP1;
	public TextMeshProUGUI textochutesP2;

	[Header("Booleanos que determinan estados")]
	public bool conBalon = false;
	public bool Player2 = false;

	//Atributos privados
	//Temporizadores, medidores de fuerza y
	//contadores de lanzamientos efecuados.
	private KeyCode miInput;
	private int chutesP1;
	private int chutesP2;
	private float MaxfuerzaUp = 300;
	private float minFuerza = 200;
	private float tiempoP1;
	private float tiempoP2;
	private float tiempoTotal;
	#endregion


	//Inicialización de atributos.
	private void Start()
    {
		esliderP2.SetActive(false);
		esliderP1.SetActive(false);
		tiempoP1 = 0.1f;
		tiempoP2 = 0.1f;
		tiempoTotal = 0.1f;
		chutesP1 = 0;
		chutesP2 = 0;
	}

    void Update () {
		if ((Player2) && chutesP2!= 0) textochutesP2.text = "Lanzamientos: " + chutesP2 + ", goles: " + Mathf.Round((float)GameObject.Find("GameManager").GetComponent<PartidoManager>().getPuntosP2() / chutesP2 * 100) + "%";
		else if ((!Player2 && chutesP1!= 0)) textochutesP1.text = "Lanzamientos: " + chutesP1 + ", goles: " + Mathf.Round((float)GameObject.Find("GameManager").GetComponent<PartidoManager>().getPuntosP1() / chutesP1 * 100) + "%";
		if (Player2 == false) {
			miInput = KeyCode.Space;
		} else {
			miInput = KeyCode.Return;
		}

		//Este siguiente bloque de código analiza si el juego está en pausa y procede a efectuar la
		//carga de disparo y el lanzamiento de este a diferentes potencias,
		//dependiendo del tiempo de carga.

		//+++Activa y desactiva los sliders de potencia de disparo, dependiendo si la carga
		//se está efecuando o no, respectivamente.
		#region Código de disparo
		if ((conBalon)&&(GameObject.Find("HUD").GetComponent<PauseMenu>().getToken()!=true))
		{
			tiempoTotal += Time.deltaTime;
			if (Player2 == false)
			{
				tiempoP1 += Time.deltaTime;
			}
			else
			{
				tiempoP2 += Time.deltaTime;
			}
			if (Input.GetKey(miInput)){
				DisparoCargado();
            }

			if (Input.GetKeyUp(miInput))
			{
				
				if (Player2) chutesP2++; else chutesP1++;
				float alturaDisparo = Random.Range(20f, MaxfuerzaUp);
				balon.transform.parent = null;
				balon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				if (Player2==false)
                {
					if (esliderP1.GetComponent<Slider>().value <= 0.2f)
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(0f, minFuerza));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if ((esliderP1.GetComponent<Slider>().value > 0.2f) && (esliderP1.GetComponent<Slider>().value <= 0.6f))
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(220f, 600f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if ((esliderP1.GetComponent<Slider>().value > 0.6f) && (esliderP1.GetComponent<Slider>().value <= 1f))
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(600f, 900f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if (esliderP1.GetComponent<Slider>().value >= 1f)
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(100f, 300f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(200f,500f));
					}
                }
                else
                {
					if (esliderP2.GetComponent<Slider>().value <= 0.2f)
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(0f, minFuerza));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if ((esliderP2.GetComponent<Slider>().value > 0.2f) && (esliderP2.GetComponent<Slider>().value <= 0.6f))
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(220f, 600f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if ((esliderP2.GetComponent<Slider>().value > 0.6f) && (esliderP2.GetComponent<Slider>().value <= 1f))
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(600f, 900f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * alturaDisparo);
					}
					if (esliderP2.GetComponent<Slider>().value >= 1f)
					{
						balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(100f, 300f));
						balon.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(200f, 500f));
					}
				}

				if (Player2 == false) esliderP1.GetComponent<Slider>().value = 0f;
				else esliderP2.GetComponent<Slider>().value = 0f;
				conBalon = false;
				esliderP2.SetActive(false);
				esliderP1.SetActive(false);


			}

		}
        #endregion

    }

	//Método encargado principalmente para el tratamiento de errores. Se aplica cada vez 
	//que sea necesario quitar la posesión del balon a los jugadores
	public void DejarBalon()
    {
        if (conBalon)
		{
			balon.transform.parent = null;
			balon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			balon.GetComponent<Rigidbody>().AddForce(transform.forward * Random.Range(100f, 200f));
			balon.GetComponent<Rigidbody>().AddForce(transform.up * Random.Range(1f, 30f));
			conBalon = false;
		}
	}

	//Función encargada de mostrar el medidor de disparo en el canvas de los jugadores.
	private void DisparoCargado()
    {
		if (Player2 == false)
		{
			esliderP2.SetActive(false);
			esliderP1.SetActive(true);
		}
		else
		{
			esliderP1.SetActive(false);
			esliderP2.SetActive(true);
		}
		if (Player2 == false) esliderP1.GetComponent<Slider>().value += 0.004f;
		else esliderP2.GetComponent<Slider>().value += 0.004f;
	}

    //Funciones Get. Emiten el número de golpes al balón por jugador y
    //la posesión del balón registrada.
    #region Funciones Get


    public int getChutesP1()
    {
		return chutesP1;
    }
	public int getChutesP2()
	{
		return chutesP2;
	}
	public float getTiempoP2()
	{
		return tiempoP2;
	}
	public float getTiempoP1()
	{
		return tiempoP1;
	}

	public float getTiempoTotal()
	{
		return tiempoTotal;
	}

    #endregion


}
