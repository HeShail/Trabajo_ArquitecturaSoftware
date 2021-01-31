using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoPanel : MonoBehaviour
{
	public GameObject player;
    private void Update()
    {
        transform.position = player.transform.position;
    }

}
