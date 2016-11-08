using UnityEngine;
using System.Collections;

public class PitchControl : MonoBehaviour {
	
    public Sinus Sn;
	public Vector3 PitInit;
    public double currentFreq;
    public Vector3 stepDestination;
    public float glide { get; set; }
	public float vel = 1;
    public float scale = 10;
    public int note;
    public KeyCode[] codes = { KeyCode.A, KeyCode.W, KeyCode.S, KeyCode.E, KeyCode.D, KeyCode.F, KeyCode.T, KeyCode.G, KeyCode.Y, KeyCode.H, KeyCode.U, KeyCode.J, KeyCode.K, KeyCode.O, KeyCode.L, KeyCode.P, KeyCode.Semicolon, KeyCode.Quote };
    public float keyboardOffset { get; set; }
		
	// Use this for initialization
	void Start () {

        glide = 0.1f;
		PitInit = transform.position;
        stepDestination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        currentFreq = ((transform.position.y - PitInit.y) * scale) + Sn.noteEquation(0);
        Sinus.frequency = currentFreq;
        //Sinus.frequency = Mathf.Lerp((float)(Sinus.frequency), (float)(currentFreq), (float)glide);

        transform.position = Vector3.Lerp(transform.position, stepDestination, glide);
	
		//wave changes
		if (Input.GetKeyDown (KeyCode.UpArrow)) {

            //transform.Translate(Vector3.up * vel); 
            note++;
            stepDestination = new Vector3 (transform.position.x, locationFromStep(note), transform.position.z);
			
		}
        if (Input.GetKeyDown(KeyCode.DownArrow)) {


            //if (transform.position.y > PitInit.y) {
            if (note > 0)
            {
                //transform.Translate (Vector3.down * vel);
                note--;
                stepDestination = new Vector3(transform.position.x, locationFromStep(note), transform.position.z);


            }

        }
        for(int i = 0; i < 18; i++)
        {
            if(Input.GetKeyDown(codes[i]))
            {
                note = i + (int)keyboardOffset;
                stepDestination = new Vector3(transform.position.x, locationFromStep(note), transform.position.z);
            }
        }

    }
    public float locationFromStep(int newStep)
    {
        
        double newNote = Sn.noteEquation(newStep);
        return PitInit.y + ((float)(newNote - Sn.noteEquation(0)) / scale);

    }

}


