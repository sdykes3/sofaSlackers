using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int currentSpot;

	/*Vector2 foodCorner, gamesCorner, bowlingCorner;

	Vector2 foodDir, gamesDir, bowlingDir;
	Vector2 desires;*/

	// Use this for initialization
	void Start () {
		/*foodCorner = GameObject.Find ("LeftCorner").transform.position;
		gamesCorner = GameObject.Find ("TopCorner").transform.position;
		bowlingCorner = GameObject.Find ("RightCorner").transform.position;

		foodDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * 30), Mathf.Sin(Mathf.Deg2Rad * 30));
		gamesDir = new Vector2(Mathf.Cos(Mathf.Deg2Rad * 150), Mathf.Sin(Mathf.Deg2Rad * 150));;
		bowlingDir = -Vector2.up;

		desires = GameObject.Find("DesireCubeP1").transform.position;*/
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*public float getFood(){ return Vector2.Distance(desires, foodCorner); }
	public float getGames(){ return Vector2.Distance(desires, gamesCorner); }
	public float getBowling(){ return Vector2.Distance(desires, bowlingCorner); }

	public void reduceFood(float x){ moveAwayFrom (foodDir, x); }
	public void reduceGames(float x){ moveAwayFrom (gamesDir,x); }
	public void reduceBowling(float x){ moveAwayFrom (bowlingDir, x); }*/

	/*void moveAwayFrom(Vector2 dir, float x)
	{
		Vector2.Distance (GameObject.Find ("DesireCircle").transform.position, desires);
		Vector2 nextPos = desires + (dir * x);
		if(checkCircleBounds(nextPos))
		{
			desires = nextPos;
			updatePoint();
		}
	}

	void updatePoint()
	{
		GameObject dc= GameObject.Find ("DesireCubeP1");
		if(gameObject.name == "Player1") {
			dc = GameObject.Find ("DesireCubeP1");
		} else if (gameObject.name == "Player2") {
			dc = GameObject.Find ("DesireCubeP2");
		}
		print (desires);
		dc.transform.position = new Vector3(desires.x, desires.y, -2);
	}

	bool checkCircleBounds(Vector2 pos)
	{
		GameObject bounds = GameObject.Find ("DesireCircle");
		float radius = bounds.transform.localScale.x / 2.0f;
		Vector2 center = bounds.transform.position;

		if(Vector2.Distance(center, pos) > radius)
		{
			return false;
		}

		return true;

	}*/

	public void setSpot(int whichSpot) {
		print ("Spot changing from " + currentSpot + " to " + whichSpot);
		currentSpot = whichSpot;
	}

	public int getSpot() {
		return currentSpot;
	}

}
