max(X,Y,X):- X>Y, !.
max(_,Y,Y).

max(X,Y,Z,U):- max(X,Y,V),max(V,Z,U).

max3(X,Y,Z,X):- X > Y, X > Z, !.
max3(_,Y,Z,Y):- Y > Z, !.
max3(_,_,Z,Z).

sumDigUp(0,0):- !.
sumDigUp(N,S):- 
	Cifr is N mod 10,
	N1 is N div 10,
	sumDigUp(N1,S1),
	S is S1 + Cifr. 

sumDigDown(0,CurS,CurS):- !.
sumDigDown(N,CurS,Sum):- 
	Cifr is N mod 10,
	N1 is N div 10,
	CurS1 is CurS + Cifr,
	sumDigDown(N1,CurS1,Sum).

sumDigDown(N,Sum):- sumDigDown(N,0,Sum).



writelist([]):- !.
writelist([H|T]):- write(H),nl,write(T).

r_list(A,N):-r_list(A,N,0,[]).
r_list(A,N,N,A):-!.
r_list(A,N,K,B):-read(X),append(B,[X],B1),K1 is K+1,r_list(A,N,K1,B1).

sum_el([], 0) :- !.
sum_el([H|T],Sum) :- sum_el(T, SumT), Sum is H + SumT.




myApp([],Y,Y).
myApp([H|T],Y,[H|T1]):-
	myApp(T,Y,T1).




%дз

% Задание 2

% Найти произведение цифр числа

% Произведение цифр числа, рекурсия вверх
prodDigitsUp(0,1):- !.
prodDigitsUp(N,Prod):-
    Cifr is N mod 10,
    N1 is N div 10,
    prodDigitsUp(N1,Prod1),
    Prod is Prod1 * Cifr.

% Произведение цифр числа, рекурсия вниз
prodDigitsDown(0,Prod,Prod):- !.
prodDigitsDown(N,CurProd,Prod):-
    Cifr is N mod 10,
    N1 is N div 10,
    CurProd1 is CurProd * Cifr,
    prodDigitsDown(N1,CurProd1,Prod).

prodDigitsDown(N,Prod):- prodDigitsDown(N,1,Prod).


% Найти количество нечетных цифр числа, больших 3

% Количество нечетных цифр >3, рекурсия вверх
countOddGT3Up(0,0):- !.
countOddGT3Up(N,Count):-
    Cifr is N mod 10,
    N1 is N div 10,
    countOddGT3Up(N1,Count1),
    (Cifr mod 2 =:= 1, Cifr > 3 -> Count is Count1 + 1 ; Count = Count1).

% Количество нечетных цифр >3, рекурсия вниз
countOddGT3Down(0,Count,Count):- !.
countOddGT3Down(N,CurCount,Count):-
    Cifr is N mod 10,
    N1 is N div 10,
    (Cifr mod 2 =:= 1, Cifr > 3 -> CurCount1 is CurCount + 1 ; CurCount1 = CurCount),
    countOddGT3Down(N1,CurCount1,Count).

countOddGT3Down(N,Count):- countOddGT3Down(N,0,Count).


% Найти НОД двух чисел

% НОД двух чисел, рекурсия вниз
nod(X,0,X):- !.
nod(X,Y,Res):-
    Ost is X mod Y,
    nod(Y,Ost,Res).


% Задание 3

% 1.1 Найти количество элементов после последнего максимального элемента

% Найти индекс последнего максимального элемента
lastMaxIndex(List,Index):-
    max_list(List,Max),
    findLastIndex(List,Max,0,-1,Index).

findLastIndex([],_,_,Index,Index).
findLastIndex([H|T],Max,CurrI,LastI,Index):-
    (H =:= Max -> NewLastI is CurrI ; NewLastI = LastI),
    NextI is CurrI + 1,
    findLastIndex(T,Max,NextI,NewLastI,Index).

% Главный предикат для задачи 1.1
countAfterLastMax(List,Count):-
    lastMaxIndex(List,Idx),
    length(List,Len),
    Count is Len - Idx - 1.



% 1.13 Перенести элементы до минимального в конец списка

% Найти индекс первого минимального элемента
firstMinIndex(List,Index):-
    min_list(List,Min),
    findFirstIndex(List,Min,0,Index).

findFirstIndex([H|_],Min,Idx,Idx):- H =:= Min, !.
findFirstIndex([_|T],Min,CurrI,Idx):-
    NextI is CurrI + 1,
    findFirstIndex(T,Min,NextI,Idx).

% Разделить список по индексу
splitListAt(List,Idx,Front,Back):-
    length(Front,Idx),
    append(Front,Back,List).

% Главный предикат для задачи 1.13
moveBeforeMinToEnd(List,Result):-
    firstMinIndex(List,Idx),
    splitListAt(List,Idx,Front,Back),
    append(Back,Front,Result).


% 1.25 Найти максимальный элемент в интервале a..b

% Выбрать элементы в интервале
selectInInterval([],_,_,[]).
selectInInterval([H|T],A,B,[H|T1]):-
    H >= A, H =< B, !,
    selectInInterval(T,A,B,T1).
selectInInterval([_|T],A,B,T1):-
    selectInInterval(T,A,B,T1).

% Главный предикат для задачи 1.25
maxInInterval(List,A,B,Max):-
    selectInInterval(List,A,B,Filtered),
    max_list(Filtered,Max).






























