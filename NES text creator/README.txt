Rocky NES text tool and engine
---------------------------------------------------------------------------------------------
;;;;;;;;;;;;
;EDITOR USE;
;;;;;;;;;;;;

	opening the text editor creates a new text file. press "Add" under the list 
	window to create a new block of text data. text data consists of a label and 
	a string of text. 

	Labels and strings are limited in input. labels can only contain numbers, letters, and the character '_'. 
	strings can contain the text characters availible in the first three lines of the text.chr file:

	0123456789abcdef
	ghijklmnopqrstuv
	wxyz.?!,:'"%~$-*

	some capital letters are acceptable for opcodes:

	
	'N' - line break
	'R' - reset text box
	'L' - loop text from beggining
	'W' - wait for input (resets box after)
	' ' - a space is also an opcode that the engine will recongnize. 
	
	There are also "additive" codes which require a certain amount of digits after the code is called to export correctly.
	
	'SXXX' - sets the over all speed of the text to XXX frames.
	'PXXX' - adds XXX to the frames needed to print the next character, creating a pause.

	currently, you can input values up to 999, but it wil be converted to one bye in HEX, so values over 255 will just flip over.

	when you've made all the text data's you want, you can save the data as an 
	XML for later use (with the Save option), or as an .i or .asm file for the engine
	(with the Export option).

	the .i/.asm data can be ".include"-ed in your game's main asm file.


---------------------------------------------------------------------------------------------
;;;;;;;;;;;;;;;;;;;;;;;;;
;THE AUTO TEXT FORMATTER;
;;;;;;;;;;;;;;;;;;;;;;;;;

this handy little python script was made after spending an entire day trying to format the internet tough guy copy pasta. Such a task is incredibly tedious (add line breaks, test, add line breaks, test a little further, add line breaks, test a little further, etc etc), so the script makes the work exponentially quicker. 

you can type a short snippit of dialouge, or even an entire speech in to a txt file, throw it into the script, give the dimensions in terms of max character count per line and max line count, and you'll get another txt file with text you can copy directly into the text editor. it not only auto formats line breaks, but also adds pauses after punctuation. after this starting point, it's easy to go in and finess the text as you please!

for an example, here's some input:

Hello? Is there any body in there? Just nod if you can hear me... Is there anyone at home? Come on, now, I hear you're feeling down. Well I can ease your pain, get you on your feet again. Relax, I'll need some information first. Just the basic facts... Can you show me where it hurts?

and the resulting output txt file with a max character count of 16 and a max line count of 3:

hello?P015 is there any body inNthere?P015 just nod if you can hear me.P015.P015.P015 is thereNanyone at home?P015 come on,P010 now,P010 i hear you'reNfeeling down.P015Nwell i can ease your pain,P010 getNyou on your feetagain.P015 relax,P010Ni'll need someNinformationNfirst.P015 just the basic facts.P015.P015.P015Ncan you show me where it hurts?P015W

USE: just drag the file you want converted into the script, and it will run automatically. 
TIP: don't want it to automatically format punctuation! easy! if you run the program from the command line, you can include "-p" after the path of the file to be processed and it will skip all the code for pauses at punctuation.
 
---------------------------------------------------------------------------------------------

This is still most definitely a work in progress. Please send any and all feedback to:

johnwelter@me.com

Thanks!

	-John Welter





