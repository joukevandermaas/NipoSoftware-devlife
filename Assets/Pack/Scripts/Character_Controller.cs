using UnityEngine;
using System.Collections;

public class Character_Controller : MonoBehaviour {
	private GameObject head;
	private GameObject body_0;
	private GameObject arm_0;
	private GameObject shield_1;
	private GameObject shield_2;
	private GameObject sword;
	//--Animators
	private Animator head_anim;
	private Animator body_0_anim;
	private Animator arm_0_anim;
	private Animator shield_1_anim;
	private Animator shield_2_anim;
	private Animator sword_anim;
	//--Renderers
	private SpriteRenderer head_rend;
	private SpriteRenderer body_0_rend;
	private SpriteRenderer arm_0_rend;
	private SpriteRenderer shield_1_rend;
	private SpriteRenderer shield_2_rend;
	private SpriteRenderer sword_rend;
	//--aux
	private bool faceright;
	private float maxspeed;
	private bool shield_mode=false;
	private bool twinkled=false;

	void Start () {
		maxspeed=2f;//Set walk speed
		faceright=true;
		get_ChildParts();//Get head, arm, body, shield etc...
		get_ChildAnimators();
		get_ChildRenderers();
		set_ChildRender();
		set_ChildBool("walk",false);
		set_ChildBool("jump",false);
		set_ChildBool("attack",false);
		set_ChildBool("shield_mode",false);
		set_ChildBool("dead",false);
	}

	void set_ChildRender(){
		shield_2_rend.enabled=false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		set_ChildBool("jump",false);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey ("k")){//###########Change the dead event, for example: life bar=0
			set_ChildBool ("dead", true);
			DisableChild();
			Invoke("On_Destroy",2f);
		}
		if(head_anim.GetBool("dead")==false){
			if (head_anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {set_ChildBool("jump",false);}
			set_ChildBool ("attack", false);
			if (Input.GetMouseButtonDown(0)){PlayAttack();}
			if (Input.GetMouseButtonDown(1)){PlayShieldMode(true);}
			if (Input.GetMouseButtonUp(1)){PlayShieldMode(false);}
			if(shield_mode==false){
				if (Input.GetButtonDown("Jump")){PlayJump ();}
				PlayMove ();
			}
		}else{
			if(twinkled==false){
				twinkled=true;
				Invoke("Twinkle_",0.1f);
			}
		}
	}

	void PlayShieldMode(bool aux_){
		shield_mode=aux_;
		set_ChildBool ("shield_mode", aux_);
		set_ChildBool ("walk", false);
	}

	void PlayAttack(){set_ChildBool ("attack", true);}

	void PlayJump(){
		if(head_anim.GetBool("jump")==false){//only once time each jump
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,200));
			set_ChildBool("jump",true);
		}
	}

	void PlayMove(){
		float move = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxspeed, GetComponent<Rigidbody2D>().velocity.y);
		if(move>0){//Go right
			set_ChildBool ("walk", true);
			if(faceright==false){Flip ();}
		}
		if(move==0){set_ChildBool ("walk", false);}//Stop
		if((move<0)){//Go left
			set_ChildBool ("walk", true);
			if(faceright==true){Flip ();}
		}
	}

	void get_ChildParts(){
		head = getChild("Head");
		body_0 = getChild("Body_0");
		arm_0 = getChild("Arm_0");
		shield_1 = getChild("Shield_1");
		shield_2 = getChild("Shield_2");
		sword = getChild("Sword");
	}

	void get_ChildAnimators(){
		head_anim = head.GetComponent<Animator> ();
		body_0_anim = body_0.GetComponent<Animator> ();
		arm_0_anim = arm_0.GetComponent<Animator> ();
		shield_1_anim = shield_1.GetComponent<Animator> ();
		shield_2_anim = shield_2.GetComponent<Animator> ();
		sword_anim = sword.GetComponent<Animator> ();
	}

	void get_ChildRenderers(){
		head_rend = head.GetComponent<SpriteRenderer> ();
		body_0_rend = body_0.GetComponent<SpriteRenderer> ();
		arm_0_rend = arm_0.GetComponent<SpriteRenderer> ();
		shield_1_rend = shield_1.GetComponent<SpriteRenderer> ();
		shield_2_rend = shield_2.GetComponent<SpriteRenderer> ();
		sword_rend = sword.GetComponent<SpriteRenderer> ();
	}

	void DisableChild(){//Use it when dying...
		body_0_rend.enabled=false;
		arm_0_rend.enabled=false;
		shield_1_rend.enabled=false;
		shield_2_rend.enabled=false;
		sword_rend.enabled=false;
	}

	GameObject getChild(string withName) {
		Transform[] ts = this.gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts){ if (t.gameObject.name == withName)return t.gameObject;}
		return null;
	}

	void set_ChildBool(string name_,bool value_){
		head_anim.SetBool (name_, value_);
		body_0_anim.SetBool (name_, value_);
		arm_0_anim.SetBool (name_, value_);
		shield_1_anim.SetBool (name_, value_);
		shield_2_anim.SetBool (name_, value_);
		sword_anim.SetBool (name_, value_);
	}

	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Twinkle_(){
		head_rend.enabled=!head_rend.enabled;
		twinkled=false;
	}

	void On_Destroy(){
		Destroy (this.gameObject);
	}
}
