using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingObjects : MonoBehaviour {

	public Transform mark;
	public string typeObject = null;


    public KeyCode clic;
   
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
    
	public PlacedObjects po;

    // Use this for initialization
    void Start () {
		nbObjectsText.text = "0/" + nbObjects;

		GameObject go = GameObject.Find("PlacedObjects");
		if(go == null){
			Debug.LogError("Failed to find an object named 'PlacedObjects'");
			this.enabled = false;
			return;
		}

		po = go.GetComponent<PlacedObjects>();

	}

	// Update is called once per frame
	void Update () {

		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 obj = Camera.main.ScreenToWorldPoint (mousePosition);
		SetTypeObject ();
		if (Input.GetKey (clic) && obj[0] < 4.78 && typeObject != null && counter < nbObjects) { 	//Check if the clic is on the GamePanel. Check if one specific object is active.
			if (po.IsIn (mousePosition) == false) { 	//To solve the multiple input for one click issue.
				po.AddPosition (mousePosition);
				po.AddObject(typeObject);
				counter++;
				Instantiate (mark, new Vector3(obj[0], obj[1], -1), mark.rotation);
				nbObjectsText.text = counter + "/" + nbObjects;
			}
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
}
