using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class TerceraPersonaControl : MonoBehaviour {
	Transform thisTransform ;
	public float speed = 6.0F;
	public float rotateSpeed = 10.0F;
	public float gravity = 20.0F;
	public GameObject panelPlayer;
	public Transform ref_piv_cam;
	public GameObject sliderResistenciaP1;
	public GameObject sliderResistenciaP2;

	private Vector3 moveDirection = Vector3.zero;

	public bool SOYPLAYERUNO = true;
		

	public Animator anim;
	//collider ataques


	void Start(){
		thisTransform = GetComponent<Transform>();
		sliderResistenciaP1.GetComponent<Slider>().value = 1f;
		sliderResistenciaP2.GetComponent<Slider>().value = 1f;
	}

	void Update(){

		if (sliderResistenciaP1.GetComponent<Slider>().value >= 1f) sliderResistenciaP1.SetActive(false);
		if (sliderResistenciaP2.GetComponent<Slider>().value >= 1f) sliderResistenciaP2.SetActive(false);
		CharacterController controller = GetComponent<CharacterController> ();
		if (controller.isGrounded) {

				//Relativo a Camara
			if (SOYPLAYERUNO == true) {
				moveDirection = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")));
				
			} else {
				moveDirection = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis("HorizontalV2"), 0, Input.GetAxis("VerticalV2")));
			}
			moveDirection *= speed;
				
		} 



			//ROTACION
		if (SOYPLAYERUNO == true) {
			Vector3 horizontalVelocity = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")));
		

			float magnitud = horizontalVelocity.magnitude;
			horizontalVelocity.y = 0;
			if (horizontalVelocity.magnitude > 0.1f) {

				thisTransform.forward = horizontalVelocity.normalized;

			}
			anim.SetFloat ("magnitud", magnitud); 

		} else {
			Vector3 horizontalVelocity = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis("HorizontalV2"), 0, Input.GetAxis("VerticalV2")));


			float magnitud = horizontalVelocity.magnitude;
			horizontalVelocity.y = 0;
			if (horizontalVelocity.magnitude > 0.1f) {

				thisTransform.forward = horizontalVelocity.normalized;

			}

			anim.SetFloat ("magnitud", magnitud); 

		}
		//APLICA MOVIMIENTO

		if ((Input.GetKey(KeyCode.LeftShift)) && SOYPLAYERUNO)
        {
			sliderResistenciaP1.SetActive(true);

			if (sliderResistenciaP1.GetComponent<Slider>().value > 0.01f)
            {
				moveDirection *= 1.6f;
				sliderResistenciaP1.GetComponent<Slider>().value -= 0.01f;
			}

        }
        else
        {
			sliderResistenciaP1.GetComponent<Slider>().value += 0.002f;
		}

		if ((Input.GetKey(KeyCode.RightShift)) && !SOYPLAYERUNO)
		{
			sliderResistenciaP2.SetActive(true);
			if (sliderResistenciaP2.GetComponent<Slider>().value > 0.01f)
            {
				moveDirection *= 1.6f;
				sliderResistenciaP2.GetComponent<Slider>().value -= 0.01f;
			}
		}
		else
		{
			sliderResistenciaP2.GetComponent<Slider>().value += 0.002f;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);


		


		}

		
	}






