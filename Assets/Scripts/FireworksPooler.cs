using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que administra el object pooling para los fuegos artificiales de gol.
public class FireworksPooler : MonoBehaviour
{
    #region Atributos
    private static FireworksPooler current;
    [Header("Prefab asignado")]
    public GameObject objectPooled;

    [Header("Objetos del pool")]
    public List<GameObject> pooledObjects;

    [Header("Lanzadores")]
    public PirotecniaSpawner lanzador1;
    public PirotecniaSpawner lanzador2;
    public PirotecniaSpawner lanzador3;
    public PirotecniaSpawner lanzador4;

    [Header("Propiedades del pool")]
    public int pooledAmount;
    public bool willGrow;

    //Controladores de secuencia de disparo.
    private bool botonRojo;
    private float cont;
    #endregion

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        cont = 0f;
        current = this;
        for(int i=0; i< pooledAmount; i++)
        {
            GameObject obj = Instantiate(objectPooled);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void Update()
    {
        if (botonRojo)
        {
            if (cont >= 0f)
            {
                lanzador1.Andanada();
                lanzador2.Andanada();
                lanzador3.Andanada();
                lanzador4.Andanada();
                cont -= Time.deltaTime;
            }

        }
        else
        {
            botonRojo = false;
        }

    }

    //Función encargada de extraer un objeto del pool. Además, contamos con una propiedad
    //que al activarse, rompe el límite de objetos capaces de instanciarse (>pooledAmount).
    public GameObject GetPooledObject()
    {
        for (int i=0; i<pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(objectPooled);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }

    //Método que ejecuta el disparo sincronizado de todos los fuegos artficiales desde todos 
    //los spawners.
    public void Descarga()
    {
        cont = 3f;
        botonRojo = true;
    }

}
