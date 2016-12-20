;
; TextEngine.asm
;
; John Welter
; 2016
; 
; dynamic text engine for NES game production
;
; 
;

;necessary varaibles to put in main file:
;
;
;txtPtr		  .rs 2			;pointer to current byte in text
;txtChrCount	  .rs 1			;counter for printed characters in a line
;txtLnBrkOff	  .rs 1			;line break offset for premature line breaks 
;txtLinCount	  .rs 1			;counter for line count
;txtFrmCount	  .rs 1			;frame count between parses
;txtLoc		  .rs 2			;location of printhead 
;txtResetFlag  .rs 1			;flag for text box reset- if on, the box is currently resetting (set to 3, counts down)
;txtDisableFlag .rs 1		;flag for text disable- if on, text is not printed 	
;txtDisablePrepareFlag .rs 1
;txtCurrentTxt .rs 2			;Holds beggining address of current text block 
;txtTemp		  .rs 1			;temp value for text engine 	
;txtStart	  .rs 2			;holds value of beggining of text box 
;txtSpeed	  .rs 1			;speed of the text 
;txtDefaultSpeed .rs 1		;default speed to jump back to 
;txtMaxChr	  .rs 1			;max amount of characters in a box 
;txtMaxLin	  .rs 1			;max amount of lines in a box 
;txtInputTileLoc	.rs 2		;tile location for input wait tile 
;txtPauseFlag	.rs 1		;flag for pause- if on, text box is waiting for input 
;txtPrepareUnpause .rs 1		;prepares to reset pause flag once input is given 
;
;txtBoxWidth		.rs 1		;width of text box to be printed
;txtBoxHeight	.rs 1		;height of text box to be printed
;txtBoxLoc		.rs 2		;location of text box to be printed (top left corner)
;txtBoxTilePtr	.rs 2		;location to use in the box tiles
;txtBoxDrawFlag	.rs 1
;

;neccesary additional engines:
;
;	UtilEngine		;for setting the PPU
;



TxtProcess:

	LDA txtDisableFlag	;check if disabled
	CMP #$00			
	BNE TxtDefaultDone	;if so, skip all of this.

	LDA txtPauseFlag	;check if paused
	CMP #$00
	BEQ .notPaused		;if so, 
	
	LDA txtPrepareUnpause	;check if A was pressed to prepare unpause
	CMP #$01
	BNE .unpauseDone		;if so
  
	JSR TxtUnpause			;use this frame to unpause
	LDA #$00
	STA txtPrepareUnpause

.unpauseDone:	

	JMP TxtDefaultDone		;finish this frame
	

.notPaused:
	
	LDA txtResetFlag	;else if not paused, check if resetting
	CMP #$00
	BEQ .notResetting	;if so,
	
	JSR TxtReset		;do a frame of reset
	JMP TxtDefaultDone	;finish this frame
	
.notResetting:		

	LDA txtBoxDrawFlag	;check if drawing the box
	CMP #$00
	BEQ .notBoxDraw		;if not,
	
	JSR TxtBoxDraw		;work on drawing the box this frame
	JMP TxtDefaultDone	;finish this frame

.notBoxDraw:
	
	
	DEC txtFrmCount	;check if on a print frame	
	LDA txtFrmCount	
	CMP #$00
	BNE TxtDefaultDone	;if so, finish this frame
	
	JSR TxtParse	;if not, parse the next character

ResetFrame:

	LDA txtFrmCount	;reset the frame count
	CLC
	ADC txtSpeed	;by adding the text speed
	STA txtFrmCount
	
TxtDefaultDone:
	
	RTS				;finish the frame


;--------------------------------------

TxtEnable:

	;enables text

	LDA #$00
	STA txtDisableFlag
	RTS
	
;--------------------------------------

TxtFullEnable:

	LDA #$00				;enable text
	STA txtDisableFlag		
	JSR TxtPrepareBoxDraw	;prepare to draw the box next frame
	JSR TxtSetTextToBox		;set text parameters to fit the box
	RTS

;--------------------------------------

TxtDisable:

	;disables text

	LDA #$01
	STA txtDisableFlag
	RTS
;--------------------------------------

TxtFullDisable:

	
	LDA #$01					;disable text
	STA txtDisablePrepareFlag	
	JSR PrepareReset			;prepare to reset the text
	LDA txtCurrentTxt+1 		;reset current text
	LDX txtCurrentTxt
	JSR TxtLoad					
	RTS							
	
;--------------------------------------
;--------------------------------------

TxtPause:

	;
	; input- A = input wait tile
	; 
	; set PPU input location from Input Tile Location variable
	;
	; print tile at location
	;
	; set pause flag 

	TAY
	
	LDA txtInputTileLoc+1	;set PPU input location to printhead
	LDX txtInputTileLoc
	JSR SetPPU
	
	TYA
	STA $2007
	
	LDA #$01
	STA txtPauseFlag
	RTS
	
;--------------------------------------

TxtUnpause:

	; set PPU input location from Input Tile Location variable
	;
	; resets input tile location to it's original form (from #TXTNORMALTILE)
	;
	; resets pause flag

	
	LDA txtInputTileLoc+1	;set PPU input location to printhead
	LDX txtInputTileLoc
	JSR SetPPU
	
	LDA #TXTNORMALTILE
	STA $2007

	LDA #$00
	STA txtPauseFlag
	RTS
	
;--------------------------------------

TxtLoad:

	;
	; input- A = high byte of text data, X = low byte of text data
	; sets text pointer and current text variables to passed in values
	;
	; sets text speed to default
	; resets frame count

	STA txtPtr+1
	STA txtCurrentTxt+1
	STX txtPtr
	STX txtCurrentTxt

	LDA txtDefaultSpeed
	JSR TxtSetSpeed
	STA txtFrmCount

	RTS
	
	
;--------------------------------------
TxtSetStart:	;sets start of text box to passed in values	
	
	STA txtStart+1	;input A = high byte of location
	STX txtStart	;input X = low byte of location
	RTS

TxtSetMaxLin:	; sets max line count

	STA txtMaxLin 	; input A = max line count
	RTS

TxtSetMaxChr:	; sets max character count

	STA txtMaxChr	; input A = max character count
	RTS
	
TxtSetLoc:		; sets printhead location

	STA txtLoc+1	; input A = high byte of printhead location
	STX txtLoc		; input X = low byte of printhead location
	RTS
	
TxtSetSpeed:	;sets speed of text

	CMP #$00			; if the input is 0
	BNE .nonDefault		
	LDA txtDefaultSpeed	;	reset speed to default

.nonDefault:	;else set the speed

	STA txtSpeed	; input A = speed to change to
	RTS
	
TxtSetInputTileLoc:	; sets input tile location

	STA txtInputTileLoc+1	; input A = high byte of input tile location
	STX txtInputTileLoc		; input X = low byte of input tile location
	RTS
	
;--------------------------------------	

TxtParse:

	;;read charfrom data
	;;if char, load to print
	;;else, do one of the other commands
	
	LDY #$00
	
	
	LDA [txtPtr], y			;get byte from text pointer
	CMP #$30				;if below 0x30, it's a character
	BCC .print				;	jump to print
	

.opcode:					;else, it's an opcode

	CMP #$FF				;FF - end of text data
	BEQ .close
	CMP #$FE				;FE - wait for input 
	BEQ .wait
	CMP #$FD				;FD - loop from beginning
	BEQ .loop
	CMP #$FC				;FC - reset text box
	BEQ .reset
	CMP #$FB				;FB - line break
	BEQ .lnBreak
	CMP #$FA				;FA - insert space
	BEQ .space
	CMP #$F9				;F9 - set speed
	BEQ .speed
	CMP #$F8				;F8 - pause the text for extra frames (adds to frame count one time)
	BEQ .pause
	JMP .update_pointer		;else, update the pointer

.print:

	; prints character to screen
	; incriments printhead
	; incriments character count
	; update pointer
	
	JSR TxtPrint
	JSR TxtNext
	;INC txtChrCount
	JSR TxtIncChrCount
	JMP .update_pointer
	
.loop:
	
	; reset text block
	; reload current text
	; finish
	
	JSR PrepareReset
	LDA txtCurrentTxt+1
	LDX txtCurrentTxt
	JSR TxtLoad
	JMP .end
	
.wait
	
	; pause to wait for input
	; update pointer
	
	LDA #TXTPAUSETILE	;load tile to indicate input wait
	JSR TxtPause
	JMP .update_pointer
	
.close:	
	
	; disable text 
	; finish
	
	JSR TxtFullDisable
	JMP .end
	
.space:

	; increment pointer to next byte
	; insert space
	; increment the character count
	; if not paused
	; 	parse again
	; else
	; 	finish
	
	JSR TxtIncPtr
	JSR TxtSpace
	JSR TxtIncChrCount
	LDA txtPauseFlag	;;check if pause flag was set during IncChrCount
	CMP #$01			
	BEQ .end			;;if so, skip second parse
	JSR TxtParse
	JMP .end
	
.lnBreak:

	; increment pointer to next byte
	; break line
	; parse again

	JSR TxtIncPtr
	JSR LineBreak
	
	LDA txtResetFlag
	BNE .lnBreakDone	;skip the parse if the line break causes a reset
	JSR TxtParse
	
.lnBreakDone:
	JMP .end
	
.reset:

	; prepare reset
	; update pointer
	
	JSR PrepareReset
	JMP .update_pointer

.speed:

	; increment pointer to next byte
	; read byte for new speed
	; set the speed
	; increment pointer again
	; reparse
	; finish

	JSR TxtIncPtr
	LDA [txtPtr], y
	JSR TxtSetSpeed
	JSR TxtIncPtr
	JSR TxtParse
	JMP .end

.pause

	; increment pointer to next byte
	; read byte for pause frames
	; add to frame count 
	
	JSR TxtIncPtr
	LDA [txtPtr], y
	STA txtTemp			; txtTemp used to hold addable value needed whenever
	LDA txtFrmCount		; adds extra frames to frame count
	CLC
	ADC txtTemp
	STA txtFrmCount

.update_pointer:

	; increment pointer to next byte
	
	JSR TxtIncPtr

.end:

	; finish
	
	RTS
	
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;;Increments for Character count and Line count

TxtIncChrCount:

	; Increments character count from characters and spaces

	INC txtChrCount		; increment character count
	LDA txtChrCount		
	CMP txtMaxChr		; if not equal to max characters, 
	BNE IncChrDone		; 	finish
						; else
	JSR LineBreak		; break line

IncChrDone:

	RTS					; finish
		
TxtIncLinCount:

	INC txtLinCount		; increment line count
	LDA #$00				; set character count to 0
	STA txtChrCount
	
	LDA txtLinCount		
	CMP txtMaxLin		; if line count is not maxed out,
	BNE IncLinDone		; 	finish
						; else
	JSR PrepareReset	; prepare to reset block
	LDA #TXTPAUSETILE
	JSR TxtPause		;wait for input

IncLinDone:

	RTS
	
;--------------------------------------
TxtPrint:

	;;print current char in txtPtr
	
	TAY
	
	LDA txtLoc+1	;set PPU input location to printhead
	LDX txtLoc
	JSR SetPPU
	
	TYA				; print tile at the location
	STA $2007
	
	RTS
	
;--------------------------------------
TxtNext:

	;;increments screen location of printing
	
	INC txtLoc
	LDA txtLoc
	CMP #$00		; if the low byte flips over,
	BNE NextDone	
	INC txtLoc+1	; increment the high byte
	
NextDone:

	RTS
	
TxtSpace:

   ;; insert a space by incrementing the printhead an extra space
   JSR TxtNext
   RTS
  

LineBreak:
	
	; subtract current offest from #$40, store into text linebreak offset 
	LDA #$40
	SEC
	SBC txtChrCount ;current offset = 0x40 - character count
	STA txtLnBrkOff
	
	; add offset to printhead location
	LDA txtLoc
	CLC
	ADC txtLnBrkOff
	STA txtLoc
	LDA txtLoc+1
	ADC #$00
	STA txtLoc+1
	
	JSR TxtIncLinCount ; increment line count
	
	RTS
	
LineBreakHead:

	;; used in reset- reset blacks out a line starting from the printhead without 
	;; changing the printhead, so a different line break is necessary to get it to the next line.
	;;[CURRENT]: double spaced, same X offset as head of last line
	;;LDA #$40
	CLC
	ADC txtLoc
	STA txtLoc
	LDA txtLoc+1
	ADC #$00
	STA txtLoc+1
	
	RTS

;-------------------------------------

TxtIncPtr:

	INC txtPtr		; increment pointer to next byte
	LDA txtPtr
	CMP #$00
	BNE .end
	INC txtPtr+1	;if low byte flips over, increment high byte
.end:
	RTS
	
;--------------------------------------
PrepareReset:
	
	LDA txtStart+1		;sets printhead to beggining of text box
	STA txtLoc+1
	LDA txtStart
	STA txtLoc
	
	;;LDA #$03			;sets reset flag to max line amount to be decremented
	LDA txtMaxLin
	STA txtResetFlag
	RTS

;--------------------------------------
TxtReset:

	
	LDA txtLoc+1	;set PPU input location to printhead
	LDX txtLoc
	JSR SetPPU
	
	LDX #$00		

TxtResetLoop:

	LDA #$53			;load text box background tile, TXTBOXC
	STA $2007			;print to PPU location
	INX				
	CPX txtMaxChr		;if X does not equal the max character count for the line, 
	BNE TxtResetLoop	;loop to continuously print background tiles (automatically increments PPU location)
	
	LDA #$40
	JSR LineBreakHead 	;else move print head down 
	
	DEC txtResetFlag  	;decremnent reset flag
	LDA txtResetFlag  	;if the flag is not 0, finish for this frame and continue the next
	CMP #$00
	BNE ResDone
						;else
ResTop:	

	LDA txtDisablePrepareFlag ;if prepared for full disable
	BEQ .notFullDisable			
	
	JSR TxtPrepareBoxDraw	  ;prepare box for deletion
	JMP ResDone				  ;finisherup

.notFullDisable:
						
	JSR TxtResetTextHead	;if it's not prepared for full disable, put the text head back where it was
	
	
ResDone:

	RTS
	
;--------------------------------------------



;;BOX DRAW FUNCTIONS

;;start with corner tile
;;

;;

TxtSetBoxDimensions:

	;;A = Width 
	;;X = Height
	
	STA txtBoxWidth
	STX txtBoxHeight

	;;set new text to box as separate function...? yes, for space sake. but call it here for sure
	;;JSR TxtSetTextToBox
	
	RTS

TxtSetBoxLocation: 

	;;A = loc+1
	;;X = loc
	
	STA txtBoxLoc+1
	STX txtBoxLoc
	
	RTS
	
TxtSetTextToBox: ;; only call if text is currently disabled


	
	LDA txtBoxLoc	;set text location from box top corner
	CLC
	ADC #$43
	STA txtStart
	
	LDA txtBoxLoc+1	;add any leftovers
	ADC #$00
	STA txtStart+1
	
	
	;set the location of the wait for input tile

	LDA txtBoxLoc	
	STA txtInputTileLoc
	LDA txtBoxLoc+1
	STA txtInputTileLoc+1
	
	LDX #$02			;sub 2 from height
	
.loop:

	LDA txtInputTileLoc	;loop adding 32 each time up to height-2 times
	CLC
	ADC #$20
	STA txtInputTileLoc
	LDA txtInputTileLoc+1
	ADC #$00
	STA txtInputTileLoc+1
	
	INX
	CPX txtBoxHeight
	BNE .loop
	
	LDA txtInputTileLoc ;add length of box -3
	CLC
	ADC txtBoxWidth
	STA txtInputTileLoc
	
	LDA txtInputTileLoc
	SEC
	SBC #$03
	STA txtInputTileLoc
	
	
	LDA txtBoxWidth		;subtract 6 from the width to set the max character per line
	SEC
	SBC #$06
	STA txtMaxChr
	
	
	LDA txtBoxHeight	;do some dividey stuff to get the max lines per box
	SEC
	SBC #$02
	STA txtMaxLin
	LDA txtMaxLin
	LSR txtMaxLin

	RTS
	
TxtPrepareBoxDraw:

	LDA txtBoxLoc+1		;sets printhead to top corner of text box
	STA txtLoc+1
	LDA txtBoxLoc
	STA txtLoc

	LDA txtBoxHeight		;set boxHeightFlag to height of box
	STA txtBoxDrawFlag	
	
	RTS
	
TxtBoxDraw:

	LDA txtLoc+1	;set PPU input location to printhead
	LDX txtLoc
	JSR SetPPU
	
	LDY #$00

	LDA #HIGH(TxtBoxTiles)	;set box tile pointer to body tiles of the box tiles
	STA txtBoxTilePtr+1
	LDA #LOW(TxtBoxTiles)
	STA txtBoxTilePtr

	LDA txtDisablePrepareFlag	;if it's disabling, we're going to erase
	BEQ .notErasing
	
	LDA txtBoxTilePtr			;so we set the box tile pointers to the normal tiles (placeholder for replacement stuff)
	CLC
	ADC #$09					
	STA txtBoxTilePtr
	LDA txtBoxTilePtr+1
	ADC #$00
	STA txtBoxTilePtr+1
	JMP .initDraw				;start drawing
	
.notErasing
	
	LDA txtBoxDrawFlag		;check the drawflag
	CMP txtBoxHeight		;if it's the same as the height, 
	BNE .notTop
	
	LDA txtBoxTilePtr		;set the box tile pointer for the top row of tiles
	CLC
	ADC #$03
	STA txtBoxTilePtr
	LDA txtBoxTilePtr+1
	ADC #$00
	STA txtBoxTilePtr+1

	JMP .initDraw			;start drawing
	
.notTop:

	CMP #$01				;if it's equal to one, 
	BNE .initDraw			
	
	LDA txtBoxTilePtr		;set box tiles to the bottom row of tiles
	CLC
	ADC #$06
	STA txtBoxTilePtr
	LDA txtBoxTilePtr+1
	ADC #$00
	STA txtBoxTilePtr+1

.initDraw:				
	
	LDA [txtBoxTilePtr], y	;draw left tile
	STA $2007 
	LDX #$02
	INY
	LDA [txtBoxTilePtr], y

.loop:

	STA $2007			;print to PPU location (middle tile)
	INX				
	CPX txtBoxWidth
	BNE .loop			;loop to continuously print background tiles (automatically increments PPU location)
	
	
	INY
	LDA [txtBoxTilePtr], y	;print right tile
	STA $2007
	
	LDA #$20
	JSR LineBreakHead	;else move print head down 
	DEC txtBoxDrawFlag	 	;decremnent reset flag
	LDA txtBoxDrawFlag 		;if the flag is not 0, finish for this frame and continue the next
	BNE .boxDone
	
.finish:
	
	JSR TxtResetTextHead
	
	LDA txtDisablePrepareFlag ;if if prepared for disable
	CMP #$00
	BEQ .boxDone
	
	STA txtDisableFlag		;disable the text engine
	LDA #$00				
	STA txtDisablePrepareFlag ;reset the preparation flag
	
.boxDone:
	RTS
	
TxtResetTextHead:

	LDA txtStart+1	  	;set printhead back to the top
	STA txtLoc+1
	LDA txtStart
	STA txtLoc
	
	LDA #$00		  	;reset character count
	STA txtChrCount
	LDA #$00		  	;reset line count
	STA txtLinCount
	RTS



	
TxtBoxTiles:

	.db $33, TXTNORMALTILE,   $34
	.db $30, $31, $32
	.db $35, $36, $37
	.db TXTNORMALTILE, TXTNORMALTILE, TXTNORMALTILE ;; only for easy use version of erasing box
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	


	