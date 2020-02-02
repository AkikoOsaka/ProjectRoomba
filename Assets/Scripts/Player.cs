using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int speed;

	public Animator Animator;

	Vector3 lookposition;
	Vector3 clickposition;

    // Start is called before the first frame update
    void Start(){
		Animator = gameObject.GetComponentInChildren<Animator>();
		lookposition = transform.position; 
    }

    // Update is called once per frame
    void Update(){
		if (Vector3.Distance (transform.position, lookposition) > 0.1) {
			transform.position = Vector3.MoveTowards (transform.position, lookposition, Time.deltaTime * speed);
			
		} else {
			transform.position = lookposition;
			Animator.SetBool("Walk", false);
		}
    }

	void MoveTo(Vector3 _clickposition){
		lookposition = new Vector3 (_clickposition.x, transform.position.y, _clickposition.z);
		transform.LookAt(lookposition);
		Animator.SetBool("Walk", true);
	}
}
