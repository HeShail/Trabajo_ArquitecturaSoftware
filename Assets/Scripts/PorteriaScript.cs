using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteriaScript : MonoBehaviour
{
    public bool porteriaP1;
    public PartidoManager gameManager;
    private float cont;
    // Start is called before the first frame update
    void Start()
    {
        cont = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        cont+= Time.deltaTime;
    }
    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Pelota")&& (cont >= 2))
        {
            cont = 0;
            if (porteriaP1) gameManager.PuntuaP2(); else gameManager.PuntuaP1();
            gameManager.GOL();
        }
    }
}
