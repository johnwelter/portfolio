using UnityEngine;
using System.Collections;

/*
 * 
 *  SetOffset
 *  
 *  if the craft is on the tilting floor, the script figures out how much to offset the orientation of the craft to 
 *  match the tilt of the floor. it also causes the craft to slide based on how tilted the floor is.
 * 
 */


public class SetOffset : MonoBehaviour {


	public SteppingController SC;   //stepping controller of the physical craft
	public floorTilt FT;            //floor tilt script of this floor
	public float yRot;              //y value of the rotation of the floor
	public float pAmount;           //amount of pitch to send to craft
	public float rAmount;           //amount of roll to send to craft
	public float slideMagnitude;    //multiplyer to finess the effect of the slide power 
    public bool pTilt = false;      //boolean for pitch tilt
	public int pSign = 1;           //which direction the pitch is going (negative, positive)
	public int rSign = 1;           //which direction the roll is going (negative, positive) 
    public float rollMax = 26f, pitchMax = 12f;     //max angle for roll and pitch
	public Vector3 slideDirection;                  //direction to slide in
	public GameObject slideDirectorA;               //object to slide toward on one side of the floor
	public GameObject slideDirectorB;               //object to slide toward on other side of the floor

    // Use this for initialization
    void Start()
    {

        FT = GetComponent<floorTilt>();    //grab floor's floorTilt scrpit

    }
        
    /*/////////////////////////////////////
     * 
     *  OnTriggerStay
     * 
     *  as long as the craft is on the tilting floor, it's position and rotation will be affected by the floor.
     * 
     *  get the current y rotation of the craft
     *  use the tilt floor method (roll or pitch) to set the roll and pitch amounts to rotate the craft 
     *  find the size of the slide power and use slide directors to find out which way to slide the craft
     *  use moveTowards to slide the craft
     *  use the stepping controller to set the roll and pitch offset of the craft from the floor tilt
     * 
     */ 
	void OnTriggerStay(Collider obj)        
	{
		yRot = obj.gameObject.transform.rotation.eulerAngles.y; 

        if(pTilt)
        {
            pTiltFloor();
        }
		else
        {
            rTiltFloor();
        }
			
		//obj.gameObject.transform.Translate (slideDirection*(GetComponent<floorTilt>().slidePower/slideMagnitude));
		float step = (Mathf.Abs(FT.slidePower)*slideMagnitude) * Time.deltaTime;

		GameObject director;

		if (Mathf.Sign (FT.slidePower) == 1) {
			director = slideDirectorA;
		} else {
			director = slideDirectorB;
		}

		slideDirection = new Vector3 (director.transform.position.x, director.transform.position.y, obj.gameObject.transform.position.z);
		obj.gameObject.transform.position = Vector3.MoveTowards (obj.gameObject.transform.position, slideDirection, step);

        SC.setRollOffset ((rSign *rollMax * rAmount * FT.rotation) / FT.amplitude);
		SC.setPitchOffset ((pSign * pitchMax* pAmount * FT.rotation) / FT.amplitude);

	}

  
    /*///////////////////////////////
     * 
     *  pTiltFloor
     * 
     *  sets roll and pitch amounts to send to the craft based on the tilt of the floor (if it's a pitch tilt)
     *  also sets the sign for tilt direction
     * 
     */


    void pTiltFloor()
    {
        if (yRot > 180)
        {

            rSign = -1;
            yRot = yRot - 360;
        }
        else {

            rSign = 1;

        }

        float absRot = Mathf.Abs(yRot);

        if (absRot < 90)
        {

            pSign = 1;
            rAmount = absRot / 90;
            pAmount = (90 - absRot) / 90;
        }
        else {

            pSign = -1;
            pAmount = (absRot - 90) / 90;
            rAmount = (90 - (absRot - 90)) / 90;

        }

    }

    /*///////////////////////////////
    * 
    *  rTiltFloor
    * 
    *  sets roll and pitch amounts to send to the craft based on the tilt of the floor (if it's a roll tilt)
    *  also sets the sign for tilt direction
    * 
    */
    void rTiltFloor()
    {
        if (yRot > 180)
        {

            pSign = -1;
            yRot = yRot - 360;
        }
        else {

            pSign = 1;

        }

        float absRot = Mathf.Abs(yRot);

        if (absRot < 90)
        {

            rSign = -1;
            pAmount = absRot / 90;
            rAmount = (90 - absRot) / 90;

        }
        else {

            rSign = 1;
            rAmount = (absRot - 90) / 90;
            pAmount = (90 - (absRot - 90)) / 90;

        }

    }

    /*///////
     * 
     *  OnTriggerExit
     * 
     *  resets the offset of the craft once it leaves the floor
     *  
     */
	void OnTriggerExit(Collider obj)
	{

		SC.resetRollPitchOffset ();

	}
}
