using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo que gestiona el seguimiento del sticker balón a su referencia.
public class MovimientoMapaBalon : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        transform.position = player.transform.position;
    }
}
