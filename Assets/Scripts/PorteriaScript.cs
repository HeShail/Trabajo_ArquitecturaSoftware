using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase encargada de actualizar el marcador, 
//dependiendo de la porteria que ha recibido el gol.
//Empleo de singleton PartidoManager
public class PorteriaScript : MonoBehaviour
{
    public bool porteriaP1;
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

    //Función que detecta la entrada del balón a portería.
    //También incluye un temporizador para evitar anotar más de 
    //un punto por jugada.
    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Pelota") && (cont >= 2))
        {
            cont = 0;
            if (porteriaP1) PartidoManager.manager.PuntuaP2(); else PartidoManager.manager.PuntuaP1();
            PartidoManager.manager.GOL();
        }
    }
}
