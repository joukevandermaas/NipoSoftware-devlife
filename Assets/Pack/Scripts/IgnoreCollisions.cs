using UnityEngine;
using System.Collections;

public class IgnoreCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Player"), true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
