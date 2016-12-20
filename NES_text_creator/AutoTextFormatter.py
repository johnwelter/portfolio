#
#
#
#	auto text formatter
#
#	John Welter 2016
#
#	auto formats a txt file so it's text can be copy pasted into the Rocky NES Text Editor 
#	witout having to manually put in all the line breaks and punctuation pauses. 
#
#
import sys

pathName = sys.argv[1] #set path of txt to be read
print (pathName)
inFile = open(pathName, 'r')
fileCopy = inFile.read().lower() #read in all text to a copy in lowercase

fileCopy = fileCopy.replace("“", "\"")	#replace all unusable quotation marks
fileCopy = fileCopy.replace("”", "\"")
fileCopy = fileCopy.replace("‘", "'")
fileCopy = fileCopy.replace("’", "'")
fileCopy = fileCopy.replace("\n", "")	#as well as removing the line breaks, if there are any

print("\ninput text:\n\n"+fileCopy + '\n\n')

inFile.close() 	#close the infile- we're using a copy from here out

try: #turn off puctuation check (command after text file, looking for '-p')
	
	noPunctuationCheck = (sys.argv[2])
	
except IndexError:
	
	noPunctuationCheck = ""


MaxCharCount = int(input("Input max amount of characters per line: "))
print("\n")
MaxLinCount = int(input("Input max amount of lines per box: "))
print("\n")

newFile = ""		#full text of formatted file
currentWord = ""	#current scanned word
charCount = 0 		#current character count
linCount = 0		#current line count
commandOffest = 0	#offset for pause commands


#///////////////
#
#	lineBreak
#
#	temporarily adds a line break to the newFile where a linebreak command was added
#	or a new line was found. if we've paased the line count, add another two to signify
#	the end of a "box"
#
def lineBreak():	
	global newFile
	global linCount
	newFile += '\n'
	linCount += 1
	if linCount == MaxLinCount:
		newFile += '\n\n'
		linCount = 0
	
	return linCount
	
#////////////////
#
#	parsing routine
#
#	parses each word to decide if a line break is natural or needs to be added to  	
#	avoid word wrapping. if punctuation is found, add a pause command unless 
#	the punctuation check was disabled
#
#

for char in fileCopy:
	
	
	if char != ' ':	#unless we reach a space, we build up the current word.
		
		currentWord += char #add to the current word
		
		#if the current character is also a comma, add a command for a 10 frame pause
		if (char == ',') and noPunctuationCheck != '-p':		
		
			currentWord += "P010"
			commandOffest += 4
			
		#if the current character is a ., ?, or !, add a 15 frame pause
		elif (char == '.' or char == '?' or char == '!') and noPunctuationCheck != '-p':	
		
			currentWord += "P015"
			commandOffest += 4

	else:	#if we've reached a space, we've reached the end of a word. check for breaks
	
		if charCount < MaxCharCount:		#if we haven't reached the max character count for a line, 
			
			newFile += currentWord+char		#add the current word + the space
			if charCount == MaxCharCount-1:	#if we're right at the end of the line anyway
			
				lineBreak()					#add a temporary line break to the newFile
				charCount = -1				#reset character count
				
			commandOffest = 0				#reset the command offset and current word
			currentWord = ""
		elif charCount == MaxCharCount:		#if the space itself would start a new line
			
			newFile += currentWord			#add the current word without the space		
			lineBreak()						#add the temporary line break
			commandOffest = 0				#reset commandOffest and current word
			currentWord = ""				
			charCount = -1					#reset character count
			
		elif charCount > MaxCharCount:		#if the word would wrap 
			newFile = newFile[:-1] + 'N'	#tack an new line command to the newFile, replacing the last space
			lineBreak()						#add the temporary break 
			newFile += currentWord+char		#add the current word to the newFile with the space
			
			#subtract the command offset and current word length to get the new character count 
			#to account for the word wrapping over
			charCount = len(currentWord)-(commandOffest)	
			commandOffest = 0				#reset the command offset and current word
			currentWord = ""
	charCount += 1							#increment character count

# take care of the last word 
if charCount < MaxCharCount:		#if the last word is in the line bounds,

	newFile += currentWord+'W'		#add the last word with a wait command
elif charCount == MaxCharCount:		#if the last word is right at the bounds 
					
	newFile += currentWord			#add the last word
elif charCount > MaxCharCount:		#if the last word wraps

	newFile = newFile[:-1] + 'N'	#tack on a new line command to replace the last space
	newFile += '\n'					#add a temp line break (did I forget to take that out?)
	lineBreak()
	newFile += currentWord+'W'		#add a wait command


print("preview:\n\n"+newFile + '\n\n')	#show new file contents

newFile = newFile.replace('\n', '')		#take away all temporary line breaks

print("output:\n\n"+newFile + '\n')		#print the final output

import os

filename, file_extention = os.path.splitext(pathName)	#save file as new txt file with "_processed" added to the name

outFile = open((filename + '_processed' + file_extention), 'w')
outFile.write(newFile)
outFile.close()


os.system('pause')
