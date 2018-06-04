using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour {

	public Text scoreText;
	public Text levelText;
	public Text idText;
	public Text nameText;

	int score;

	int scoreRequired; //Score requis pour avoir une médaille.

	int countMedals;
	int countPlayerMedals;

	private GameObject player;
	private Player p;

	private DataController data;

	private Medal[] medalList;
	private List<Medal> medalListAcquired; //Really acquired medals by the player

	private Medal medal;

	public GameObject medalObject;
	//public Canvas Canvas;

	GameObject m; //Médaille à instancier

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		p = player.GetComponent<Player> (); //Récupération du script pour avoir les méthodes.
		score = p.GetScore () + 1500;

		data = new DataController ();

		nameText.text = "Name : " + p.GetUsername ();
		scoreText.text = "Score : " + score;
		levelText.text = "Niveau : " + p.GetLevel ();
		idText.text = "ID : " + p.GetID ();

		medalListAcquired = new List<Medal> ();

		/*StartCoroutine (data.RequestGetIDMedals ());
		medalList = data.GetListMedals ();
		*/
		CheckForMedals (score);
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator ICantStandItAnymore(int i){
		yield return StartCoroutine (data.RequestGetMedal (i));
	}

	/// <summary>
	/// Checks if the player as enough points to have medals.
	/// </summary>
	/// <param name="score">Player Score.</param>
	void CheckForMedals(int score){

		countMedals = 3;

		//countMedals = data.GetCountMedals ();


		for (int i = 1; i < countMedals + 1; i++) {
			StartCoroutine (ICantStandItAnymore(i));
			medal = data.GetMedal ();

			Debug.Log (medal.GetName());
			scoreRequired = medal.obtentionMedal;
			if (scoreRequired <= score) { //Si le score requis est inférieur au score du joueur.
				medalListAcquired.Add (medal);
			}
		}

		foreach (Medal medal in medalList) {
			Debug.Log (medal.nameMedal);
		}

	}

}
