using UnityEngine;
using System.Collections;

public class Bullet_Controller : MonoBehaviour {
	private float speed= 10f;
	private bool faceright = true;
	// Use this for initialization
	void Start () {
	
	}
	/*void OnCollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag=="MINIME"){
			int aux = Random.Range(0, 3);
			switch (aux){
			case 0:
				GetComponent<Animator>().SetBool("blood",true);
				break;
			case 1:
				GetComponent<Animator>().SetBool("blood2",true);
				break;
			case 2:
				GetComponent<Animator>().SetBool("blood3",true);
				break;
			}
			speed=0;
			GetComponent<BoxCollider2D>().enabled=false;
			Debug.Log("Blood");
		}

		//Destroy(this.gameObject);
	}*/

	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag=="MINIME"){
			if(this.gameObject.name!="AcidBullet"){
				int aux = Random.Range(0, 3);
				switch (aux){
				case 0:
					GetComponent<Animator>().SetBool("blood",true);
					break;
				case 1:
					GetComponent<Animator>().SetBool("blood2",true);
					break;
				case 2:
					GetComponent<Animator>().SetBool("blood3",true);
					break;
				}
			}else{
				Destroy(this.gameObject);
			}
			speed=0;
			GetComponent<BoxCollider2D>().enabled=false;
			//Debug.Log("Blood");
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * speed * Time.deltaTime);
		if(this.gameObject.name!="AcidBullet"){
			if (GetComponent<Animator>().GetCurrentAnimatorStateInfo (0).IsName ("Empty")) {
				Destroy(this.gameObject);
			}
		}
	}

	public void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
