using UnityEngine;
using System.Collections;

//Script que sujeta el comportamiento del balón, atado a su rigidbody.
public class TRIGGERPELOTA : MonoBehaviour {

	[Header("Scripts invocados")]
	public CHUTAR chutarscript;

	//Función encargada de efectuar la recogida del balón,
	//emparentando este al personaje jugador.
	void OnTriggerEnter(Collider other)
	{
		if ((other.transform.tag == "Pelota")&& (PartidoManager.manager.relocado()!= true)) {
			

			Rigidbody PelotaRB = other.GetComponent<Rigidbody> ();
			PelotaRB.constraints = RigidbodyConstraints.FreezePosition;

			other.transform.parent = transform;
			other.transform.localPosition = new Vector3 (0, 0, 0);
            chutarscript.conBalon = true;
		}

	}

	//Método que desactiva el atributo de posesión del balón al jugador.
	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Pelota") {

			chutarscript.conBalon = false;


		}

	}

}
