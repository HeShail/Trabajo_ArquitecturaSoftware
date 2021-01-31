using UnityEngine;
using System.Collections;

public class TRIGGERPELOTA : MonoBehaviour {
	public CHUTAR chutarscript;
	private PartidoManager gameManager;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<PartidoManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		if ((other.transform.tag == "Pelota")&& (gameManager.relocado()!= true)) {
			

			Rigidbody PelotaRB = other.GetComponent<Rigidbody> ();
			PelotaRB.constraints = RigidbodyConstraints.FreezePosition;

			other.transform.parent = transform;
			other.transform.localPosition = new Vector3 (0, 0, 0);
            chutarscript.conBalon = true;
		}

	}
	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Pelota") {

			chutarscript.conBalon = false;


		}

	}

}
