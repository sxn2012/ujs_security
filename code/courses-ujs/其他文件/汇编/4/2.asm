assume ds:data,cs:code
data segment
N db ?
result dw ?
data ends
code segment
start:
mov ax,data
mov ds,ax
mov ax,1
mov result,ax
mov ah,01h
int 21h
sub al,30h
mov N,al
mov ah,0
mov cx,ax
mov dl,0ah
mov ah,02h
int 21h
mov dl,0dh
mov ah,02h
int 21h
call calculate
mov cx,0
mov ax,result
nextout:
mov dx,0
mov bx,10
div bx
push dx
inc cx
cmp ax,0
JNE nextout
output:
pop dx
add dx,30h
mov ah,02h
int 21h
loop output


mov ax,4c00h
int 21h

calculate proc
mov ax,cx
cmp ax,0
JNE next
jmp finishp
next:
mov bx,result
mul bx
mov result,ax
dec cx
call calculate
jmp finishp
finishp:
ret
calculate endp

code ends
end start