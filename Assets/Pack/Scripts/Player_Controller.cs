using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	public GameObject BulletPrefab;
	public GameObject spawner;
	private float speed= 1.8f;
	//private int direction = 0;	//true right, false left
	private bool faceright = true;
	private bool grounded = false;
	float move=0;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		grounded=true;
		if(move!=0){anim.SetBool("walk",true);}
		anim.SetBool("jump",false);
	}

	void Update () {
		if(Input.GetMouseButtonUp(0)){
			GameObject bullet = Instantiate(BulletPrefab, spawner.transform.position, Quaternion.identity)as GameObject;
			bullet.name="bullet";
			if(faceright==false){
				bullet.SendMessage("Flip");
			}
		}
		move = Input.GetAxis ("Horizontal");
		if(move>0){
			if(grounded==true){
				anim.SetBool("walk",true);
			}
			if(faceright==false){
				Flip();
			}
			transform.Translate(Vector3.right * 1 * Time.deltaTime);
		}
		if(move==0){
			anim.SetBool("walk",false);
		}
		if(move<0){
			if(grounded==true){
				anim.SetBool("walk",true);
			}
			if(faceright==true){
				Flip();
			}
			transform.Translate(Vector3.right * 1 * Time.deltaTime);
		}
		if (Input.GetButtonDown("Jump")&&grounded==true){
			PlayJump ();
		}
	}

	void Flip_(){
		GetComponent<SpriteRenderer>().flipX=faceright;
		faceright=!faceright;
	}

	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void PlayJump(){
		anim.SetBool("jump",true);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,220));
		grounded=false;
	}
}
