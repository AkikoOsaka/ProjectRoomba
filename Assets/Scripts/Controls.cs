using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{
	public GameObject playerController;
	public GameObject dogController;
	public GameObject workbenchRobosCanvas;
	public GameObject workbenchArtefactsCanvas;
	public GameObject dogInventoryCanvas;

	public GameObject tavanbot;

	public List<string> roboDummy = new List <string>();
	public List<string> artefactDummy = new List <string>();
	public Transform roboText;
	public Transform artefactText;
	
	bool menuOpen = false;
	bool successfulRepair = false;
	bool moveCamera = false;
	int wrongPart = 0;
	float speed = 2f;
	List<string> selectedParts = new List<string>();
	List<string> redParts = new List<string>();
	List<string> blueParts = new List<string>();
	List<string> yellowParts = new List<string>();
	GameObject selectedPart;

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
				if(!menuOpen){
					if(hit.transform.name == "Ground"){
						playerController.SendMessage ("MoveTo", hit.point);
					}

					if(hit.transform.name.Contains("Findspot")){
						dogController.SendMessage ("MoveTo", hit.point);
					}
					
					if(hit.transform.name.Contains("Robo_") || hit.transform.name.Contains("Artefact_")){
						dogController.SendMessage ("PlaceInInventory", hit.transform.gameObject);
					}
					
					if(hit.transform.name == "Dog"){
						dogInventoryCanvas.SetActive(true);
						menuOpen = true;
						
						foreach(string key in dogController.GetComponent<Dog>().inventory.Keys){
							string temp = dogController.GetComponent<Dog>().inventory[key].ToString();
							dogInventoryCanvas.BroadcastMessage ("ShowInInventar", key + "_" + temp);
						}

					}
					if(hit.transform.name == "tavanbot") {
						tavanbot.SendMessage ("SwitchSong");
					}
					if(hit.transform.name == "Workbench_Robos"){
						workbenchRobosCanvas.SetActive(true);
						menuOpen = true;
						
						foreach(string key in dogController.GetComponent<Dog>().inventory.Keys){
							string temp = dogController.GetComponent<Dog>().inventory[key].ToString();
							workbenchRobosCanvas.BroadcastMessage ("ShowInWorkbench", key + "_" + temp);
						}
					}
					
					if(hit.transform.name == "Workbench_Artefacts"){
						workbenchArtefactsCanvas.SetActive(true);
						menuOpen = true;
						
						foreach(string key in dogController.GetComponent<Dog>().inventory.Keys){
							string temp = dogController.GetComponent<Dog>().inventory[key].ToString();
							workbenchArtefactsCanvas.BroadcastMessage ("ShowInWorkbench", key + "_" + temp);
						}
					}
				} else {
					//Debug.Log(hit.transform.name);
				}
				//Debug.Log(hit.point);
			}
		}
		
		if(moveCamera){
			transform.parent.transform.position = Vector3.MoveTowards (transform.parent.transform.position, new Vector3(playerController.transform.position.x, transform.parent.transform.position.y, playerController.transform.position.z), Time.deltaTime * speed);
		}
    }
	
	void OnTriggerExit(Collider _collision){
		/*if (Vector3.Distance (transform.position, playerController.transform.position) > 10) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z), Time.deltaTime * speed);
			Debug.Log("Kamera");
		} else {
			//transform.position = playerController.transform.position;
		}*/
		
		if(_collision.gameObject.name == playerController.name){
			//transform.position = Vector3.MoveTowards (transform.position,  playerController.transform.position, Time.deltaTime * speed);
			moveCamera = true;
			Debug.Log(_collision.gameObject.name);
		}
	}
	
	void OnTriggerStay(Collider _collision){
		/*if (Vector3.Distance (transform.position, playerController.transform.position) > 10) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z), Time.deltaTime * speed);
			Debug.Log("Kamera");
		} else {
			//transform.position = playerController.transform.position;
		}*/
		
		if(_collision.gameObject.name == playerController.name){
			//transform.position = Vector3.MoveTowards (transform.position,  playerController.transform.position, Time.deltaTime * speed);
			moveCamera = false;
			//Debug.Log(_collision.gameObject.name);
		}
	}
	
	public void UsePartInWorkbench(){
		selectedPart = EventSystem.current.currentSelectedGameObject;
		if(selectedParts.Contains(selectedPart.name)){ 
			selectedPart.transform.GetChild(0).gameObject.SetActive(false);
			selectedParts.Remove(selectedPart.name);
		} else {
			selectedPart.transform.GetChild(0).gameObject.SetActive(true);
			selectedParts.Add(selectedPart.name);
		}
	}
	
	public void CloseWindow(GameObject _object){
		_object.SetActive(false);
		menuOpen = false;
	}
	
	public void BuildObjectAtWorkbench(string _workbenchname){
		wrongPart = 0;
		if(selectedParts.Count == 0){
			Debug.Log("SELECTED PARTS EMPTY");
			return;
		}
		
		if(_workbenchname == "Robo"){
			if(selectedParts.Count < roboDummy.Count){
				return;
			}
			foreach(string objectpart in selectedParts){
				foreach(string dummypart in roboDummy){
					if(!objectpart.Contains(dummypart)){
						wrongPart++;
					}
				}
				if(wrongPart >= roboDummy.Count){
					//Parts dont fit so stopp the repair
					wrongPart = 0;
					return;
				}
				wrongPart = 0;
			}
		} else if(_workbenchname == "Artefact") {
			if(selectedParts.Count < artefactDummy.Count){
				return;
			}
			foreach(string objectpart in selectedParts){
				foreach(string dummypart in artefactDummy){
					if(!objectpart.Contains(dummypart)){
						wrongPart++;
					}
				}
				if(wrongPart >= roboDummy.Count){
					//Parts dont fit so stopp the repair
					wrongPart = 0;
					return;
				}
				wrongPart = 0;
			}
		}
		
		foreach(string objectpart in selectedParts){
			if(objectpart.Contains("Blue")){
				blueParts.Add(objectpart);
			} else if(objectpart.Contains("Red")){
				redParts.Add(objectpart);
			} else if(objectpart.Contains("Yellow")){
				yellowParts.Add(objectpart);
			}
		}
		
		if(blueParts.Count == selectedParts.Count){
			successfulRepair = true;
		} else if(redParts.Count == selectedParts.Count){
			successfulRepair = true;
		} else if(yellowParts.Count == selectedParts.Count){
			successfulRepair = true;
		} else {
			successfulRepair = false;
		}
		
		if(successfulRepair){
			foreach(string objectpart in selectedParts){
				dogController.GetComponent<Dog>().inventory[objectpart] -= 1;
			}
			
			if(_workbenchname == "Robo"){
				workbenchRobosCanvas.BroadcastMessage ("DisableSelectionIcon", selectedParts);
				workbenchRobosCanvas.SetActive(false);
				int _number = int.Parse(roboText.GetComponent<Text>().text);
				roboText.GetComponent<Text>().text = (_number + 1).ToString();
			} else if(_workbenchname == "Artefact") {
				workbenchArtefactsCanvas.BroadcastMessage ("DisableSelectionIcon", selectedParts);
				workbenchArtefactsCanvas.SetActive(false);
				int _number = int.Parse(artefactText.GetComponent<Text>().text);
				artefactText.GetComponent<Text>().text = (_number + 1).ToString();
			}
			
			menuOpen = false;
			successfulRepair = false;
		} else {
			Debug.Log("Falsche Teile");
			if(_workbenchname == "Robo"){
				workbenchRobosCanvas.BroadcastMessage ("DisableSelectionIcon", selectedParts);
			} else if(_workbenchname == "Artefact") {
				workbenchArtefactsCanvas.BroadcastMessage ("DisableSelectionIcon", selectedParts);
			}
		}
		
		selectedParts.Clear();
		blueParts.Clear();
		redParts.Clear();
		yellowParts.Clear();
		wrongPart = 0;
		
	}

}
