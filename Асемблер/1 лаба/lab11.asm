DATA SEGMENT
 mas1 dw -5,7,-2,3,-8,9,-4,5,-6,4 	;исходный массив
 mas2 dw 10 dup(0)			;полученный массив
DATA ENDS

STCK SEGMENT STACK
 db 256 DUP(?)
STCK ENDS

CODE SEGMENT 'code'
ASSUME CS:CODE,DS:DATA

start:
	mov AX,DATA
	mov DS,AX	
	
	xor BX,BX			;обнуление регистров
	xor SI,SI
	
	
	
	mov AX,mas1[0]
	mov CX,mas1[18]
	mov DI, offset mas2  

sec_mas:
	mov DX,mas1[SI]
	cmp DX,AX 
	jge  hi_check
	mov [DI],DX
	add DI,2
hi_check:
	cmp DX,CX
	jle skip_one
	mov [DI],DX
	add DI,2
skip_one:	
	add SI,2                       
	cmp SI,20 
	jne sec_mas
	
	
	
	
	
	
	
	xor SI,SI
frst_sort:              ;отрицательные в стэк
	mov AX,mas1[SI]
	cmp AX,0 
	jge skip_push
	push AX 
skip_push:
	add SI,2                       
	cmp SI,20                      
	jne frst_sort

xor SI,SI
	
sec_sort:					   ;положительные и нули в стэк
	mov AX,mas1[SI]                
	cmp AX,0                       
	jl  skip_push2                   
	push AX 	
skip_push2:	
	add SI,2                       
	cmp SI,20                      
	jne sec_sort
	
	
	mov SI,18 
sort1:                             ;значения из стэка в массив
	pop AX
	mov mas1[SI],AX 
	sub SI,2
	CMP SI,-2
	jne sort1
	
	xor SI,SI	
		
sort2:                              ;сортируем отрицательные по возрастанию
	mov AX,mas1[SI]
	mov CX,mas1[SI+2]
	cmp AX,0
	jge skip_it
	cmp CX,AX
	jg skip_it
	mov DX,AX
	mov mas1[SI],CX
	mov mas1[SI+2],DX
	xor SI,SI
	jmp sort2
	
skip_it:
	add SI,2                       
	cmp SI,20 
	jne sort2
	
	

		
	mov AX,4C00h			;int для выхода из программы
	int 21h 
		
CODE ENDS
END start


	