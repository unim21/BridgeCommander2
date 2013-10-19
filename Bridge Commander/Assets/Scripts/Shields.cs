using UnityEngine;
using System.Collections;

/// <summary>
/// Shield Script. Copyright 2013-2014 Bridge Commander 2.
/// All rights reserved. Star Trek property of CBS and Paramount Pictures.
/// Code author: Frederic Babord
/// Date Created: 9th October 2013
/// Date Complete: TBA
/// </summary>

public class Shields : MonoBehaviour {
	//Variables
	public float regenRate = 6f;
	public float shieldVisibleTime = 3.5f;
	public float shieldCapacity = 100.0f;
	public float maxShieldCapacity = 100.0f;
	
	public float leftSC;
	public float rightSC;
	public float topSC;
	public float bottomSC;
	public float foreSC;
	public float aftSC;
	
	public Color shieldColor = Color.cyan;
	public GameObject shield;
	public ParticleSystem shieldEffect;
	public Shader shieldShader;
	
	public float shieldRegenerationInterval = 5.0f;
	public float shieldRegenerationAmaount = 1f;
	
	// Use this for initialization
	void Start () {
		shieldColor.a = 0.25f;
		shieldShader = Shader.Find("Transparent/Diffuse"); //Shield shader needs work
		gameObject.renderer.material.shader = shieldShader;
		leftSC = shieldCapacity;
		rightSC = shieldCapacity;
		topSC = shieldCapacity;
		bottomSC = shieldCapacity;
		foreSC = shieldCapacity;
		aftSC = shieldCapacity;
		shieldEffect.enableEmission = false;
		shield.renderer.enabled = false;
	}
	
	void Update(){
		if(leftSC < maxShieldCapacity || rightSC < maxShieldCapacity || topSC < maxShieldCapacity || bottomSC < maxShieldCapacity || foreSC < maxShieldCapacity || aftSC < maxShieldCapacity){
			RegenerateShield();
		}
	}
	
	void OnCollisionEnter(Collision collision){
		Vector3 hitDirection = collision.contacts[0].point - transform.position;
		Vector3 left = transform.TransformDirection( Vector3.left );
		Vector3 right = transform.TransformDirection( Vector3.right );
		Vector3 top = transform.TransformDirection( Vector3.up );
		Vector3 down = transform.TransformDirection( Vector3.down );
		Vector3 fore = transform.TransformDirection( Vector3.forward );
		Vector3 back = transform.TransformDirection( Vector3.back );
		
		shield.renderer.enabled = true;
		ContactPoint contact = collision.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		//ShieldDamager();
		Instantiate(shieldEffect, pos, rot);
		shieldEffect.enableEmission = true;
		//Requires specific shield facing script
		shield.renderer.enabled = true;
		shield.renderer.material.color = shieldColor;
		//ShieldDamager();
		
		if( Vector3.Dot( left, hitDirection ) > 0 ){
			ShieldDamageLeft();
		}
		if(Vector3.Dot ( right, hitDirection) > 0){
			ShieldDamageRight();
		}
		if(Vector3.Dot ( top, hitDirection) > 0){
			ShieldDamageTop();
		}
		if(Vector3.Dot ( down, hitDirection) > 0){
			ShieldDamageDown();
		}
		if(Vector3.Dot ( fore, hitDirection) > 0){
			ShieldDamageFor();
		}
		if(Vector3.Dot ( back, hitDirection) > 0){
			ShieldDamageAft();	
		} 
	}
	
	
	void OnCollisionExit(Collision collision){
		StartCoroutine("shieldTimer");
		shieldEffect.enableEmission = false;
	}
	
	void RegenerateShield(){
		StartCoroutine("shieldRegenTimer");
	}

	
	void ShieldDamageLeft(){
		leftSC -= 50;
	}
	
	void ShieldDamageRight(){
		rightSC -= 50;
	}
	
	void ShieldDamageTop(){
		topSC -= 50;
	}
	
	void ShieldDamageDown(){
		bottomSC -= 50;
	}
	
	void ShieldDamageFor(){
		foreSC -= 50;
	}
	
	void ShieldDamageAft(){
		aftSC -= 50;
	}

	IEnumerator shieldTimer(){
		yield return new WaitForSeconds(shieldVisibleTime);
		StartCoroutine("shieldAlphaTransparency");
		StopCoroutine("shieldTimer");
	}
	
	IEnumerator shieldRegenTimer(){
		yield return new WaitForSeconds(shieldRegenerationInterval);
		if(leftSC < maxShieldCapacity){
			leftSC += shieldRegenerationAmaount;
			if(leftSC > maxShieldCapacity){
				leftSC = 100.0f;
			}
		}
		if(rightSC < maxShieldCapacity){
			rightSC += shieldRegenerationAmaount;
			if(rightSC > maxShieldCapacity){
				rightSC = 100.0f;
			}
		}
		if(topSC < maxShieldCapacity){
			topSC += shieldRegenerationAmaount;
			if(topSC > maxShieldCapacity){
				topSC = 100.0f;
			}
		}
		if(bottomSC < maxShieldCapacity){
			bottomSC += shieldRegenerationAmaount;
			if(bottomSC > maxShieldCapacity){
				bottomSC = 100.0f;
			}
		}
		if(foreSC < maxShieldCapacity){
			foreSC += shieldRegenerationAmaount;
			if(foreSC > maxShieldCapacity){
				foreSC = 100.0f;
			}
		}
		if(aftSC < maxShieldCapacity){
			aftSC += shieldRegenerationAmaount;
			if(aftSC > maxShieldCapacity){
				aftSC = 100.0f;
			}
		}
		StopCoroutine("shieldRegenTimer");
	}
	
	IEnumerator shieldAlphaTransparency(){
		while(shieldColor.a != 1.0f){
			yield return new WaitForSeconds(1);
			shieldColor.a += 0.2f;
			if(shieldColor.a > 1.0f){
				shieldColor.a = 1.0f;
			}
		}
		shield.renderer.enabled = false;
		shieldColor.a = 0.25f;
		StopCoroutine("shieldAlphaTransparency");
	}
}
