using UnityEngine;
using System.Collections;

public class CloneMaker_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Play(){
		GetComponent<Animator>().SetBool("play",true);
	}

	public void Stop(){
		GetComponent<Animator>().SetBool("play",false);
	}
}
