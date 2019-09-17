DATAS SEGMENT
	N db ?
	
	i dw ?
DATAS ENDS

STACKS SEGMENT
   
STACKS ENDS

CODES SEGMENT
    ASSUME CS:CODES,DS:DATAS,SS:STACKS
START:
    MOV AX,DATAS
    MOV DS,AX
    
    mov AH,01H
	int 21H
	sub al,30h
	mov N,AL
	mov dl,0ah
	mov ah,02h
	int 21h
	mov dl,0dh
	mov ah,02h
	int 21h
	mov bx,0000h
argtest:
	mov al,N
	mov ah,0
	push ax
	inc bx
	push bx
	jmp prtline
nextline:
	mov bx,i
	cmp bl,N
	JNE argtest
	jmp finish
prtline:
	pop bx
	mov i,bx	
	pop cx
	sub cx,i
	inc cx
printword:
	mov dx,i
	add dx,30h
	mov dh,0
	mov ah,02h
	int 21h
	mov dl,20h
	mov ah,02h
	int 21h
	loop printword
	mov ax,i
	push ax
nextword:
	pop ax
	dec al
	push ax
	JZ finishline
	mov dl,al
	add dl,30h
	mov ah,02h
	int 21h
	mov dl,20h
	mov ah,02h
	int 21h
	jmp nextword
finishline:
	pop ax
	mov dl,0ah
	mov ah,02h
	int 21h
	mov dl,0dh
	mov ah,02h
	int 21h
	jmp nextline
	
	
	
	
finish:	
    MOV AH,4CH
    INT 21H
CODES ENDS
    END START