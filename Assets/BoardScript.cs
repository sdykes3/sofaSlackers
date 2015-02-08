using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {

	int[,] board;
	public GameObject[,] boardLocations;
	public GameObject[,] sprites;
	public GameObject emptyLocPrefab;
	public GameObject typA;
	public GameObject typB;
	public GameObject typC;
	
	public bool ded;
	ArrayList result;
	
	GameObject scoretxt;
	float score;
	
	AudioSource dedSound;
	AudioSource fullSound;

	void Start () {
		score = 0.0f;
		scoretxt = GameObject.Find("score");
		
		ded = false;//ain't ded yet!
		dedSound = gameObject.GetComponents<AudioSource>()[0];
		fullSound = gameObject.GetComponents<AudioSource>()[1];
		
		board = new int[6, 4];
		for(int i = 0; i < 6; i++) {
			for(int j = 0; j < 4; j++) {
				board[i,j] = 0;
			}
		}
		boardLocations = new GameObject[6, 4];
		for(int i = 0; i < 6; i++) {
			for(int j = 0; j < 4; j++) {
				boardLocations[i,j] = Instantiate (emptyLocPrefab, new Vector3(3.9f+(1.55f*i),12.3f+(-1.5f*j),0), Quaternion.identity) as GameObject;
			}
		}
		
		sprites = new GameObject[6,4];
	}
	
	// Update is called once per frame
	void Update () {
		if(ded) return;
		score += 0.1f;
		scoretxt.GetComponent<UnityEngine.UI.Text>().text = "" + (int)score;
	}

	public void addToBoard(int type, int lane) {
		//print ("Add type " + type + " to lane " + lane);
		GameObject typeToInstantiate = typA;
		if(type == 1) {
			typeToInstantiate = typA;
		} else if (type == 2) {
			typeToInstantiate = typB;
		} else if (type == 3) {
			typeToInstantiate = typC;
		}

		if(board[lane,3] == 0) {
			board[lane,3] = type;
			sprites[lane,3] = Instantiate (typeToInstantiate, boardLocations[lane,3].transform.position, Quaternion.identity) as GameObject;
			result = check (lane, 3, 0, 0, 1, type);
		} else if (board[lane,2] == 0) {
			board[lane,2] = type;
			sprites[lane,2] = Instantiate (typeToInstantiate, boardLocations[lane,2].transform.position, Quaternion.identity) as GameObject;
			result = check (lane, 2, 0, 0, 1, type);
		} else if (board[lane,1] == 0) {
			board[lane,1] = type;
			sprites[lane,1] = Instantiate (typeToInstantiate, boardLocations[lane,1].transform.position, Quaternion.identity) as GameObject;
			result = check (lane, 1, 0, 0, 1, type);
		} else if (board[lane,0] == 0) {
			board[lane,0] = type;
			sprites[lane,0] = Instantiate (typeToInstantiate, boardLocations[lane,0].transform.position, Quaternion.identity) as GameObject;
			result = check (lane, 0, 0, 0, 1, type);
		} else {
			Destroy (sprites[lane, 3]);
			for (int i = 3; i > 0; i--) {
				board[lane, i] = board[lane, i-1];
				sprites[lane, i] = sprites[lane, i-1];
				sprites[lane, i].transform.position = boardLocations[lane, i].transform.position;
			}
			board[lane,0] = type;
			sprites[lane,0] = Instantiate (typeToInstantiate, boardLocations[lane,0].transform.position, Quaternion.identity) as GameObject;
			for (int i = 0; i < 4; i++) {
				result = check( lane, i, 0, 0, 1, board[lane,i]);
				if (result.Count >= 3) break;
			}
		}
		
		if (result.Count >= 3) {
			die();
		}
		else {
			if(board[0,0] != 0 && board[1,0] != 0 && board[2,0] != 0 &&
			   board[3,0] != 0 && board[4,0] != 0 && board[5,0] != 0)
			{
				clearBoard();
			}
		}
		//dont need to do if col is full, but currently is anyway
		//check (lane, 2, 0, 0, 1, type);
	}
	
	void clearBoard()
	{
		score += 9999.0f;
		fullSound.Play();
		for(int i = 0; i < 6; i++) {
			for(int j = 0; j < 4; j++) {
				board[i,j] = 0;
				Destroy (sprites[i,j]);
			}
		}
	}
	
	void die() {
	
		ded = true;
		GameObject.Find("Main Camera").audio.Stop();
		dedSound.Play();
		InvokeRepeating("blink", 0.0f, 0.5f);
		Invoke("toDedScreen", 3.0f);
	}
	
	void blink()
	{
		foreach (Vector2 v in result)
		{
			sprites[(int)v.x, (int)v.y].SetActive(!sprites[(int)v.x, (int)v.y].activeSelf);
		}
	}
	
	void toDedScreen()
	{
		CancelInvoke();
		Vector2 v = (Vector2)result[0];
		int type = board[(int)v.x, (int)v.y];
		if(type == 1) {
			Application.LoadLevel("GameOverFood");
		} else if (type == 2) {
			Application.LoadLevel("GameOverGames");
		} else if (type == 3) {
			Application.LoadLevel("GameOverBowling");
		}
	}

	//returns empty list if not found
	//returns list of vector2s denoting where the lined-up entries are if they exist
	ArrayList check(int lane, int row, int modL, int modR, int depth, int type) {

		ArrayList line = new ArrayList();
		line.Add (new Vector2(lane,row));


		//check down
		int streak = 1;
		int i = 1;
		ArrayList candidates = new ArrayList ();
		while(row + i < 4 && board[lane,row+i] == type) {
			streak++;
			candidates.Add (new Vector2(lane, row+i));
			i++;
		}
		if (streak >= 3) {
			foreach( Vector2 point in candidates) {
				line.Add (point);
			}
			return line;
		}
		//check NW/SE
		streak = 1;
		i = 1;
		candidates = new ArrayList();
		while (row - i >= 0 && lane - i >= 0 && board[lane-i,row-i] == type) {
			streak++;
			candidates.Add (new Vector2(lane-i, row-i));
			i++;
		}
		i=1;
		while (row + i < 4 && lane + i < 6 && board[lane+i,row+i] == type) {
			streak++;
			candidates.Add (new Vector2(lane+i, row+i));
			i++;
		}
		if (streak >= 3) {
			foreach( Vector2 point in candidates) {
				line.Add (point);
			}
			return line;
		}
		//check SW/NE
		streak = 1;
		i = 1;
		candidates = new ArrayList();
		while (row + i < 4 && lane - i >= 0 && board[lane - i, row + i] == type) {
			streak++;
			candidates.Add (new Vector2(lane -i, row +i));
			i++;	
		}
		i=1;
		while (row - i >= 0 && lane + i < 6 && board[lane+i, row -i] == type) {
			streak++;
			candidates.Add (new Vector2(lane +i, row -i));
			i++;
		}
		if (streak >= 3) {
			foreach( Vector2 point in candidates) {
				line.Add (point);
			}
			return line;
		}
		//check E/W
		streak = 1;
		i = 1;
		candidates = new ArrayList();
		while (lane + i < 6 && board[lane+i, row] == type) {
			streak++;
			candidates.Add (new Vector2(lane+i, row));
			i++;
		}
		i=1;
		while (lane - i >= 0 && board[lane-i, row] == type) {
			streak++;
			candidates.Add (new Vector2(lane-i, row));
			i++;
		}
		if (streak >= 3) {
			foreach( Vector2 point in candidates) {
				line.Add (point);
			}
			return line;
		}
		
		return new ArrayList();

	}
}
