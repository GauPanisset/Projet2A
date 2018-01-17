using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	protected int score = 0;
	protected int level = 1;
	protected List<string> evolution; //Not used

	static Player instance;

	// Use this for initialization
	void Start () {
		if (instance != null) {
			Destroy (this.gameObject);
			return; 
		}

		instance = this;

		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int CalculScore(Vector2 click, Vector2 position){
		int para = 1000;
		int scoreMax = 500;
		float sqrDist = (click [0] - position [0]) * (click [0] - position [0]) + (click [1] - position [1]) * (click [1] - position [1]);
		return (int)(Mathf.Exp (-Mathf.Pow (sqrDist / para, 2)) * scoreMax);
	}

	public void ChangeScore(int s){
		score += s;
	}

	public void LevelUp(){
		level++;
	}

	public int GetScore(){
		return score;
	}

	public int GetLevel(){
		return level;
	}

}
