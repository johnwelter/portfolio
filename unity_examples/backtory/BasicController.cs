using UnityEngine;
using System.Collections;

/*
 *  BasicController
 *  
 *  modified version of the basic controller for the rhythm tool
 * 
 *  handles all functions that happen on the beat of the music, like the conveyer belts.
 *  also handles resetting, starting, and finishing up the game.
 * 
 */
public class BasicController : MonoBehaviour
{	
	/// <summary>
	/// RhythmTool. 
	/// </summary>
	public RhythmTool rhythmTool; //the rhythm tool of the game

	public bool test = true;      //
		
	/// <summary>
	/// AudioClip of a song.
	/// </summary>
	public AudioClip audioClip;  //the current song to play

	public pass[] movers;        // the ball movers
	public AudioSource[] indicators;    //the sound effects for success/fail etc.
	public spawn[] spawners;        //ball spawners
	public spawn[] beltSpawners;    //belt spawners
	public int[] nbacktrack;        //nback list 
	public int[] missReserve;       //reserve to find if any previous signals were missed


	public int suc = 0;             //successful matches
	public int mis = 0;             //misses
	public int err = -1;            //errors
	public bool forcestop = false;  //boolean to see if game is over based on a bad score
	public int backAmount;          //n of the nback

	//public Vector3 next;

	public int beats = 0;           //amount of beats
	public int steps;               //amount of signals that have passed
	public int lastFrame = 0;       

	public bool end = false;        //boolean for end of song
	
	private Frame[] low;            
	// Use this for initialization


	public bool paused;
	void Start ()
	{		


		//s = transform.Find("spawn").GetComponent<spawn> ();
		backAmount = StartVar.nback;    //get nback amount from the global start values
		print (backAmount);             
		
		rhythmTool=GetComponent<RhythmTool>(); //get the rhythm tool
		
		rhythmTool.NewSong(audioClip);          //set song for the rhythm tool to play
		
		low = rhythmTool.Low.Frames;            
		nbacktrack = new int[backAmount + 1];   //create nback list
		missReserve = new int[backAmount - 1];  //create reserve list
        //GetComponent<reachscaler>().enabled = true;
        //Instantiate (startball, new Vector3 (((float)0.08), ((float)2.05), ((float)-0.23)), Quaternion.identity);
    }

    /*/////////////////
     *  
     *  OnEndOfSong
     * 
     *  resets game if endofsong signal sent
     *  also sets text of screen to show if the player won or lost  
     * 
     * 
     */
	void OnEndOfSong()
	{

        //GetComponent<reachscaler>().enabled = false;

        if (suc <= err || forcestop) {      //if the player loses, play lose sound, set screen to "YOUR'E FIRED"

			GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[2];
			GameObject.Find ("effects").GetComponent<AudioSource>().Play();
			GameObject.Find ("labels").GetComponent<TextMesh> ().text = "YOU'RE\nFIRED";

		} else {                            //if the player wins, play win sound, set screen to "GOOD JOB"

			GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[3];
			GameObject.Find ("effects").GetComponent<AudioSource>().Play();
			GameObject.Find ("labels").GetComponent<TextMesh> ().text = "GOOD\nJOB";
		}

		
		nbacktrack = new int[backAmount + 1];   //reset nback and reserve lists
		missReserve = new int[backAmount - 1];
		//Instantiate (startball, new Vector3 (((float)0.08), ((float)2.05), ((float)-0.23)), new Quaternion ());
		rhythmTool.NewSong (audioClip);         //reset rhythm tool song
		beats = -1;                             //reset all values
		steps = 0;                          
		lastFrame = 0;
		suc = 0;
		mis = 0;
		err = 0;
		forcestop = false;

		foreach(spawn S in beltSpawners)        //reactivate spawners
		{
			S.active = true;
		}
		
		foreach(pass P in movers)               //reactivate movers
		{
			P.active = true;
		}

		GameButton.start = true;                //reset the game button to act as a start button
        //GetComponent<reachscaler>().enabled = true;


	}
	// Update is called once per frame
	void Update ()
	{		

		for(int i = lastFrame+1; i < rhythmTool.CurrentFrame+1; i++)
		{
			int nextBeat = rhythmTool.NextBeatIndex();

			if(rhythmTool.CurrentFrame > nextBeat && rhythmTool.CurrentFrame > rhythmTool.TotalFrames/2 && !end) //if we're on the last beat (that's looked at), prepare to finish 
			{
				GameObject.Find("success").GetComponent<TextMesh> ().text = "";
				GameObject.Find ("misses").GetComponent<TextMesh> ().text = "";
				GameObject.Find("errors").GetComponent<TextMesh> ().text = "";

				GameObject.Find("labels").GetComponent<TextMesh>().text = "finish!\nplease\nwait...";
				end = true;
			}
			if(rhythmTool.IsBeat(i, 0) == 1 || end) //if we're on a beat, head thorugh the process
			{

				if(beats%(StartVar.difficultylevel) == 0){ // if we're on a beat on which something happens (aka every other beat), make that something happen.


					//chance of bomb (1/5)
					int isBomb = Random.Range (0, 5);   


					//randomly choose the ID of which ball will spawn
					int pickBall = Random.Range (0, 31); //gives every ball a 1/3 chance
					pickBall = pickBall % (3);

					//add oldest ID to reserve, add newest ID in it's place
					missReserve[steps%(backAmount-1)] = nbacktrack[steps%(backAmount+1)]; 
					nbacktrack[steps%(backAmount+1)] = pickBall+1;

					if(isBomb == 3) //if we're spawning a bomb, 
					{
						pickBall += 3; //add to the pickball to shift the index over to the bombs

					//randomly choose which spawner creates the ball (50/50) chance
					int pickSpawner = Random.Range (0, 101);   
					pickSpawner = pickSpawner % 2;

					//spawn selected ball from selected spawner, play sound of which one was selected
					if(!end){
						spawners[pickSpawner].doIt(pickBall);
						indicators[pickSpawner].Play();
					}

					//swap the circle colors here:

					//code to set up and call shift
					//if(backAmount==2)
					//{
					//	int S = nbacktrack[(steps)%3];
					//	int A = nbacktrack[(steps+1)%3];
					//	int B = nbacktrack[(steps+2)%3];
					//	GameObject.Find("CircleController").GetComponent<ChangeColor>().shift(S, A, B);
					//}

					//

					steps++;    //add to steps

                    //some debug to print out the current signal chart (what's current and what's reserved)
					string printOut = "(";  

					for(int j = 0; j < (backAmount+1); j++)
					{
						printOut += nbacktrack[j] + ", ";
					}
					printOut += ")(";

					for(int k = 0; k < (backAmount-1); k++)
					{
						printOut += missReserve[k] + ", ";
					}
					printOut += ")";

					print (printOut);


					//spawn a new belt piece
					foreach(spawn S in beltSpawners)
					{
						S.doIt(0);
						if(end)
						{
							S.active = false;
						}
					}

					//get each mover to pass their object

					foreach(pass P in movers)
					{
						P.doIt();
						if(end)
						{
							P.active = false;
						}
					}

				}
				beats++;
			}

			lastFrame = i;
		}
		if(mis+err>=20)     //if our misses and errors total 20, force end the game
			print ("Stopping forcefully!!");
			rhythmTool.Stop ();
			GameObject.Find("success").GetComponent<TextMesh> ().text = "";
			GameObject.Find ("misses").GetComponent<TextMesh> ().text = "";
			GameObject.Find("errors").GetComponent<TextMesh> ().text = "";
			
			GameObject.Find("labels").GetComponent<TextMesh>().text = "finish!\nplease\nwait...";
			end = true;

			GameObject[] remaining;                                 //find and delete all remaining objects 
			remaining = GameObject.FindGameObjectsWithTag("ball");
			foreach (GameObject A in remaining)
			{
				Destroy(A);
			}
			remaining = GameObject.FindGameObjectsWithTag("bomb");
			foreach (GameObject A in remaining)
			{
				Destroy(A);
			}

			foreach(spawn S in beltSpawners)        //have the belts continue to move
			{
				S.doIt(0);
				if(end)
				{
					S.active = false;
				}
			}
			foreach(pass P in movers)
			{
				P.doIt();
				if(end)
				{
					P.active = false;
				}
			}
			
			forcestop=true;
			OnEndOfSong();
			
		}
	}

    /*///////////
     * 
     *  check
     *  
     *  if a ball is hit, this checks if it was a successful hit.
     *  if the ball's ID number is 0, it's a normal ball. 
     *      if the ball's ID matches the ID in the nback list n ID's back, it's a success. play the hit sound and up the score
     *      if not, it's an error. play the error sound and up the error count.
     *  if the ball's ID number is 2, it's a bomb and is always a success. play the hit sound and up the score.
     *  
     *  
     *  NOTE: before the buttons were used to start the game, a start ball was used. that's why the ID number is referred to here as notStart, since I 
     *        since we wanted to make sure a ball being hit (which always calls this method) was not the start ball, hence notStart.
     * 
     */

	public void check(int notStart)
	{

		if (notStart == 0) {

			if (steps > backAmount && nbacktrack [(steps-1)%(backAmount+1)] == nbacktrack [(steps-backAmount-1)%(backAmount+1)]) {
				
				//print ("yep!");
				GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[0];
				GameObject.Find ("effects").GetComponent<AudioSource>().Play();
				suc++;
				GameObject.Find ("success").GetComponent<TextMesh>().text = suc.ToString();
			} else {
				
				//print ("nope!");
				GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[1];
				GameObject.Find ("effects").GetComponent<AudioSource>().Play();
				err++;
				GameObject.Find ("errors").GetComponent<TextMesh>().text = "\n\n"+err.ToString();
			}



		}
		if (notStart == 2) {

			//print ("saved!");
			GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[0];
			GameObject.Find ("effects").GetComponent<AudioSource>().Play();
			suc++;
			GameObject.Find ("success").GetComponent<TextMesh>().text = suc.ToString();

		}

	}
}
