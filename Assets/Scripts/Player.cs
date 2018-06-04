using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	protected int score = 0;
	protected int level = 1;
	protected int flat = 0;
	protected int id;
	public string username;

	private DataController data;

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

	public int GetScore(){
		return score;
	}

	public int GetLevel(){
		return level;
	}

	public string GetUsername(){
		return username;
	}
	public int GetID(){
		return id;
	}

	public void SetUsername(string username){
		this.username = username;
	}

	public void SetID(int id){
		this.id = id;
	}

	public void SetScore(int score){
		this.score = score;
	}

	public void SetLevel(int level){
		this.level = level;
	}

}
