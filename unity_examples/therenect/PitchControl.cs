using UnityEngine;
using System.Collections;


/*///////////////
 *
 *  PitchControl
 *  
 *  the main control for the right hand block in the therenect game. controls the pitch of the theremin's ocilator. 
 *  
 * 
 */
public class PitchControl : MonoBehaviour {
	
    public Sinus Sn;                         //a Sinus that sends out the frequency to the audio output.          
	public Vector3 PitInit;                  //starting position of right hand block when the game is started, used to calculate offset
    public double currentFreq;               //current frequency bassed on the position of the block
    public Vector3 stepDestination;          //where the block will go if the keyboard or arrows are used to move the block
    public float glide { get; set; }         //how fast the box glides between notes when moved
	public float vel = 1;                    //velocity coefficient for moving the block
    public float scale = 10;                  //scale of the movement
    public int note;                          //which note we're on (for the note equation)

    //keyboard keys
    public KeyCode[] codes = { KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.E, KeyCode.D, KeyCode.F, KeyCode.T, KeyCode.G, KeyCode.Y, KeyCode.H, KeyCode.U, KeyCode.J, KeyCode.K, KeyCode.O, KeyCode.L, KeyCode.P, KeyCode.Semicolon, KeyCode.Quote };
    public float keyboardOffset { get; set; } //offset of keyboard when shifted
		
	// Use this for initialization
	void Start () {

        glide = 0.1f;                           //start with a low glide
		PitInit = transform.position;           //set initial position
        stepDestination = transform.position;   //destination to move is the current location, so it won't move
	}
	
	// Update is called once per frame
	void Update () {

        currentFreq = ((transform.position.y - PitInit.y) * scale) + Sn.noteEquation(0); //get the pitch from the blocks current location, nased on how far away it is from the base note
        Sinus.frequency = currentFreq;                                                   //set the frequency
       

        transform.position = Vector3.Lerp(transform.position, stepDestination, glide);  //move the block towards it's current goal
	
		//half step controls
        //
        //   increments or decrements the current note, gets location of where the block would need to move based in it's frequency 
        //
        //
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
 
            note++;                                                         
            stepDestination = new Vector3 (transform.position.x, locationFromStep(note), transform.position.z);
			
		}
        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            if (note > 0)
            {
                note--;
                stepDestination = new Vector3(transform.position.x, locationFromStep(note), transform.position.z);

            }
        }

        //keyboard controls
        //
        //  check all possible keycodes. if a valid key is pressed, find the location of where the block would go based of the frequncy associated with that key
        //

        for(int i = 0; i < 18; i++)
        {
            if(Input.GetKeyDown(codes[i]))
            {
                note = i + (int)keyboardOffset;     //add keyboard offset if the keyboard was shifted.
                stepDestination = new Vector3(transform.position.x, locationFromStep(note), transform.position.z);
            }
        }

    }
    /*/////////////
     * 
     * locationFromStep
     * 
     * finds where the block would need to go based on the offset of a given note from the base note. 
     * 
     */
    public float locationFromStep(int newStep)
    {
        
        double newNote = Sn.noteEquation(newStep);
        return PitInit.y + ((float)(newNote - Sn.noteEquation(0)) / scale);

    }

}


