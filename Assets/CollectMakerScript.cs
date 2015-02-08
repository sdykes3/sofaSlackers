using UnityEngine;
using System.Collections;

public class CollectMakerScript : MonoBehaviour {

	public GameObject typA;
	public GameObject typB;
	public GameObject typC;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("GenerateCollectables",2,1.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void GenerateCollectables() {
		if(GameObject.Find ("ThisIsJustToHoldAScript").GetComponent<BoardScript>().ded) CancelInvoke ();
		int type;
		int lane;
		GameObject thing;
		int[] alreadyHasThing = {0,0,0,0,0,0};
		for(int i = 0; i < 3; i++) {
			type = Random.Range(0,3);
			lane = Random.Range(0,6);
			while(alreadyHasThing[lane] != 0) {
				lane = Random.Range(0,6);
			}
			alreadyHasThing[lane] = 1;
			if(type == 0) {
				thing = Instantiate (typA, spawnInLane(lane), Quaternion.identity) as GameObject;
				thing.GetComponent<CollectableScript>().setLane(lane);
				thing.transform.parent = GameObject.Find("PrefabHolder").transform;
			} else if (type == 1) {
				thing = Instantiate (typB, spawnInLane(lane), Quaternion.identity) as GameObject;
				thing.GetComponent<CollectableScript>().setLane(lane);
				thing.transform.parent = GameObject.Find("PrefabHolder").transform;
			} else if (type == 2) {
				thing = Instantiate (typC, spawnInLane(lane), Quaternion.identity) as GameObject;
				thing.GetComponent<CollectableScript>().setLane(lane);
				thing.transform.parent = GameObject.Find("PrefabHolder").transform;
			}
		}
	}

	Vector3 spawnInLane(int thisLane) {
		if(thisLane == 0) {
			return new Vector3(4.0f,21,0);
		} else if (thisLane == 1) {
			return new Vector3(5.5f,21,0);
		} else if (thisLane == 2) {
			return new Vector3(7.0f,21,0);
		} else if (thisLane == 3) {
			return new Vector3(8.5f,21,0);
		} else if (thisLane == 4) {
			return new Vector3(10.0f,21,0);
		} else {
			return new Vector3(11.5f,21,0);
		}
	}
}
