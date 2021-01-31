using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMapaBalon : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        transform.position = player.transform.position;
    }
}
