assume ds:data,cs:code
data segment
N db ?
sum dw ?
data ends
code segment
start:
mov ax,data
mov ds,ax
mov ah,01h
int 21h
sub al,30h
mov N,al

mov dl,0dh
mov ah,02h
int 21h
mov dl,0ah
mov ah,02h
int 21h

mov bl,N
mov bh,0
push bx
call func
mov bx,dx
mov sum,bx
mov dx,0
mov ax,bx
mov cx,0
next:
inc cx
mov bx,10
div bx
push dx
mov bx,dx
cmp ax,0
JNE next
l2:
pop bx
add bx,30h
mov dl,bl
mov ah,02h
int 21h
loop l2
mov ax,4c00h
int 21h

func proc
mov bp,sp
add bp,2
mov cx,[bp]
mov ax,0
mov dx,0
l1:
add ax,2
add dx,ax
loop l1
ret
func endp
code ends
end start	