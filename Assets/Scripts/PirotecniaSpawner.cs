using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de los lanzadores pirotécnicos.
public class PirotecniaSpawner : MonoBehaviour
{
    float i;
    private void Start()
    {
        i = 0;
    }

    //Función encargada de extraer un objeto del pool, asignarle el tranform del gameobject
    //y liberarlo.
    private void FUEGO()
    {

            GameObject obj = GameObject.Find("FireworksPooler").GetComponent<FireworksPooler>().GetPooledObject();
            if (obj == null) return;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);

    }


    //Método que libera en una velocidad determinada la función de extracción de objectos del pool.
    //++Controla la velocidad de salida de los objetos del pool.
    public void Andanada()
    {
        if (i >= 0.2f)
        {
            FUEGO();
            i = 0;
        }
        else i += Time.deltaTime;
    }
}
