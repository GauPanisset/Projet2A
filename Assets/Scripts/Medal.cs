using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal {

	int idMedal;
	public int obtentionMedal;
	public int rewardMedal;
	public string nameMedal;
	public string descriptionMedal;

	public void AddValues(int idMedal, string nameMedal, string descriptionMedal, int obtentionMedal, int rewardMedal){
		this.idMedal = idMedal;
		this.nameMedal = nameMedal;
		this.descriptionMedal = descriptionMedal;
		this.obtentionMedal = obtentionMedal;
		this.rewardMedal = rewardMedal;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string GetName(){
		return nameMedal;
	}

	public int GetID(){
		return idMedal;
	}
}
