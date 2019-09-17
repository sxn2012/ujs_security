data segment
pass db 15 dup(?)
text db 15,?,15 dup(?)
data ends
assume ds:data, cs:code
code segment
start:
        mov ax,data
        mov ds,ax
        lea dx,text
        mov ah,0ah
        int 21h
        mov cl,text+1
        mov ch,0
        mov si,offset text
        add si,2
        mov di,offset pass
one:
        mov ax,[si]
        add ax,11h
        mov [di],ax
        inc si
        inc di
        loop one
        mov ax,24h
        mov [di],ax
        inc di
        lea dx,pass
        mov ah,09h
        int 21h
        mov ax,4c00h
        int 21h
code ends
end start
