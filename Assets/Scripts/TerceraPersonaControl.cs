using UnityEngine;
using System.Collections;

using UnityEngine.UI;

//Clase que gestiona completamente el movimiento de los personajes controlables.
public class TerceraPersonaControl : MonoBehaviour {
    
	#region Atributos
    [Header("Gameobjects dependientes")]
	public GameObject panelPlayer;
	public GameObject sliderResistenciaP1;
	public GameObject sliderResistenciaP2;

	[Header("Velocidades")]
	public float speed = 6.0F;
	public float rotateSpeed = 10.0F;
	public float gravity = 20.0F;

	[Header("Vectores")]
	public Transform ref_piv_cam;
	private Vector3 moveDirection = Vector3.zero;
	Transform thisTransform;

	[Header("Identificador de player")]
	public bool SOYPLAYERUNO = true;

	[Header("Animator")]
	public Animator anim;
	#endregion

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

				//Movimiento de los personajes relativo a cámara.
			if (SOYPLAYERUNO == true) {
				moveDirection = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")));
				
			} else {
				moveDirection = ref_piv_cam.TransformDirection (new Vector3 (Input.GetAxis("HorizontalV2"), 0, Input.GetAxis("VerticalV2")));
			}
			moveDirection *= speed;
				
		} 



			//Rotación de los personajes relativo a cámara.
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

		//Aplicación de movimiento y función de sprint
		if ((Input.GetKey(KeyCode.LeftShift)) && SOYPLAYERUNO)
        {
			sliderResistenciaP1.SetActive(true);

			if (sliderResistenciaP1.GetComponent<Slider>().value > 0.01f)
            {
				moveDirection *= 1.2f;
				sliderResistenciaP1.GetComponent<Slider>().value -= 0.004f;
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
				moveDirection *= 1.2f;
				sliderResistenciaP2.GetComponent<Slider>().value -= 0.004f;
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






