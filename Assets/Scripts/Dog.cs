using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
	public int speed;
	public int diggingspeed;
	public Transform dogplace;
	public Dictionary <string,int> inventory = new Dictionary <string,int>();

	Vector3 lookposition;
	Vector3 clickposition;
	public bool digging = false;
	bool gotOrder = false;
	float diggingtime;

	Animator animator;

	// Start is called before the first frame update
	void Start(){
		lookposition = transform.position; 
		animator = gameObject.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update(){

		if(gotOrder){
			if (Vector3.Distance (transform.position, lookposition) > 0.4) {
				transform.position = Vector3.MoveTowards (transform.position, lookposition, Time.deltaTime * speed);
				animator.SetBool("Walk", true);
			} else {
				transform.position = lookposition;
				digging = true;
			}
		} else {
			transform.LookAt(new Vector3 (dogplace.parent.position.x, transform.position.y, dogplace.parent.position.z));
			if (Vector3.Distance (transform.position, dogplace.position) > 0.4) {
				transform.position = Vector3.MoveTowards (transform.position, dogplace.position, Time.deltaTime * speed);
				animator.SetBool("Walk", true);
			} else {
				transform.position = dogplace.position;
				animator.SetBool("Walk", false);
			}
		}
	}

	void MoveTo(Vector3 _clickposition){
		if(digging){
			digging = false;
			diggingtime = 0;
		}
		
		lookposition = new Vector3 (_clickposition.x, transform.position.y, _clickposition.z);
		transform.LookAt(lookposition);
		gotOrder = true;
	}

	void OnCollisionStay(Collision _collision){
		if(digging){
			
			Debug.Log(digging);
			if(diggingtime < diggingspeed){
				diggingtime += Time.deltaTime;
				animator.SetBool("Dig", true);
			} else {
				digging = false;
				gotOrder = false;
				diggingtime = 0;
				GameObject findObject = Instantiate (_collision.gameObject.GetComponent<Findspot>().find, gameObject.transform.parent);
				findObject.name = _collision.gameObject.GetComponent<Findspot>().find.name;
				findObject.transform.position = gameObject.transform.position;
				Destroy (_collision.gameObject);
				animator.SetBool("Dig", false);
			}
			
		}
	}
	
	void PlaceInInventory(GameObject _object){
		Debug.Log(_object.name);
		if(inventory.ContainsKey(_object.name)){
			inventory[_object.name] += 1;
		} else {
			inventory[_object.name] = 1;
		}
		Destroy(_object);
	}
}
