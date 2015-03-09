using UnityEngine;
using System.Collections;

public class MoveChicken : MonoBehaviour {

	public GameObject Boss, Knife,DeadFriends,EcranFin;
	public int distanceBoss = 20;
	public float vitesseTourner = 1;

	Animator anim;

	float bossLife = 4;
	//Etat 0=Rien/1=Couteau/2=DeadFriends
	int state = 0;
	float elapsedTime= 0;
	float speed = 0;
	float accel = 25;
	float decel = 14;
	float speedMin = 0;
	public float speedMax = 25;
	float decreaseSpeed = 4;
	bool boolAccel = false;
	bool boolLeft = false;
	bool boolRight = false;
	bool dead = false;
	bool transition = false;
	Transform imageUI1,imageUI2;
	Vector3 tourner = new Vector3();
	
	void Start () {
		speed = speedMax;
		anim = GetComponent<Animator>();
		DeadFriends.SetActive(false);
		Knife.GetComponent<Knife>().elapsedTime =-3;
		imageUI1 = EcranFin.transform.GetChild(0);
		imageUI2 = EcranFin.transform.GetChild(1);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.LeftArrow))	
			boolLeft = true;
		
		if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.LeftArrow))	
			boolLeft = false;
		
		if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))	
			boolRight = true;
		
		if (Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.RightArrow))		
			boolRight = false;
		tourner.x=0;
		if(boolRight)
			tourner.x += vitesseTourner;
		if(boolLeft)
			tourner.x -= vitesseTourner;



		/*if(Input.GetKeyUp(KeyCode.Keypad1))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(6);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}
		if(Input.GetKeyUp(KeyCode.Keypad2))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(7);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}
		if(Input.GetKeyUp(KeyCode.Keypad3))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(8);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}
		if(Input.GetKeyUp(KeyCode.Keypad4))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(9);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}
		if(Input.GetKeyUp(KeyCode.Keypad5))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(10);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}
		if(Input.GetKeyUp(KeyCode.Keypad6))
		{
			resetCam ();
			Transform camera;
			camera = transform.GetChild(11);
			camera.gameObject.GetComponent<Camera>().enabled = true;
		}*/

		if(!boolAccel)
		{
			if(speed<=speedMax){
				speed += accel*Time.deltaTime;
			}
			
			if(speed>speedMax){
				speed -= decel*Time.deltaTime;
			}
			transform.Translate((tourner+(transform.forward*speed))*Time.deltaTime,Space.World);
			Boss.transform.Translate(new Vector3(0,0,transform.forward.z*speed*Time.deltaTime),Space.World);
		}

		
		if(dead || boolAccel){
			if(speed>=speedMin){
				speed -= accel*Time.deltaTime*decreaseSpeed;
				if(speed<speedMin){
					speed=0;
				}
				transform.Translate((tourner+(transform.forward*speed))*Time.deltaTime,Space.World);
			}
		}

		if(bossLife==3 && transition)
		{
			transition = false;
			DeadFriends.SetActive(true);
		}
		if(bossLife==2&& transition)
		{
			DeadFriends.SetActive(true);
			transition = false;
			DeadFriends.GetComponent<DeadFriends>().timerDeadFriends = 1;
		}
		if(bossLife==1&& transition)
		{
			DeadFriends.SetActive(true);
			transition = false;
			DeadFriends.GetComponent<DeadFriends>().nbDeadFriends = 2;
		}
		if(bossLife==0&& transition)
		{
			transition = false;
			speed+=50;
			Knife.SetActive(false);
			DeadFriends.SetActive(false);
			Boss.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
		}
		if(bossLife==-1&& transition)
		{
			transition = false;
			Knife.SetActive (true);
			Knife.GetComponent<Knife>().reset ();
			DeadFriends.SetActive(true);
			DeadFriends.GetComponent<DeadFriends>().nbDeadFriends = 3;
			DeadFriends.GetComponent<DeadFriends>().timerDeadFriends = 0.8f;
		}
		if(bossLife==-2&& transition)
		{
			transition = false;
			Knife.SetActive(false);
			DeadFriends.SetActive(false);
			Boss.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
			boolAccel = true;
		}
		
		
		if(boolLeft && speed > 0.2){
		}
		
		
		
		if(boolRight && speed > 0.2){
		}

		if(dead)
		{
			elapsedTime+=Time.deltaTime;
			if(elapsedTime>=3)
			{
				Application.LoadLevel(0);
			}
		}


		anim.SetFloat("ChickenSpeed",speed);
		anim.speed = speed/8;	

		if(boolAccel && speed <5)
		{
			elapsedTime+=Time.deltaTime;
			if(elapsedTime>=2)
			{
				imageUI1.gameObject.GetComponent<CanvasGroup>().alpha = 1;
			}
			if(elapsedTime>=8)
			{
				imageUI2.gameObject.GetComponent<CanvasGroup>().alpha = 1;
			}			
			if(elapsedTime>=12)
			{
				Application.LoadLevel(0);
			}
		}

	}

	/*void resetCam()
	{
		Transform camera;
		camera = transform.GetChild(6);
		camera.gameObject.GetComponent<Camera>().enabled = false;
		camera = transform.GetChild(7);
		camera.gameObject.GetComponent<Camera>().enabled = false;
		camera = transform.GetChild(8);
		camera.gameObject.GetComponent<Camera>().enabled = false;
		camera = transform.GetChild(9);
		camera.gameObject.GetComponent<Camera>().enabled = false;
		camera = transform.GetChild(10);
		camera.gameObject.GetComponent<Camera>().enabled = false;
		camera = transform.GetChild(11);
		camera.gameObject.GetComponent<Camera>().enabled = false;
	}*/

	void UDead()
	{
		dead = true;
		anim.enabled = false;
		Transform chickPart;
		chickPart = transform.GetChild(0);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();
		chickPart = transform.GetChild(1);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();
		chickPart = transform.GetChild(2);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();
		chickPart = transform.GetChild(3);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();
		chickPart = transform.GetChild(4);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();
		chickPart = transform.GetChild(5);
		chickPart.gameObject.AddComponent<Rigidbody>();
		chickPart.gameObject.AddComponent<BoxCollider>();

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "turboPad")
		{
			bossLife -=1;
			transition = true;
			//this.speed += other.gameObject.GetComponent<TurboPad>().getTurboSpeed();
			speedMax += 10;
			Boss.GetComponent<SpriteRenderer>().color = new Color(1,bossLife/5,bossLife/5,1);
			//Debug.Log(bossLife/5);
			/*Vector3 posTempo = new Vector3(Boss.transform.position.x,Boss.transform.position.y,Boss.transform.position.z-distanceBoss);
			Boss.transform.position = posTempo;*/
		}
		else{
			if(!dead)
			{
				UDead();
			}
		}
		

		
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "deadFriend")
		{
			speed -=10;
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		
	}
	
	void OnTriggerExit(Collider other)
	{
	}
}


