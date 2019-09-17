data segment
	space db ' '
	enter db 0AH,0DH
	N db ?
	M db ?
data ends
code segment
assume ds:data,cs:code
start:
	mov ax,data
	mov ds,ax
	mov AH,01H
	int 21H
	mov N,AL
	mov CL,N
	mov M,cL
	mov CH,0
	mov BL,0
outputline:
	inc bl
	sub M,bl
	add M,1
	add bl,30H
	mov al,0
outputchar:
	mov dl,bl
	mov ah,02H
	int 21H
	mov dl,space
	mov ah,02h
	int 21H
	inc al
	cmp al,M
	JNE outputchar
outputdecrease:
	dec bl
	cmp bl,0
	JE next
	mov dl,bl
	mov ah,02H
	int 21H
	mov dl,space
	mov ah,02h
	int 21H
	JMP outputdecrease
next:
	mov dl,enter
	mov ah,02h
	int 21h
	mov dl,enter+1
	mov ah,02h
	int 21h
	loop outputline
	mov cx,4c00h
	int 21h
end start
code ends

