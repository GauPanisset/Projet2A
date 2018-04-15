using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveringObjects : MonoBehaviour {

	public Transform tick;

	private PlacedObjects po;
	public int id;

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

		po = go.GetComponent<PlacedObjects> ();
		po.RandInitObjects ();

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


	}
		
	// Update is called once per frame
	void Update () {
		DisableToggle ();
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
						int ind = po.GetObjects ().IndexOf (typeObject);
						po.GetObjects().RemoveAt (ind);
						po.GetPositions ().RemoveAt (ind);
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
		if (po.isReady ()) {
			//Disable toggle of objects that are not hide in the scene.
			if (po.GetObjects ().Contains ("baignoire") == false) {
				isBaignoire.interactable = false;
			} 
			if (po.GetObjects ().Contains ("canape") == false) {
				isCanape.interactable = false;
			} 
			if (po.GetObjects ().Contains ("chaise") == false) {
				isChaise.interactable = false;
			} 
			if (po.GetObjects ().Contains ("evier") == false) {
				isEvier.interactable = false;
			} 
			if (po.GetObjects ().Contains ("tablerec") == false) {
				isTableRec.interactable = false;
			} 
			if (po.GetObjects ().Contains ("tableron") == false) {
				isTableRon.interactable = false;
			}
			if (po.GetObjects ().Contains ("toilette") == false) {
				isToilette.interactable = false;
			}
		}
	}
		
	private bool CheckObject(Vector2 mousePosition){
		//Check if the player clicked on an object.
		int ind = po.GetObjects().IndexOf(typeObject);
		Debug.Log (ind);
		//Debug.Log (((mousePosition [0] - listPositions[ind][0]) * (mousePosition [0] - listPositions[ind][0]) + (mousePosition [1] - listPositions[ind][1]) * (mousePosition [1] -listPositions[ind][1])));
		int score = player.CalculScore (mousePosition, po.GetPositions()[ind]);
		if (score > 0.02 * 500) {
			player.ChangeScore (score);
			scoreText.text = "Score : " + player.GetScore ();
			return true;
		} else {
			return false;
		}
	}

}
