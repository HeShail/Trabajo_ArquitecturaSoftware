using UnityEngine;
using System.Collections;

//Clase encargada de controlar la cámara grúa con un movimiento suavizado.
public class FollowCamera : MonoBehaviour {

	[Header("Posiciones")]
	public Transform cameraTransform;
	public Transform target;

	[Header("Tiempos")]
	public float smoothTime = 0.3F;
	public float SmoothRotation = 1F;

	void Update() {
		
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * smoothTime );
			transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation,  Time.deltaTime  * SmoothRotation );

	}
}
