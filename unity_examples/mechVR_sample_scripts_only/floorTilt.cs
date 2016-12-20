using UnityEngine;
using System.Collections;


/*
 * 
 *  floorTilt
 *  
 *  uses curves to tilt a floor in an occilating motion (left to right (roll tilt), or front to back(pitch tilt))
 * 
 */


public class floorTilt : MonoBehaviour {


	public float amplitude = 20;    //how far the floors will tilt
	public float offset;            //an offset to stagger multiple floors
	public float rotation;          //rotation to send to the craft's rotation
	public float speed;             //how fast to tilt

	public float slidePower;        //how fast the craft will slide on the floor

    public bool pTilt = false;      //bool to check if it's a pitch tilt

    public Vector3 euler;           //used to set rotation of floor after mathing

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		//use this to have the floor be fast on flat, and hold on the tilt (a normal sin wave)
		//rotation = amplitude * Mathf.Sin ((Time.realtimeSinceStartup*speed) + offset);

		//use this to have the floor hold on flat, and be fast on the tilt (a sin wave mixed with a cos wave in a funky way)
		rotation = (amplitude * Mathf.Sin ((Time.realtimeSinceStartup*speed) + offset)) * Mathf.Abs(Mathf.Abs(Mathf.Cos((Time.realtimeSinceStartup*speed) + offset))-1);
		slidePower = (rotation / amplitude); //the farther it's tilted, the more powerful the slide

        if(pTilt)
        {
            euler = new Vector3(0, 0, rotation);    //if it's a pitch tilt, rotate on z
        }
        else
        {

            euler = new Vector3(rotation, 0, 0);    //if it's a roll tilt, rotate on x
        }

		transform.localRotation = Quaternion.Euler (euler); //set the rotation of the floor object
		if (rotation > 180) {
			 
			rotation = (360 - rotation) * -1;   //if it's over 180, set it to the negative

		}
	
	}
}
