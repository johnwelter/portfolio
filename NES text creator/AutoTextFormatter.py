#auto text formatter
import sys

pathName = sys.argv[1]
print (pathName)
inFile = open(pathName, 'r')
fileCopy = inFile.read().lower()

fileCopy = fileCopy.replace("“", "\"")
fileCopy = fileCopy.replace("”", "\"")
fileCopy = fileCopy.replace("‘", "'")
fileCopy = fileCopy.replace("’", "'")
fileCopy = fileCopy.replace("\n", "")

print("\ninput text:\n\n"+fileCopy + '\n\n')

inFile.close()

try: #turn off puctuation check
	
	noPunctuationCheck = (sys.argv[2])
	
except IndexError:
	
	noPunctuationCheck = ""


MaxCharCount = int(input("Input max amount of characters per line: "))
print("\n")
MaxLinCount = int(input("Input max amount of lines per box: "))
print("\n")

newFile = ""
currentWord = ""
charCount = 0 
linCount = 0
commandOffest = 0

def lineBreak():
	global newFile
	global linCount
	newFile += '\n'
	linCount += 1
	if linCount == MaxLinCount:
		newFile += '\n\n'
		linCount = 0
	
	return linCount

for char in fileCopy:
	
	
	if char != ' ':
		
		currentWord += char
		if char == ',':
		
			currentWord += "P010"
			commandOffest += 4
			
		elif (char == '.' or char == '?' or char == '!') and noPunctuationCheck != '-p':
		
			currentWord += "P015"
			commandOffest += 4

	else:
	
		if charCount < MaxCharCount:
			
			newFile += currentWord+char
			if charCount == MaxCharCount-1:
			
				lineBreak()
				charCount = -1
				
			commandOffest = 0
			currentWord = ""
		elif charCount == MaxCharCount:
			
			newFile += currentWord
			lineBreak()
			commandOffest = 0
			currentWord = ""
			charCount = -1
			
		elif charCount > MaxCharCount:
			newFile = newFile[:-1] + 'N'
			lineBreak()
			newFile += currentWord+char
			charCount = len(currentWord)-(commandOffest)
			commandOffest = 0
			currentWord = ""
	charCount += 1

if charCount < MaxCharCount:

	newFile += currentWord+'W'
elif charCount == MaxCharCount:
	
	newFile += currentWord
elif charCount > MaxCharCount:

	newFile = newFile[:-1] + 'N'
	newFile += '\n'
	lineBreak()
	newFile += currentWord+'W'


print("preview:\n\n"+newFile + '\n\n')

newFile = newFile.replace('\n', '')

print("output:\n\n"+newFile + '\n')

import os

filename, file_extention = os.path.splitext(pathName)

outFile = open((filename + '_processed' + file_extention), 'w')
outFile.write(newFile)
outFile.close()


os.system('pause')
