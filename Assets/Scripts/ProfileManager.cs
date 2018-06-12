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

	private float[] X; //Positions médailles
	private float[] Y; //Positions médailles

	private GameObject can;

	private DataController data;

	private Medal[] medalList;
	private List<Medal> medalListAcquired; //Really acquired medals by the player
	private List<GameObject> medalgolist;

	private Medal medal;

	public GameObject medalObject;
	//public Canvas Canvas;
	private Image img;

	//Sprites :
	public Sprite bronzeSprite;
	public Sprite silverSprite;
	public Sprite goldSprite;


	GameObject m; //Médaille à instancier

	// Use this for initialization
	void Start () {

		//Récupération scripts et gameobjects
		player = GameObject.Find ("Player");
		p = player.GetComponent<Player> (); //Récupération du script pour avoir les méthodes.
		score = p.GetScore () + 1500;

		can = GameObject.Find ("Canvas");

		data = new DataController ();

		//Texts
		nameText.text = "Name : " + p.GetUsername ();
		scoreText.text = "Score : " + score;
		levelText.text = "Niveau : " + p.GetLevel ();
		idText.text = "ID : " + p.GetID ();

		medalListAcquired = new List<Medal> ();
		medalgolist = new List<GameObject> ();

		StartCoroutine (GetMedals ());

		Canvas t_Canvas = can.GetComponent<Canvas> ();
		float width = t_Canvas.pixelRect.width;
		float height = t_Canvas.pixelRect.height;

		X = new float[] {0f, 0.45f * width/2f, 0.9f * width/2f, 0f, 0.45f * width/2f, 0.9f * width/2f};
		Y = new float[] {0f, 0f, 0f, - height/2f, - height/2f, - height/2f};

		Vector3 localPos = can.transform.position + new Vector3(0, height/4f, 0);
		Vector3 offset = new Vector3 (50f, -5f, 0f);

		for (int i = 0; i < X.Length; i++) {
			Vector3 position = new Vector3 (X[i], Y[i], 0);
			GameObject medalgo = Instantiate (medalObject, position + localPos + offset, new Quaternion(0, 0, 0, 0)) as GameObject;
			medalgo.transform.SetParent (can.transform);
			medalgolist.Add (medalgo);
		}
			
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseEnter(){
		foreach (Medal medal in medalList){
			Renderer rend = GetComponent<Renderer> ();
			Debug.Log ("lol" + medal.GetName());
		}
	}

	private IEnumerator GetMedals(){
		StartCoroutine (data.RequestGetMedals ());
		yield return new WaitForSeconds (1f);
		medalList = data.GetListMedals ();

		countMedals = medalList.Length;
		for (int i = 0; i < countMedals; i++) {
			
			medal = medalList [i];
		}

		CheckForMedals (score);

		for (int i = 0; i < countMedals; i++) {
			img = medalgolist[i].GetComponent<Image> ();

			if (i == 0) {
				img.sprite = bronzeSprite;
			} else if (i == 1) {
				img.sprite = silverSprite; 
			} else if (i == 2) {
				img.sprite = goldSprite;
			}
		}
	}

	/// <summary>
	/// Checks if the player as enough points to have medals, fills a list of these medals, and makes instantiation.
	/// </summary>
	/// <param name="score">Player Score.</param>
	List<Medal> CheckForMedals(int score){

		countMedals = 3;

		medalList = data.GetListMedals ();

		for (int i = 0; i < countMedals; i++) {
			Medal m = new Medal ();
			m = medalList [i];

			scoreRequired = medal.obtentionMedal;
			if (scoreRequired <= score) { //Si le score requis est inférieur au score du joueur.
				medalListAcquired.Add (medal);
			}
		}

		return medalListAcquired;
	}

}
