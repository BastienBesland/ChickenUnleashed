using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	Animator anim;
	public Animation animWings;

	float speed = 0;
	float accel = 15;
	float decel = 14;
	float speedMin = 0;
	float speedMax = 25;
	RaycastHit hit;
	bool useless = false;
	//public Vector3 distance = new Vector3();
	int state = 0;
	//Vector3 dir = new Vector3();
	float decreaseSpeed = 4;
	float turn = 0;
	float turnAccel = 110;
	bool boolAccel = false;
	bool boolLeft = false;
	bool boolRight = false;

	void Start () {
		anim = GetComponent<Animator>();
		//GetComponent("WingController") as Animator;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z))	
			boolAccel = true;
		
		if (Input.GetKeyUp(KeyCode.Z))	
			boolAccel = false;

		if (Input.GetKeyDown(KeyCode.Q))	
			boolLeft = true;
		
		if (Input.GetKeyUp(KeyCode.Q))	
			boolLeft = false;

		if (Input.GetKeyDown(KeyCode.D))	
			boolRight = true;
		
		if (Input.GetKeyUp(KeyCode.D))	
			boolRight = false;
		/*if(Input.GetKeyDown(KeyCode.Joystick1Button0))
			boolAccel = true;
		if(Input.GetKeyUp(KeyCode.Joystick1Button0))
			boolAccel = false;

		if(Input.GetAxis("Horizontal") >= 0.15f)
		{
			boolRight = true;
		}
		else{
			boolRight = false;
		}
		if(Input.GetAxis("Horizontal") <= -0.15f)
		{
			boolLeft = true;
		}
		else{
			boolLeft = false;
		}

		if(Input.GetKeyDown(KeyCode.Joystick1Button4))
		{
			useless = true;
		}
		if(Input.GetKeyUp(KeyCode.Joystick1Button4))
		{
			useless = false;
		}

		Debug.Log(useless);
		if(useless && Input.GetKeyDown(KeyCode.Joystick1Button5))
		{
			speed+=2;
		}*/
		//Debug.Log (Input.GetAxis("Fire1") + " | "+ Input.GetAxis("Fire2"));

		if(boolAccel){
			if(speed<=speedMax){
				speed += accel*Time.deltaTime;
			}

			if(speed>speedMax){
				speed -= decel*Time.deltaTime;
			}
			transform.localPosition+=transform.forward*speed*Time.deltaTime;
		}

		if(!boolAccel){
			if(speed>=speedMin){
				speed -= accel*Time.deltaTime*decreaseSpeed;
				if(speed<speedMin){
					speed=0;
				}
				//transform.Translate(new Vector3(0,0,speed)*Time.deltaTime);
				transform.localPosition+=transform.forward*speed*Time.deltaTime;
			}
		}

		if(boolLeft && speed > 0.2){
			turn -= Time.deltaTime*turnAccel;
			//transform.rotation= Quaternion.AngleAxis(turn, Vector3.up);
			//transform.rotation= Quaternion.Euler(0,turn,0);
		}
		
		
		
		if(boolRight && speed > 0.2){
			turn += Time.deltaTime*turnAccel;
			//transform.rotation= Quaternion.AngleAxis(turn, Vector3.up);
			//transform.rotation= Quaternion.Euler(0,turn,0);
		}


		//if(Physics.Raycast(transform.position,-Vector3.up,out hit,0.1f))
		if(Physics.Raycast(transform.position,-Vector3.up,0.1f)){
			state=1;
		}
		else{
			state=0;
		}

		transform.rotation= Quaternion.AngleAxis(turn, Vector3.up);/*
		Camera.main.transform.localPosition = transform.position+distance;
		Camera.main.transform.localRotation = transform.rotation;*/

		Debug.Log(speed);
		anim.SetFloat("ChickenSpeed",speed);
		anim.speed = speed/8;
		//Camera.main.fieldOfView=speed+50;

		//Debug.Log(animWings.GetBool("Boosted"));
	}

	void OnTriggerEnter(Collider other)
	{



		if(other.tag == "tremplin")
		{
			this.speed += other.gameObject.GetComponent<Tremplin>().getTremplinAccel();
			this.GetComponent<Rigidbody>().AddForce(Vector3.up*other.gameObject.GetComponent<Tremplin>().getTremplinForce());
		}

		if(other.tag == "turboPad")
		{
			this.speed += other.gameObject.GetComponent<TurboPad>().getTurboSpeed();
		}
		//GetComponent<Animation>().Play();

		//animWings.SetBool("Boosted",true);
	}

	void OnTriggerStay(Collider other)
	{
		/*if(other.tag == "turboPad")
		{
			this.speed = other.gameObject.GetComponent<TurboPad>().getTurboSpeed()+speedMax;
		}
*/


	}

	void OnTriggerExit(Collider other)
	{
		//animWings.SetBool("Boosted",false);
		/*if(other.tag == "turboPad")
		{
			this.speed -= other.gameObject.GetComponent<TurboPad>().getTurboSpeed()/20;
		}*/
	}
}


