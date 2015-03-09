using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	public float elapsedTimer = 3;
	public float elapsedTime = -3;
	bool tomber = false;
	public float lavelocite = -5;

	public GameObject chicken;

	public Vector3 decalKnife = new Vector3(6.44f,8.14f,86.64f);
	Vector3 pos = new Vector3();

	float tempX = 0;

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= elapsedTimer && !tomber)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0,lavelocite,0);
			tempX = chicken.transform.position.x+decalKnife.x;
			tomber = true;
			GetComponent<Rigidbody>().useGravity = true;
		}
		if(tomber)
		{
			pos = new Vector3(tempX,transform.position.y,chicken.transform.position.z+decalKnife.z);
			transform.position = pos;
		}
		else
		{
			pos = chicken.transform.position+decalKnife;
				//new Vector3(tempX,transform.position.y,transform.position.z);
			transform.position = pos;
		}
		if(elapsedTime >= elapsedTimer*2)
		{
			reset();

		}


	}

	public void reset()
	{
		tomber = false;
		GetComponent<Rigidbody>().useGravity = false;
		elapsedTime = 0;
		transform.Translate(0,10,0);
	}
}
