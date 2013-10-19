#pragma strict

var projectile:Rigidbody;
var tspeed:float = 0.0;
var thisTarget:GameObject;
//var torpTimeout:float = 60;
private var launchLocation:Vector3;
launchLocation = Vector3(transform.position.x,transform.position.y,transform.position.z);

function Update(){
	if (Input.GetButtonDown("Fire1")){
		GetLaunchLocation();
		var instantiatedProjectile:Rigidbody = Instantiate(projectile,launchLocation,transform.rotation);
		instantiatedProjectile.velocity = transform.forward * tspeed * 1;
		Debug.Log("root: " + instantiatedProjectile.transform.root + " and collider: " + transform.root.collider + " and torp collider: " + instantiatedProjectile.collider);
		Physics.IgnoreCollision(instantiatedProjectile.collider,transform.root.collider);
		Debug.Log("Torp Fired");
		//DirectToTarget(instantiatedProjectile,thisTarget);
	}
	if(instantiatedProjectile){
		instantiatedProjectile.transform.Rotate(Vector3(10.0,0.0,0.0));
		Debug.Log("Torp turning. torp is: " + instantiatedProjectile.name);
		//yield WaitForSeconds(torpTimeout);
		//Destroy instantiatedProjectile;
	}
}
function GetLaunchLocation(){
	launchLocation = Vector3(transform.position.x,transform.position.y,transform.position.z);
}
/*function DirectToTarget(var myProjectile:Rigidbody,var toKill:GameObject)
{
myProjectile.transform.Rotate(10,0,0);
}
*/

function LookAtTarget(){
//	projectile.transform.LookAt(thisTarget);
}

function TargetMoved(){
	var fore;
	//if(Physics.Raycast
}