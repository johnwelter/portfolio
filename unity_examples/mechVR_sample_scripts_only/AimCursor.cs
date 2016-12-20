using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

/*////////////////////
 * 
 *  AimCursor
 *  
 *  sends a ray out from the center of the camera to search for targets.
 *  if a target is found, put up an aim cursor.
 *  if a target is found while the lock button is held, put up a lock cursor (up to four).
 *  if the lock button is released, fire missles at locked targets
 *  
 */

public class AimCursor : MonoBehaviour {

	public int maxDistance = 100;       //max distance you can aim to
	public List<GameObject> lockLoc;    //list of locations of currently locked targets
	public GameObject aimLoc;           //currently aimed at target
	public GameObject prefabAim;        //aim cursor prefab
	public GameObject prefabLock;       //lock cursor prefab
	public GameObject missile;          //missle prefab
	public int axisInUse;               //integer to check if the lock button is pressed/released
	public float distance = 10;         //not sure if this is used anywhere

	public GameObject turret;           //craft's turret set
	public GameObject[] launchSpots;    //launch locations for missles (in front of turrets)

    public LayerMask mask = -1;         //layer mask to aliviate how many things the ray cast is looking at
	
	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = new Ray();                            //create ray
        ray.origin = transform.position;                //
        ray.direction = Camera.main.transform.forward;  //direct it from the center of the main camera
		RaycastHit hit;                                 

		if (Input.GetAxis("Fire4") < axisInUse){        //if the lock button was let go, 

			axisInUse = 0;                              

            for (int i = 0 ; i < lockLoc.Count; i++){   //for every target locked onto
                                    
				lockLoc[i].GetComponent<isLockedOnto>().locked = false; //set some bools to make the target not locked onto
				lockLoc[i].GetComponent<isLockedOnto>().aimed = false;



				turret.transform.LookAt(lockLoc[i].transform.position); //have the turrets of the craft look in the direction of the current target
				GameObject go = ((GameObject)Instantiate(missile, launchSpots[i].transform.position, launchSpots[i].transform.rotation));  //fire missle from ith turret
				go.GetComponent<missleControl>().set(lockLoc[i].gameObject); //set the missle's target to the current target


			}
			lockLoc.Clear();    //clear locked target locations
			GameObject[] locks = GameObject.FindGameObjectsWithTag("lock"); //find all the lock cursors
			foreach(GameObject L in locks){
                    
				Destroy(L);        //destroy them
			}
			
		}

        bool successfulCast = Physics.Raycast(ray, out hit, maxDistance, mask.value);   //if the cast was succesful in fnding an object,
   
        if (successfulCast && hit.collider.gameObject.tag.Equals ("target") && lockLoc.Count < 4 ) { //and the found object is a target, and there are less than 4 targets locked into

			GameObject[] goot = GameObject.FindGameObjectsWithTag ("shot"); //check to see if any missles or shots are already in the world
			GameObject[] got = GameObject.FindGameObjectsWithTag("missile");

			if ((Input.GetAxis("Fire4")==1) && got.Length == 0 && goot.Length == 0) {   //if there are no live shots, and the lock button is pressed

				axisInUse = (int)Input.GetAxis("Fire4");    
				if(!hit.collider.gameObject.GetComponent<isLockedOnto> ().locked){          //if the game object is not already locked onto
					Destroy(GameObject.FindGameObjectWithTag("aim"));                       //git rid of the aim cursor
					hit.collider.gameObject.GetComponent<isLockedOnto> ().locked = true;    //set some bools on the target to show that it's locked onto
					hit.collider.gameObject.GetComponent<isLockedOnto> ().aimed = true;     
					lockLoc.Add (hit.collider.gameObject);                                  //add target to list of locked targets
					GameObject A = ((GameObject)Instantiate (prefabLock, hit.transform.position, Quaternion.identity)); //create Lock cursor, which looks at the player and scales accordingly by distance (creating a GUI illusion)
                    A.GetComponent<CursorControl>().parent = hit.collider.gameObject;       //set the pseudo parent of the aim cursor to be the target
                }

			} else {    //if the lock button isn't pressed, just aim
                    
				if(!hit.collider.gameObject.GetComponent<isLockedOnto> ().aimed ){  //if the target isn't currently being aimed at
					Destroy(GameObject.FindGameObjectWithTag("aim"));               //destroy the old aim cursor if there is one
					if(aimLoc)                                                      //if there's currently an aimLocation (target) already, uh...
						aimLoc.GetComponent<isLockedOnto>().aimed = false;          //un...aim from it...? unaim from it.
					hit.collider.gameObject.GetComponent<isLockedOnto> ().aimed = true;
					aimLoc = hit.collider.gameObject;                               //then, set the aimLocation to the current target we're looking at
					GameObject A = ((GameObject)Instantiate (prefabAim, hit.transform.position, Quaternion.identity));  //create the aim cursor, which behaves like the lock cursor
                    A.GetComponent<CursorControl>().parent = hit.collider.gameObject;      //set the cursor's pseudo parent to the target
                }

			}


		} else {

			Destroy(GameObject.FindGameObjectWithTag("aim"));   //if looking at nothing, destroy aim cursor
			if(aimLoc)                                          //unaim from last target.
				aimLoc.GetComponent<isLockedOnto>().aimed = false;

		}

	
	}
}
