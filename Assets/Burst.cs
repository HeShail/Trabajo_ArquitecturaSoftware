using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase encargada de propulsar a los fuegos artificiales hacia el cielo.
public class Burst : MonoBehaviour
{
    public float fuerzaY;
    public float fuerzaX;
    public float fuerzaZ;

    //Emplea el rigidbody y aplica un rango de fuerza random en cada eje
    //además de establecer un desactivador por tiempo.
    private void OnEnable()
    {
        if (this.GetComponent<Rigidbody>()!= null)
        {
            float movimientoX = Random.Range(-fuerzaX, fuerzaX);
            float movimientoY = Random.Range(fuerzaY / 2f, fuerzaY);
            float movimientoZ = Random.Range(-fuerzaZ, fuerzaZ);

            Vector3 fuerza = new Vector3(movimientoX, movimientoY, movimientoZ);
            this.GetComponent<Rigidbody>().velocity = fuerza;
        }
        Invoke("Disable", 4f);
    }

    //Función que desactiva el proyectil.
    void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
