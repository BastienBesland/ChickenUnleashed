using UnityEngine;
using System.Collections;

public class Tremplin : MonoBehaviour {

	public int forceJump;
	public int forceAccel;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTremplinForce()
	{
		return forceJump;
	}

	public int getTremplinAccel()
	{
		return forceAccel;
	}
}
