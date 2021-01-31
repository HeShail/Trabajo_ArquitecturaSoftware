using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public Transform cameraTransform;
	public Transform target;
	public float smoothTime = 0.3F;
	public float SmoothRotation = 1F;

	void Update() {
		


			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * smoothTime );
			transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation,  Time.deltaTime  * SmoothRotation );

	}
}
