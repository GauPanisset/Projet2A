using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

	public Text timerText = null;
	private float startTime;
	private bool finish = false;
	private int maxTime = 15;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time - startTime;
		if (t % 60 >= maxTime) {
			finish = true;
			Finish ();
		}

		if (finish) {
			return;
		}

		string seconds = (maxTime - t % 60).ToString ("f2");

		timerText.text = "Time : " + seconds;
	}

	public bool TimeIsOver(){
		return finish;
	}

	public void Finish(){
		timerText.text = "Time : 0.00" ;
		timerText.color = Color.red;
	}

}
