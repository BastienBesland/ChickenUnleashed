using UnityEngine;
using System.Collections;

public class DeadFriends : MonoBehaviour {

	public GameObject friend1,friend2,Boss;
	public Vector3 decalFriend=new Vector3();
	Vector3 decalFriendTemp = new Vector3();

	public int nbDeadFriends = 1;
	float elapsedTime = 1;
	public float timerDeadFriends =2;

	// Use this for initialization
	void Start () {
		decalFriendTemp = decalFriend;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= timerDeadFriends)
		{
			elapsedTime = 0;
			for(int i =0; i <nbDeadFriends;i++)
			{
				if(i%2 == 0)
				{
					GameObject clone;
					decalFriend.x = decalFriendTemp.x+Random.Range (-7,7);
					clone = Instantiate(friend1,Boss.transform.position+decalFriend,friend1.transform.rotation) as GameObject;
					clone.GetComponent<Rigidbody>().velocity = new Vector3(0,-20,0);
				}
				else
				{
					GameObject clone;
					decalFriend.x = decalFriendTemp.x+Random.Range (-7,7);
					clone = Instantiate(friend2,Boss.transform.position+decalFriend,friend2.transform.rotation) as GameObject;
					clone.GetComponent<Rigidbody>().velocity = new Vector3(0,-20,0);
				}

			}

		}

	}
}
