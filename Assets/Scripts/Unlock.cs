/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlock : MonoBehaviour {

	public Transform tick;

	public PlacedObjects po;
	public int id;

	public string typeObject = null;
	public Toggle is1;
	public Toggle is2;
	public Toggle is3;
	public Toggle is4;
	public Toggle is5;

	public static int nbObjects = 3;
	public int counter = 0;
	public Text nbObjectsText;

	public Timer time;

	public Player player;
	public Text scoreText;

	// Use this for initialization
	void Start () {

		GameObject go = GameObject.Find("PlacedObjects");
		if(go == null){
			Debug.LogError("Failed to find an object named 'PlacedObjects'");
			this.enabled = false;
			return;
		}

		po = go.AddComponent<PlacedObjects>();

		GameObject go2 = GameObject.Find ("Timer");
		if(go2 == null){
			Debug.LogError("Failed to find an object named 'Timer'");
			this.enabled = false;
			return;
		}

		time = go2.GetComponent<Timer>();

		GameObject go3 = GameObject.Find ("Player");
		if(go3 == null){
			Debug.LogError("Failed to find an object named 'Player'");
			this.enabled = false;
			return;
		}

		player = go3.GetComponent<Player> ();

		DisableToggle ();
	}


	// Update is called once per frame
	void Update () {

		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 obj = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 screenPosition = Camera.main.ScreenToViewportPoint (mousePosition);
		SetTypeObject();
		if(time.TimeIsOver() == false) {
			if (counter < nbObjects) {
				if(Input.GetMouseButtonDown(0) && typeObject != null && screenPosition [0] < 0.8){
					if (CheckObject(mousePosition)) {
						counter++;
						Instantiate (tick, new Vector3(obj[0], obj[1], -1), tick.rotation);
						nbObjectsText.text = counter + "/" + nbObjects;
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
		if (is1.isOn) {
			typeObject = "Ele1";
		} else if (is2.isOn) {
			typeObject = "Ele2";
		} else if (is3.isOn) {
			typeObject = "Ele3";
		} else if (is4.isOn) {
			typeObject = "Ele4";
		} else if (is5.isOn) {
			typeObject = "Ele5";
		} else {
			typeObject = null;
		}
	}

	private void DisableToggle(){
		//Disable toggle of object that are not hide in the scene.
		if (listObjects.Contains ("Ele1") == false) {
			is1.interactable = false;
		} 
		if (listObjects.Contains ("Ele2") == false) {
			is2.interactable = false;
		} 
		if (listObjects.Contains ("Ele3") == false) {
			is3.interactable = false;
		} 
		if (listObjects.Contains ("Ele4") == false) {
			is4.interactable = false;
		} 
		if (listObjects.Contains ("Ele5") == false) {
			is5.interactable = false;
		} 
	}

	private bool CheckObject(Vector2 mousePosition){
		//Check if the player clicked on an object.
		int ind = listObjects.IndexOf(typeObject);
		//Debug.Log (((mousePosition [0] - listPositions[ind][0]) * (mousePosition [0] - listPositions[ind][0]) + (mousePosition [1] - listPositions[ind][1]) * (mousePosition [1] -listPositions[ind][1])));
		int score = player.CalculScore (mousePosition, listPositions[ind]);
		if (score > 0.02 * 500) {
			player.ChangeScore (score);
			scoreText.text = "Score : " + player.GetScore ();
			return true;
		} else {
			return false;
		}
	}

}
*/