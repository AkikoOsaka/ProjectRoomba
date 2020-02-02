﻿using System.Collections;
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
	bool digging = false;
	bool gotOrder = false;
	float diggingtime;

	// Start is called before the first frame update
	void Start(){
		lookposition = transform.position; 
	}

	// Update is called once per frame
	void Update(){

		if(gotOrder){
			if (Vector3.Distance (transform.position, lookposition) > 0.4) {
				transform.position = Vector3.MoveTowards (transform.position, lookposition, Time.deltaTime * speed);
			} else {
				transform.position = lookposition;
				digging = true;
			}
		} else {
			transform.LookAt(new Vector3 (dogplace.parent.position.x, transform.position.y, dogplace.parent.position.z));
			if (Vector3.Distance (transform.position, dogplace.position) > 0.4) {
				transform.position = Vector3.MoveTowards (transform.position, dogplace.position, Time.deltaTime * speed);
			} else {
				transform.position = dogplace.position;
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
			if(diggingtime < diggingspeed){
				diggingtime += Time.deltaTime;
			} else {
				digging = false;
				gotOrder = false;
				diggingtime = 0;
				GameObject findObject = Instantiate (_collision.gameObject.GetComponent<Findspot>().find, gameObject.transform.parent);
				findObject.name = _collision.gameObject.GetComponent<Findspot>().find.name;
				findObject.transform.position = gameObject.transform.position;
				Destroy (_collision.gameObject);
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
