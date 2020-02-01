using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Workbench : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void ShowInWorkbench(string _nameAndnumber){
		string [] splitarray = _nameAndnumber.Split('_');
		string _name = splitarray[0] + "_" + splitarray[1] + "_" + splitarray[2];
		float _number = float.Parse(splitarray[3]);
		
		if(transform.GetChild(1).gameObject.activeSelf){
			if(_name == gameObject.name){
				transform.GetChild(1).gameObject.SetActive(false);
				transform.GetChild(2).gameObject.SetActive(true);
				transform.GetChild(2).gameObject.GetComponent<Text>().text = _number.ToString();
			}
		} else {
			if(_name == gameObject.name){
				transform.GetChild(2).gameObject.GetComponent<Text>().text = _number.ToString();
				
				if(_number == 0){
					gameObject.GetComponent<Button>().interactable = false;
				} else {
					gameObject.GetComponent<Button>().interactable = true;
				}
			}
		}
	}
	
	void ShowInInventar(string _nameAndnumber){
		string [] splitarray = _nameAndnumber.Split('_');
		string _name = splitarray[0] + "_" + splitarray[1] + "_" + splitarray[2];
		float _number = float.Parse(splitarray[3]);
		
		if(transform.GetChild(1).gameObject.activeSelf){
			if(_name == gameObject.name){
				transform.GetChild(1).gameObject.SetActive(false);
				transform.GetChild(2).gameObject.SetActive(true);
				transform.GetChild(2).gameObject.GetComponent<Text>().text = _number.ToString();
			}
		} else {
			if(_name == gameObject.name){
				transform.GetChild(2).gameObject.GetComponent<Text>().text = _number.ToString();
			}
		}
	}
	
	void DisableSelectionIcon(List<string> _selectedParts){
		foreach(string objectpart in _selectedParts){
			if(objectpart == gameObject.name){
				transform.GetChild(0).gameObject.SetActive(false);
			}
		}
	}
}
