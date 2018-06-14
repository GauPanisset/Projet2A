using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Create login manager, vérifie si le username rentré est déjà dans la BDD, 
/// communique avec celle ci pour envoyer les données dans le cas contraire.
/// Script grandement réutilisé pour le Loggin Manager.
/// </summary>
public class CreateLoginManager : MonoBehaviour {

	public Text userText;

	public InputField passwordInputField;

	string userLocal;
	string passwordLocal;
	string passwordHashLocal;

	string userDB; //DataBase username

	DataController data;

	static int scorePassword;
	static int oldScorePassword;


	// data initialisation
	void Awake() {
		data = new DataController (); //Création du controlleur de donnée, utilisé ici localement pour cette scène.
	}

	void Start () {
		userLocal = "";
		passwordLocal = "";
		oldScorePassword = 0;
		 //TODO : Ajouter l'affichage de force du password pour "very weak".
	}

	void Update() {
		passwordLocal = passwordInputField.text;
	}


	/// <summary>
	/// Checks the password in order to make the GUI.
	/// </summary>
	/// <param name="passwordLocal">Password local.</param>
	/*protected void checkPasswordGUI(string password){
		scorePassword = PasswordAdvisor.CheckStrength (password);
		if (oldScorePassword != scorePassword) { //Changer l'affichage - l'icône de force du password.
			oldScorePassword = scorePassword;
			if (scorePassword == 0) {

			} else if (scorePassword == 1) {

			}
		}
	}*/

	/// <summary>
	/// This function Checks the password viability.
	/// </summary>
	/// <returns><c>true</c>, if password viability was checked, <c>false</c> otherwise.</returns>
	/// <param name="password">Password (not the hash).</param>
	protected bool checkPasswordViability(string password){
		return PasswordAdvisor.CheckStrength (password) > 1;
	}

	/// <summary>
	/// Creating Account manager.
	/// </summary>
	public void CreatorManager(){
		userLocal = userText.text;
		passwordLocal = passwordInputField.text;

		if (userLocal.Length > 0) {
			StartCoroutine (CreateLogin (userLocal));
		}

		else{
			Debug.Log("Le champ Username est obligatoire");
		}
	}

	private IEnumerator CreateLogin(string userLocal){
		passwordLocal = passwordInputField.text;

		StartCoroutine(data.RequestGetPlayers(userLocal)); 

		yield return new WaitForSeconds (1f);

		this.userDB = data.GetUsername ();
		if (userLocal == "" | passwordLocal == "")
			Debug.LogError ("Not enough parameters");
		else if (userDB == userLocal) { //Si le user est dans la dataBase.
			Debug.Log("User already in database"); //TODO : Affichage plus propre.
		} else if(userLocal.Length > 2 && userDB != userLocal){ //On peut insérer le username et le mot de passe.
			if (checkPasswordViability (passwordLocal)) {
				passwordHashLocal = data.Md5Sum (passwordLocal);
				StartCoroutine (data.RequestPostPlayers (userLocal, 0, 0, 0, passwordHashLocal));
			}
		}
	}
}
