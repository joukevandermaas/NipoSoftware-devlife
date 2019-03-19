using UnityEngine;
using System.Collections;

public class Gun_Controller : MonoBehaviour {
	public GameObject BulletPrefab;
	public GameObject spawner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void shot(){
		GetComponent<Animator>().enabled=true;
		GetComponent<Animator>().Rebind();
		GetComponent<Animator>().SetBool("shot",false);
		Invoke("createBullet",2);
	}

	private void createBullet(){
		if(this.gameObject.name=="AcidPistol"){
			GameObject bullet = Instantiate(BulletPrefab, spawner.transform.position, Quaternion.identity)as GameObject;
			bullet.name="AcidBullet";
			bullet.SendMessage("Flip");
		}else{
			GetComponent<Animator>().SetBool("shot",true);
			GameObject bullet = Instantiate(BulletPrefab, spawner.transform.position, Quaternion.identity)as GameObject;
			bullet.name="bullet";
			bullet.SendMessage("Flip");
			Invoke("disableAnimator",0.1f);
		}
	}

	private void disableAnimator(){
		GetComponent<Animator>().SetBool("shot",false);
		//GetComponent<Animator>().enabled=false;
	}
}
