//
//	*This script is used to select the death mode, select character to clone and instantiate it.
//


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main_Controller : MonoBehaviour {
	public bool clone = false;			//true if exists
	public bool canKill = false;
	private GameObject cloneMaker;
	private GameObject transporter;
	private GameObject selectGo;
	int dna = 0;
	int index = 0;
	GameObject DNAgo;
	Text Element;
	// Use this for initialization
	void Start () {
		cloneMaker = GameObject.Find("CloneMaker");
		transporter = GameObject.Find("Transporter");
		selectGo = GameObject.Find("Select");
		DNAgo = GameObject.Find("DNA");
		Element = GameObject.Find("Element").GetComponent<Text>();
		CreateMenuSelect();
	}

	void CreateMenuSelect(){	//Create the characters to select for clone.
		GameObject[] gos = new GameObject[101];
		Vector3 aux =selectGo.transform.position;
		for(int i = 0; i<101;i++){
			gos[i] = Instantiate(Resources.Load("0"), aux, Quaternion.identity)as GameObject;
			gos[i].name=""+i;
			gos[i].tag="GameController";
			gos[i].transform.parent = DNAgo.transform;
			aux = new Vector3(aux.x + 2, aux.y, aux.z);
		}

	}
	
	// Update is called once per frame
	void Update () {
		Element.text =""+index;
		//Move the select option to right or left with d-a key
		if(Input.GetKeyUp("d")&&index<100){
			index++;
			DNAgo.transform.position = new Vector3(DNAgo.transform.position.x-2f,DNAgo.transform.position.y,DNAgo.transform.position.z);
		}
		if(Input.GetKeyUp("a")&&index>0){
			index--;
			DNAgo.transform.position = new Vector3(DNAgo.transform.position.x+2f,DNAgo.transform.position.y,DNAgo.transform.position.z);
		}

		if(Input.GetKeyDown(KeyCode.Return)&&clone==false){
			clone=true;
			cloneMaker.SendMessage("Play");
			Invoke("InstanceClone",4);
		}

		//##### Death Modes #####//
		//1 shot, 2 blade, 3 axe, 4 weight, 5 airpump, 6 laser, 7 electro, 8 acid, 9 fire //

		if(clone==true&&canKill==true){
			if(Input.GetKeyDown("1")){
				canKill=false;
				GameObject.Find("Gun").SendMessage("shot");
			}
			if(Input.GetKeyDown("2")){
				canKill=false;
				GameObject.Find("Blade").GetComponent<Animator>().Rebind();
				GameObject.Find("Blade").GetComponent<Animator>().enabled=true;
				GameObject.Find("Blade").GetComponent<CircleCollider2D>().enabled=true;
				Invoke("DisableBlade",5);
			}
			if(Input.GetKeyDown("3")){
				canKill=false;
				GameObject.Find("Axe").GetComponent<Animator>().Rebind();
				GameObject.Find("Axe").GetComponent<Animator>().enabled=true;
				GameObject.Find("Axe").GetComponent<CircleCollider2D>().enabled=true;
				Invoke("DisableAxe",3);
			}
			if(Input.GetKeyDown("4")){
				canKill=false;
				GameObject.Find("Weight").GetComponent<Animator>().Rebind();
				GameObject.Find("Weight").GetComponent<Animator>().enabled=true;
				GameObject.Find("Weight").GetComponent<BoxCollider2D>().enabled=true;
				Invoke("DisableWeight",10);
			}
			if(Input.GetKeyDown("5")){
				canKill=false;
				GameObject.Find("AirPump").GetComponent<Animator>().Rebind();
				GameObject.Find("AirPump").GetComponent<Animator>().enabled=true;
				GameObject.Find("AirPump").GetComponent<BoxCollider2D>().enabled=true;
				Invoke("DisableAirPump",8);
			}
			if(Input.GetKeyDown("6")){
				canKill=false;
				GameObject.Find("Laser").GetComponent<Animator>().Rebind();
				GameObject.Find("Laser").GetComponent<Animator>().enabled=true;
				GameObject.Find("Laser").GetComponent<BoxCollider2D>().enabled=true;
				Invoke("DisableLaser",3);
			}
			if(Input.GetKeyDown("7")){
				canKill=false;
				GameObject.Find("Electric").GetComponent<Animator>().Rebind();
				GameObject.Find("Electric").GetComponent<Animator>().enabled=true;
			}
			if(Input.GetKeyDown("8")){
				canKill=false;
				GameObject.Find("AcidPistol").GetComponent<Animator>().Rebind();
				GameObject.Find("AcidPistol").GetComponent<Animator>().enabled=true;
				Invoke("DisableAcid",4);
			}
			if(Input.GetKeyDown("9")){
				canKill=false;
				GameObject.Find("Hot").GetComponent<Animator>().Rebind();
				GameObject.Find("Hot").GetComponent<Animator>().enabled=true;
				Invoke("DisableHot",5);
			}
		}
	}

	public void EnableKill(){	//Now you can kill the character
		canKill=true;
	}

	public void DisableClone(){		//Clone= false, now you can start to clone
		clone=false;
	}

	private void DisableElectric(){
		GameObject.Find("Electric").GetComponent<Animator>().enabled=false;
	}

	private void DisableHot(){
		GameObject.Find("Hot").GetComponent<Animator>().enabled=false;
	}

	private void DisableAcid(){
		//GameObject.Find("AcidPistol").GetComponent<Animator>().enabled=false;
		GameObject.Find("AcidPistol").GetComponent<Animator>().SetBool("shot",true);
		GameObject.Find("AcidPistol").SendMessage("createBullet");
	}

	private void DisableLaser(){
		GameObject.Find("Laser").GetComponent<Animator>().enabled=false;
		GameObject.Find("Laser").GetComponent<BoxCollider2D>().enabled=false;
	}

	private void DisableBlade(){
		GameObject.Find("Blade").GetComponent<Animator>().enabled=false;
		GameObject.Find("Blade").GetComponent<CircleCollider2D>().enabled=false;
	}

	private void DisableAxe(){
		GameObject.Find("Axe").GetComponent<Animator>().enabled=false;
		GameObject.Find("Axe").GetComponent<CircleCollider2D>().enabled=false;
	}

	private void DisableWeight(){
		GameObject.Find("Weight").GetComponent<Animator>().enabled=false;
		GameObject.Find("Weight").GetComponent<BoxCollider2D>().enabled=false;
	}

	private void DisableAirPump(){
		GameObject.Find("AirPump").GetComponent<Animator>().enabled=false;
		GameObject.Find("AirPump").GetComponent<BoxCollider2D>().enabled=false;
		DisableClone();
		canKill=false;
	}

	void InstanceClone(){		//--> Instantiate Clone based on selected DNA Character
		cloneMaker.SendMessage("Stop");
		transporter.GetComponent<Animator>().enabled=true;
		GameObject go = Instantiate(Resources.Load("0"), GameObject.Find("InstancePoint").transform.position, Quaternion.identity)as GameObject;
		go.name=""+index;	
		go.tag="MINIME";
	}
}
