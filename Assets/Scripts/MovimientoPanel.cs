using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script que se encarga del seguimiento de un canvas al personaje
//del player. Este canvas carga con los medidores de potencia de disparo 
//y resistencia y el indicador Player1 o Player2.
public class MovimientoPanel : MonoBehaviour
{
	public GameObject player;
    private void Update()
    {
        transform.position = player.transform.position;
    }

}
