package programs.quaternion;


import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

import dataStructs.Quaternion;
import dataStructs.Vector3D;

/*

	quatrot
	
	driver to rotate a point around an arbitrary vector. 
	takes input file as argument, or manual input if one is incomplete 
	or not provided.

*/

public class quatrot {
	
	public static Double[] tempVector = {0.0, 0.0, 0.0}; 	//temp vector to hold parsed values
	public static String[] secondaryArgs = new String[3];	//secondary args taken from input file
	public static String temp;								//temporary string to parse for values
	public static char currentChar;							//current charater in input string 
	public static int track = 0;							//tracker for input parsing loop
	public static String input;								//input to parse
	
	public static void main(String args[]){
		
		if(args.length != 0)	//if there is a filename in the command line
		{
			try{
				parseSecondaryArgs(args[0]);	//parse for secondary arguments
			}catch(FileNotFoundException e)
			{
				System.out.println("file not found. manual input engaged.");	//else, go manual.
			}
		}
		
		Quaternion test = new Quaternion(0.0, new Vector3D(0.0, 0.0, 0.0));	//create new quaternion
		Vector3D point = new Vector3D(0.0, 0.0, 0.0);						//create new vector3D
		
		if(secondaryArgs[0] != null){		//if there's a quaternion string already prepared
			input = secondaryArgs[0];		//set is as input
		}
		else{								//else, do it manually.
			System.out.print("input Quaternion vector in format 'x.x, y.y, z.z':");
			input = System.console().readLine();
		}
		
		parseInput();						//parse the input, put the values in the temporary vector

		test.vector.x = tempVector[0];		//set quaternion's vector to the temp vector
		test.vector.y = tempVector[1];
		test.vector.z = tempVector[2];
	
		if(secondaryArgs[1] != null)		// if there's a point to rotate around
			input = secondaryArgs[1];		//set it as input
		else{								//else, do it manually.
			System.out.print("input point to rotate around in format 'x.x, y.y, z.z':");
			input = System.console().readLine();
		}
		
		parseInput();						//parse the input, put the values in the temporary vector
		
		point.x = tempVector[0];			//set point to the temp vector
		point.y = tempVector[1];
		point.z = tempVector[2];
		
		if(secondaryArgs[2] != null)		//if there's already an angle prepared
			input = secondaryArgs[2];		//set it as input
		else{								//else, manually
			System.out.print("input angle in format 't.t':");	
			input = System.console().readLine();
		}
		double theta = Double.parseDouble(input);	//parse the value into theta

		
		Vector3D newPoint = test.rotate(theta, point);	//rotate the quaternion and return the new point
		
		System.out.println("the new point is " +  newPoint.toString());

	}
	
	
	/*
	
		parseInput
		
		parses a string in the format 'x.x, y.y, z.z' into three doubles and inserts them in a temporary
		vector.
	
	
	*/
	public static void parseInput()
	{
	
		track = 0;					
		
		for(int i = 0; i < 3; i++)
		{
			temp = "";
			currentChar = input.charAt(track);
			// if we havent reached a comma or the end of the line, build up the value.
			while(currentChar != ',' && track != input.length())	
			{
				if(currentChar != ' ')	//ignore spaces, add everything else to the current temp string
					temp = temp + currentChar;
				track++;
				if(track < input.length()) // if we're not at the end, set the current character to the next character
					currentChar = input.charAt(track);
			}
			tempVector[i] = Double.parseDouble(temp);	//parse the double and add to the ith place in the vector
			track++;

		}
	}
	
	/*
	
		parseSecondaryArgs
		
		parses through input text data line by line to create secondary arguments,
		hopefully in this order:
		
		quaternion's vector
		point/vector to rotate around
		angle
		
	
	*/
	public static void parseSecondaryArgs(String path) throws FileNotFoundException
	{

        File text = new File(path);
      
        Scanner scnr = new Scanner(text);
      
        //Reading each line of file using Scanner class
        int lineNumber = 0;
        while(scnr.hasNextLine()){
        	
            secondaryArgs[lineNumber] = scnr.nextLine();
            System.out.println(secondaryArgs[lineNumber]);
            lineNumber++;
        }    
        
        scnr.close();
		
	}
	

}
