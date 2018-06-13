using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogginManager : MonoBehaviour {

	public Text userText;

	public InputField passwordInputField;

	string userLocal;
	string passwordLocal;

	string userDB; //DataBase username
	string passwordDB; //DataBase password
	int idDB; //DataBase id
	int scoreDB; //DataBase score
	int levelDB; //DataBase level

	DataController data;

	private GameObject player;
	private Player p;

	// data initialisation
	void Awake() {
		data = new DataController ();
	}

	void Start () {
		userLocal = "";
		passwordLocal = "";

		player = GameObject.Find ("Player");
		p = player.GetComponent<Player> (); //Récupération du script pour avoir les méthodes.
	}

	void Update() {
	}

	public void CheckSceneLoad(){
		userLocal = userText.text;
		passwordLocal = passwordInputField.text;

		StartCoroutine (LoadMenu (userLocal));

	}

	private IEnumerator LoadMenu(string userLocal){
		userLocal = userText.text;
		passwordLocal = passwordInputField.text;

		StartCoroutine (data.RequestGetPlayers (userLocal)); 
		yield return new WaitForSeconds (1f);

		if (userLocal.Length > 0) {
			StartCoroutine (data.RequestGetPlayers (userLocal)); 

			this.userDB = data.GetUsername ();
			this.passwordDB = data.GetPassword ();
			this.idDB = data.GetId ();
			this.scoreDB = data.GetScore ();
			this.levelDB = data.GetLevel ();

			if (userLocal == "" | passwordLocal == "")
				Debug.LogError ("Not enough parameters");
			else if (userDB == userLocal && passwordDB == data.Md5Sum (passwordLocal)) {
				//Puts the data related to the given username in the Player gameobject
				p.SetUsername (userLocal);
				p.SetID (idDB);
				p.SetLevel (levelDB);
				p.SetScore (scoreDB);

				SceneManager.LoadScene ("BravoToCharlieMenu1.0");
			} else {
				Debug.Log ("username or password wrong");
			}
		}
	}

}
