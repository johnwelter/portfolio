using UnityEngine;
using System.Collections;

//This is a basic controller. It gives a song to the analyzer and starts playing and analyzing it.
//See DataController and the example scene for an example on how to get the data in a game.
public class BasicController : MonoBehaviour
{	
	/// <summary>
	/// RhythmTool. 
	/// </summary>
	public RhythmTool rhythmTool;

	public bool test = true;
		
	/// <summary>
	/// AudioClip of a song.
	/// </summary>
	public AudioClip audioClip;

	public pass[] movers;
	public AudioSource[] indicators;
	public spawn[] spawners;
	public spawn[] beltSpawners;
	public int[] nbacktrack;
	public int[] missReserve;

	public GameObject[] leftovers;


	public int suc = 0;
	public int mis = 0;
	public int err = -1;
	public bool forcestop = false;
	public int backAmount;

	//public Vector3 next;

	public int beats = 0;
	public int steps;
	public int lastFrame = 0;

	public bool end = false;
	
	private Frame[] low;
	// Use this for initialization


	public bool paused;
	void Start ()
	{		


		//s = transform.Find("spawn").GetComponent<spawn> ();
		backAmount = StartVar.nback;
		print (backAmount);
		
		rhythmTool=GetComponent<RhythmTool>();
		
		rhythmTool.NewSong(audioClip);
		
		low = rhythmTool.Low.Frames;
		nbacktrack = new int[backAmount + 1];
		missReserve = new int[backAmount - 1];
        //GetComponent<reachscaler>().enabled = true;
        //Instantiate (startball, new Vector3 (((float)0.08), ((float)2.05), ((float)-0.23)), Quaternion.identity);
    }

	void OnEndOfSong()
	{

        //GetComponent<reachscaler>().enabled = false;
        if (suc <= err || forcestop) {
			GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[2];
			GameObject.Find ("effects").GetComponent<AudioSource>().Play();
			GameObject.Find ("labels").GetComponent<TextMesh> ().text = "YOU'RE\nFIRED";
		} else {
			GameObject.Find ("effects").GetComponent<AudioSource>().clip = GameObject.Find ("effects").GetComponent<sounds>().sfx[3];
			GameObject.Find ("effects").GetComponent<AudioSource>().Play();
			GameObject.Find ("labels").GetComponent<TextMesh> ().text = "GOOD\nJOB";
		}

		
		nbacktrack = new int[backAmount + 1];
		missReserve = new int[backAmount - 1];
		//Instantiate (startball, new Vector3 (((float)0.08), ((float)2.05), ((float)-0.23)), new Quaternion ());
		rhythmTool.NewSong (audioClip);
		beats = -1;
		steps = 0;
		lastFrame = 0;
		suc = 0;
		mis = 0;
		err = 0;
		forcestop = false;

		foreach(spawn S in beltSpawners)
		{
			S.active = true;
		}
		
		foreach(pass P in movers)
		{
			P.active = true;
		}

		GameButton.start = true;
        //GetComponent<reachscaler>().enabled = true;


	}
	// Update is called once per frame
	void Update ()
	{		

		for(int i = lastFrame+1; i < rhythmTool.CurrentFrame+1; i++)
		{
			int nextBeat = rhythmTool.NextBeatIndex();

			if(rhythmTool.CurrentFrame > nextBeat && rhythmTool.CurrentFrame > rhythmTool.TotalFrames/2 && !end)
			{
				GameObject.Find("success").GetComponent<TextMesh> ().text = "";
				GameObject.Find ("misses").GetComponent<TextMesh> ().text = "";
				GameObject.Find("errors").GetComponent<TextMesh> ().text = "";

				GameObject.Find("labels").GetComponent<TextMesh>().text = "finish!\nplease\nwait...";
				end = true;
			}
			if(rhythmTool.IsBeat(i, 0) == 1 || end)
			{

				if(beats%(StartVar.difficultylevel) == 0){


					//chance of bomb 
					int isBomb = Random.Range (0, 5);


					//randomly choose the ID of which ball will spawn
					int pickBall = Random.Range (0, 31);
					pickBall = pickBall % (3);

					//add oldest ID to reserve, add newest ID in it's place
					missReserve[steps%(backAmount-1)] = nbacktrack[steps%(backAmount+1)]; 
					nbacktrack[steps%(backAmount+1)] = pickBall+1;

					if(isBomb == 3)
					{
						pickBall += 3; 
					}

					//randomly choose which spawner creates the ball
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

					steps++;

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
		if(mis+err>=20)
		{
			print ("Stopping forcefully!!");
			rhythmTool.Stop ();
			GameObject.Find("success").GetComponent<TextMesh> ().text = "";
			GameObject.Find ("misses").GetComponent<TextMesh> ().text = "";
			GameObject.Find("errors").GetComponent<TextMesh> ().text = "";
			
			GameObject.Find("labels").GetComponent<TextMesh>().text = "finish!\nplease\nwait...";
			end = true;

			GameObject[] remaining;
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

			foreach(spawn S in beltSpawners)
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
