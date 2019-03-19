//
//	*This script is used to modify the animator of the base prefab with the animations of the selected character
//


using UnityEngine;
using System.Collections;
using System.Linq;
//using UnityEditor;

public class Custom_Animator : MonoBehaviour {
	AnimationClip clip;
	RuntimeAnimatorController myController;
	AnimatorOverrideController myOverrideController;
	private float speed = 0.5f;

	// Use this for initialization
	void Start () {
		if(this.gameObject.tag=="GameController"){
			speed=0;
			this.gameObject.transform.localScale = this.gameObject.transform.localScale/1f;
		}
		//--Setting animation clips
		AnimationClip clipWalk= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/walk")) as AnimationClip;
		AnimationClip clipStop= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/stop")) as AnimationClip;
		AnimationClip clipbowels= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/bowels")) as AnimationClip;
		AnimationClip clipcut= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/cut")) as AnimationClip;
		AnimationClip clipdance= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/dance")) as AnimationClip;
		AnimationClip clipexplosion= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/explosion")) as AnimationClip;
		AnimationClip clipflatten= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/flatten")) as AnimationClip;
		AnimationClip cliphead= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/head")) as AnimationClip;
		AnimationClip clipjump= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/jump")) as AnimationClip;
		AnimationClip clipshot= (AnimationClip)AnimationClip.Instantiate(Resources.Load("Animations/"+this.gameObject.name+"/shot")) as AnimationClip;

		clipWalk.name="customWalk";
		clipStop.name="customStop";
		clipbowels.name="custombowels";
		clipcut.name="customcut";
		clipdance.name="customdance";
		clipexplosion.name="customexplosion";
		clipflatten.name="customflatten";
		cliphead.name="customhead";
		clipjump.name="customjump";
		clipshot.name="customshot";

		//--
		myController = GetComponent<Animator>().runtimeAnimatorController;
		myOverrideController = new AnimatorOverrideController();
		myOverrideController.runtimeAnimatorController = myController;
		myOverrideController.name = "TempAnimator";
		//--Override animations / animator
		myOverrideController["walk"] = clipWalk;
		myOverrideController["stop"] = clipStop;
		myOverrideController["bowels"] = clipbowels;
		myOverrideController["cut"] = clipcut;
		myOverrideController["dance"] = clipdance;
		myOverrideController["explosion"] = clipexplosion;
		myOverrideController["flatten"] = clipflatten;
		myOverrideController["head"] = cliphead;
		myOverrideController["jump"] = clipjump;
		myOverrideController["shot"] = clipshot;

		//--
		GetComponent<Animator>().runtimeAnimatorController = myOverrideController;
		GetComponent<Animator>().enabled=true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(this.gameObject.tag!="GameController"){
			if(other.gameObject.name=="KillPoint"){
				Camera.main.SendMessage("EnableKill");
				Stop();
			}
			if(other.gameObject.name=="Trash"){
				GetComponent<Rigidbody2D>().isKinematic = false;
				GameObject.Find("Transporter").GetComponent<Animator>().enabled=false;
			}
			if(other.gameObject.name=="bullet"){Invoke("shotDying",1);}
			if(other.gameObject.name=="AcidBullet"){Invoke("acidDying",0);}
			if(other.gameObject.name=="UnderGround"){
				Camera.main.SendMessage("DisableClone");
				Destroy(this.gameObject);
			}
			if(other.gameObject.name=="Blade"){Invoke("BladeDying",0.5f);}
			if(other.gameObject.name=="Axe"){Invoke("AxeDying",1.5f);}
			if(other.gameObject.name=="Weight"){WeightDying();}
			if(other.gameObject.name=="AirPump"){
				this.gameObject.GetComponent<BoxCollider2D>().enabled=false;
				Invoke("AirPumpDying",4.5f);
			}
			if(other.gameObject.name=="Laser"){Invoke("LaserDying",2.3f);}
			if(other.gameObject.name=="Terminal"){Invoke("ElectricDying",1);}
			if(other.gameObject.name=="Hot"){Invoke("HotDying",1);}
		}else{
			if(other.gameObject.name=="Select"){this.gameObject.transform.localScale = this.gameObject.transform.localScale*1.05f;}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(this.gameObject.tag=="GameController"){
			if(other.gameObject.name=="Select"){
				this.gameObject.transform.localScale = this.gameObject.transform.localScale/1.1f;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(this.gameObject.tag=="GameController"){
			if(this.transform.position.x==0){
				if(GetComponent<Animator>().GetBool("dance")==false&&GetComponent<Animator>().GetBool("jump")==false){
					int x = Random.Range(-1,4);
					if(x>=0){
						GetComponent<Animator>().SetBool("dance",true);
					}else{
						GetComponent<Animator>().SetBool("jump",true);
					}
				}
			}else{
				GetComponent<Animator>().SetBool("dance",false);
				GetComponent<Animator>().SetBool("jump",false);
			}
		}
		transform.Translate(Vector3.right * speed* Time.deltaTime);
		/*if(Input.GetKeyDown("d")){
			Dance();
		}
		if(Input.GetKeyDown("j")){
			Jump();
		}
		if(Input.GetKeyDown("q")){
			DisableSelected();
		}*/
	}

	private void Jump(){GetComponent<Animator>().SetBool("jump",true);}
	private void Dance(){GetComponent<Animator>().SetBool("dance",true);}

	private void DisableSelected(){
		GetComponent<Animator>().SetBool("dance",false);
		GetComponent<Animator>().SetBool("jump",false);
	}

	public void Stop(){
		speed=0;
		GameObject.Find("Transporter").GetComponent<Animator>().enabled=false;
		GetComponent<Animator>().SetBool("stop",true);
	}

	private void HotDying(){
		GetComponent<Animator>().SetBool("burn",true);
		Invoke("recycle",3);
	}

	private void ElectricDying(){
		GetComponent<Animator>().SetBool("electric",true);
		Invoke("recycle",3);
	}

	private void acidDying(){
		GetComponent<Animator>().SetBool("acid",true);
		Invoke("recycle",3);
	}

	private void LaserDying(){
		GetComponent<Animator>().SetBool("head",true);
		Invoke("recycle",5);
	}

	private void shotDying(){
		GetComponent<Animator>().SetBool("shot",true);
		Invoke("recycle",3);
	}

	private void BladeDying(){
		GetComponent<Animator>().SetBool("bowels",true);
		Invoke("recycle",3);
	}

	private void AxeDying(){
		GetComponent<Animator>().SetBool("cut",true);
		Invoke("recycle",3);
	}

	private void WeightDying(){
		GetComponent<Animator>().SetBool("flatten",true);
		Invoke("recycle",3);
	}

	private void AirPumpDying(){
		GetComponent<Animator>().SetBool("explosion",true);
		Invoke("DestroyMe",2);
	}

	private void DestroyMe(){
		Destroy(this.gameObject);
	}

	private void recycle(){
		GameObject.Find("Transporter").GetComponent<Animator>().enabled=true;
		speed=0.5f;

	}
}
