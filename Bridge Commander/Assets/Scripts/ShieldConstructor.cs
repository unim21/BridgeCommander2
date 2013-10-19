using UnityEngine;
using System.Collections;

public class ShieldConstructor : MonoBehaviour {
	
	//Variables
	public GameObject shield;
	private GameObject newShield;
	private float localRotationX;
	void Start(){
		
		newShield = Instantiate(shield, transform.position, Quaternion.identity) as GameObject;
		newShield.transform.parent = transform;
		newShield.transform.rotation.x += 90;
	}
}
