.model small	
.stack
.386

; макросы

mPrintSymbol MACRO symbol:REQ
    mov ah,2
    mov dl,symbol
    int 21h
ENDM

mPrintInt32 MACRO int32:REQ
    mov  eax,int32
    call PrintInt32
ENDM

mPrintArray MACRO array:REQ, size:REQ, sep:=<','>
    mov  cx,size
    mov  si,OFFSET array
    mov  dl,sep
    call PrintArray
ENDM

mPrintString MACRO string:REQ
    mov ah,9                        
    mov dx,OFFSET string           
    int 21h                        
ENDM

mPrintLn MACRO
    mPrintSymbol 0dh
    mPrintSymbol 0ah
ENDM

;--------------------------

.data
	array sdword -1, 2, 5, -9, 4, -3, 8, -13, -7, -8, 9, -10, -6, -12, 6
	arrayMes byte "Source Array: $"
	resultMes byte "Result Array: $"
	resArrCountMes byte "Result length: $"
	sortMes byte "Sorted array: $"
	R dd n-1;количество неотсортированных элементов минус один
	n = ($-array)/4
.data?
	resArr sdword 15 dup(?)
	resArrCount sdword ?
.code	
.startup
	
	; выводим исходный массив
	mPrintString arrayMes
	mPrintArray  array,LENGTHOF array
	mPrintLn
	;=======================================
	;устанавливаем указатель на 1 элемент массива
	lea edi, array
	lea esi, resArr
	;====================================
	call createResult
	mPrintString resArrCountMes
	mPrintInt32 resArrCount
	mPrintLn
	mPrintString resultMes
	call PrintResult
	mPrintLn
	;==============================
	call sort
	mPrintString sortMes
	mPrintArray  array,LENGTHOF array
	mPrintLn	
	call sort1
	mPrintString sortMes
	mPrintArray  array,LENGTHOF array
	mPrintLn
	

.EXIT

sort PROC
	mov cx, 15
	xor si, si
	frst_sort:              ;отрицательные в стэк
		mov EAX,array[SI]
		cmp EAX,0 
		jge skip_push
		push EAX 
	skip_push:
		add SI, 4                      	
	loop frst_sort
	
	mov cx, 15
	xor si,si
	start_sort:
		mov EAX,array[SI]                
		cmp EAX,0                       
		jl  skip_push2                   
		push EAX 	
	skip_push2:	
		add SI,4                       	
	loop start_sort
	
	mov si, 56	
	mov cx, 15

	pop_ll:	
		pop EAX
		mov array[SI],EAX 
		sub SI,4
	loop pop_ll
RET
sort ENDP	

sort1 PROC	
	xor si,si
	mov di,4
	sort2:                              ;сортируем отрицательные по возрастанию
		mov EAX,array[SI]
		mov ECX,array[DI]
		cmp EAX,0
		jge skip_it
			cmp ECX,EAX
			jg skip_it
				mov EDX,EAX
				mov array[SI],ECX
				mov array[DI],EDX
				xor SI,SI
				mov DI,4
				jmp sort2	
	skip_it:
		add SI,4 
		add DI,4	
		cmp SI,40 
		jne sort2                          ;сортируем отрицательные по возрастанию
RET
sort1 ENDP		
	
		
	
	
	
createResult PROC
	mov eax, [edi]
	mov ecx, [edi+56]
	mov cx, 15
	mov resArrCount, 0
	start_loop:
		mov EDX,[edi]
		cmp edx, eax
		jge  hi_check
			mov [ESI],EDX
			add ESI,4
			add resArrCount, 1
		hi_check:
			cmp EDX,ECX
			jle skip_one
				mov [esi],EDX
				add esi,4
			skip_one:	
				add EDI,4                      
	loop start_loop	
RET
createResult ENDP

PrintResult PROC 
mov ecx, resArrCount	
	sub ecx, 1
	mov esi, 0
		res:
		mPrintInt32 resArr[esi]
		mPrintSymbol ','
		add esi, 4
		loop res
		mPrintInt32 resArr[esi]
RET
PrintResult ENDP

PrintInt32 PROC USES eax cx ebx edx
    pushfd               
    TEST eax,080000000h 
    jz   positive_ax    
    push eax            
    mPrintSymbol '-'    
    pop  eax            
    neg  eax            
	positive_ax:            
		mov  ebx,10         
		mov  cx,0           
	l1:
		mov  edx,0          
		div  ebx            
		add  dx,30h         
		push dx             
		inc  cx           
		cmp eax,0        
		jne l1       
		mov ah,2          
	l2:
		pop dx             
		int 21h             
		loop l2      
		popfd 
    RET
PrintInt32 ENDP

PrintArray PROC USES eax cx si
    pushfd              
    cmp  cx,1
    jb   return        
    je   skip_l1     
    dec  cx            
l1:
    mov  eax,[si]       
    call PrintInt32    
    add  si,4         
    mov  ah,2    
    int  21h      
    loop l1            
skip_l1:
    mov  eax,[si]      
    call PrintInt32     
return:
    popfd               
    RET
PrintArray ENDP

END