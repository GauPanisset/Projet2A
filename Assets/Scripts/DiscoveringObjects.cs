﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveringObjects : MonoBehaviour {

	private float maxRay = 0f;

	public Transform tick;

	public KeyCode clic;

	public PlacedObjects po;
	public List<string> listObjects;
	public List<Vector2> listPositions;

	public string typeObject = null;
	public Toggle isBaignoire;
	public Toggle isCanape;
	public Toggle isChaise;
	public Toggle isEvier;
	public Toggle isTableRec;
	public Toggle isTableRon;
	public Toggle isToilette;

	public static int nbObjects = 3;
	public int counter = 0;
	public Text nbObjectsText;

	public Timer time;

	// Use this for initialization
	void Start () {
		maxRay = 1000; //Camera.main.orthographicSize * 2;
	
		GameObject go = GameObject.Find("PlacedObjects");
		if(go == null){
			Debug.LogError("Failed to find an object named 'PlacedObjects'");
			this.enabled = false;
			return;
		}

		po = go.GetComponent<PlacedObjects>();
		for (int i = 0; i < nbObjects; i++) {
			listObjects.Add (po.GetObject (i));
			listPositions.Add (po.GetPosition (i));
		}

		GameObject go2 = GameObject.Find ("Timer");
		if(go2 == null){
			Debug.LogError("Failed to find an object named 'Timer'");
			this.enabled = false;
			return;
		}

		time = go2.GetComponent<Timer>();

		DisableToggle ();
	}


	// Update is called once per frame
	void Update () {

		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 obj = Camera.main.ScreenToWorldPoint (mousePosition); //  Camera.main.ScreenToWorldPoint (Input.mousePosition)
		SetTypeObject();
		if(time.TimeIsOver() == false) {
			if (counter < nbObjects) {
				if (Input.GetKey (clic) && obj [0] < 4.78 && typeObject != null) {
					if (CheckObject(mousePosition)) {
						counter++;
						Instantiate (tick, new Vector3(obj[0], obj[1], -1), tick.rotation);
						nbObjectsText.text = counter + "/" + nbObjects;
						int ind = listObjects.IndexOf(typeObject);
						listObjects.Remove (typeObject);
						listPositions.RemoveAt (ind);
						DisableToggle ();
					}
				}
			} else {
				Debug.Log ("Victory");
			}
		} else {
			Debug.Log ("Time is over");
		}
	}

	private void SetTypeObject(){
		if (isBaignoire.isOn) {
			typeObject = "baignoire";
		} else if (isCanape.isOn) {
			typeObject = "canape";
		} else if (isChaise.isOn) {
			typeObject = "chaise";
		} else if (isEvier.isOn) {
			typeObject = "evier";
		} else if (isTableRec.isOn) {
			typeObject = "tablerec";
		} else if (isTableRon.isOn) {
			typeObject = "tableron";
		} else if (isToilette.isOn) {
			typeObject = "toilette";
		} else {
			typeObject = null;
		}
	}

	private void DisableToggle(){
		//Disable toggle of object that are not hide in the scene.
		if (listObjects.Contains ("baignoire") == false) {
			isBaignoire.interactable = false;
		} 
		if (listObjects.Contains ("canape") == false) {
			isCanape.interactable = false;
		} 
		if (listObjects.Contains ("chaise") == false) {
			isChaise.interactable = false;
		} 
		if (listObjects.Contains ("evier") == false) {
			isEvier.interactable = false;
		} 
		if (listObjects.Contains ("tablerec") == false) {
			isTableRec.interactable = false;
		} 
		if (listObjects.Contains ("tableron") == false) {
			isTableRon.interactable = false;
		}
		if (listObjects.Contains ("toilette") == false) {
			isToilette.interactable = false;
		}
	}
		
	private bool CheckObject(Vector2 mousePosition){
		//Check if the player clicked on an object.
		int ind = listObjects.IndexOf(typeObject);
		//Debug.Log (((mousePosition [0] - listPositions[ind][0]) * (mousePosition [0] - listPositions[ind][0]) + (mousePosition [1] - listPositions[ind][1]) * (mousePosition [1] -listPositions[ind][1])));
		if (((mousePosition [0] - listPositions[ind][0]) * (mousePosition [0] - listPositions[ind][0]) + (mousePosition [1] - listPositions[ind][1]) * (mousePosition [1] -listPositions[ind][1])) < maxRay) {
			return true;
		} else {
			return false;
		}
	}

}