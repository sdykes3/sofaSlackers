using UnityEngine;
using System.Collections;

public class CollectableScript : MonoBehaviour {

	float moveSpeed = 6.0f;
	int collectType = 1;
	int lane = 0;

	public int effect;

	// Use this for initialization
	void Start () {
		if(gameObject.name.Contains("TypeA"))
		{

			effect = 1;
			collectType = 1;
		}
		else if(gameObject.name.Contains("TypeB"))
		{

			effect = 2;
			collectType = 2;
		}
		else if(gameObject.name.Contains("TypeC"))
		{

			effect = 3;
			collectType = 3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("ThisIsJustToHoldAScript").GetComponent<BoardScript>().ded) return;
		if(gameObject.transform.position.y < -gameObject.transform.localScale.y)
		{
			GameObject.Destroy(gameObject);
		}
		gameObject.transform.Translate((-Vector2.up) * Time.deltaTime * moveSpeed, Space.World);
	}

	public int getCollectType() {
		return collectType;
	}

	public int getLane() {
		return lane;
	}

	public void setLane(int lane) {
		this.lane = lane;
	}
}
