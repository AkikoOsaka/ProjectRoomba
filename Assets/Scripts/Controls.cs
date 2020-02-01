using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	public GameObject playerController;
	public GameObject dogController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				// the object identified by hit.transform was clicked
				// do whatever you want
				if(hit.transform.name == "Ground"){
					playerController.SendMessage ("MoveTo", hit.point);
				}

				if(hit.transform.name.Contains("Findspot")){
					dogController.SendMessage ("MoveTo", hit.point);
				}
				
				if(hit.transform.name.Contains("Find_")){
					dogController.SendMessage ("PlaceInInventory", hit.transform.gameObject);
				}
				
				if(hit.transform.name == "Dog"){
					dogController.SendMessage ("ShowInventory");
				}
				//Debug.Log(hit.point);
			}
		}
    }

}
