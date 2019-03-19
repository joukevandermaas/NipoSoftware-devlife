using UnityEngine;
using System.Collections;

public class MINIME_Example : MonoBehaviour {
	public float speed= 0.3f;
	public int direction = 1;	//true right, false left
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("MINIME"), LayerMask.NameToLayer("MINIME"), true);
		//random speed
		if(Random.value<0.5f){Flip();}
		//Invoke("EnableAnimator",Random.Range(0f,2f));
	}
	void OnCollisionEnter2D(Collision2D collision) {
		EnableAnimator();
		if(collision.gameObject.name=="Limit"){
			Flip();
		}
		if(collision.gameObject.tag=="MINIME"){
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Translate(direction*Vector3.right * 0.3f * Time.deltaTime);
	}

	void EnableAnimator(){
		GetComponent<Animator>().enabled=true;
	}

	void Flip(){
		direction = direction*(-1); 
		GetComponent<SpriteRenderer>().flipX=!GetComponent<SpriteRenderer>().flipX;
	}

}
