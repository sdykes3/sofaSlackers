using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	Player otherPlayer;
	KeyCode leftKey, rightKey;
	Animator anim;
	public bool animationFinishedFlag;
	
	GameObject script;


	// Use this for initialization
	void Start () {
		script = GameObject.Find("ThisIsJustToHoldAScript");
		if(gameObject.name == "Player1")
		{
			otherPlayer = GameObject.Find("Player2").GetComponent<Player>();
			gameObject.GetComponent<Player>().setSpot(0);
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;
		}
		else
		{
			otherPlayer = GameObject.Find("Player1").GetComponent<Player>();;
			gameObject.GetComponent<Player>().setSpot(5);
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;
		}

		anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
		animationFinishedFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GameObject.Find ("ThisIsJustToHoldAScript").GetComponent<BoardScript>().ded) return;
	
		detectCollectables();

		if(Input.GetKeyDown(leftKey) && (player().getSpot() > 0) && (player().getSpot()-1 != otherPlayer.getSpot()) && animationFinishedFlag)
		{
			print ("left");
			//gameObject.transform.Translate((-Vector2.right) * spotWidth, Space.World);
			animationFinishedFlag = false;
			anim.SetTrigger("left");
			player().setSpot(player().getSpot()-1);
		}
		else if(Input.GetKeyDown(rightKey) && (player().getSpot() < 5) && (player().getSpot()+1 != otherPlayer.getSpot()) && animationFinishedFlag)
		{
			print ("right");
			//gameObject.transform.Translate((Vector2.right) * spotWidth, Space.World);
			animationFinishedFlag = false;
			anim.SetTrigger("right");
			player().setSpot(player().getSpot() + 1);
		}
		
		if(animationFinishedFlag)
		{
			Vector3 spot = new Vector3(4.07f, 2, 0);
			spot.x += (player ().getSpot() * 1.5f);
			gameObject.transform.position = spot;
			gameObject.transform.GetChild(0).localPosition = Vector3.zero;
		}

	}

	Player player() { return gameObject.GetComponent<Player> (); }

	float leftBound(GameObject obj){
		return obj.transform.position.x - obj.transform.localScale.x / 2;
	}
	float rightBound(GameObject obj){
		return obj.transform.position.x + obj.transform.localScale.x / 2;
	}
	float lowerBound(GameObject obj){
		return obj.transform.position.y - obj.transform.localScale.y / 2;
	}
	float upperBound(GameObject obj){
		return obj.transform.position.y + obj.transform.localScale.y / 2 + 1;
	}

	void detectCollectables()
	{
		foreach( GameObject collectable in GameObject.FindGameObjectsWithTag("collectable"))
		{
			if( (leftBound(collectable) <= rightBound(gameObject)) && (rightBound(collectable) >= leftBound(gameObject)) &&
			   (lowerBound(collectable) <= upperBound(gameObject)) && (upperBound(collectable) >= lowerBound(gameObject)) )
			{

				script.GetComponent<BoardScript>().addToBoard(collectable.GetComponent<CollectableScript>().getCollectType(),
				                                                                                  collectable.GetComponent<CollectableScript>().getLane());

				GameObject.Destroy(collectable);
				gameObject.audio.Play();
			}
		}
	}

}
