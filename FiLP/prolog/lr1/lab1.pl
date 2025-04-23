man:-man(X), print(X),nl, fail.
woman:- woman(X),print(X),nl,fail.

children(X,Y) :- parent(X,Y).


mother(X,Y) :- parent(X, Y), woman(X).
mother(X) :- mother(Y, X), print(Y).

brother(X, Y) :- man(X), parent(Z, X), parent(Z, Y), X \= Y.
brothers(X) :- brother(X,Y), Y\=X, print(Y),nl,fail.

b_s(X,Y) :- parent(Z,X), parent(Z,Y), Y\=X, nl.
b_s(X) :-  setof(Y, b_s(Y, X), Siblings), write(Siblings).

% 2 

father(X,Y):- parent(X,Y),man(X).
father(X):- parent(Y,X),man(Y),print(Y).

sister(X,Y):- parent(Z,X),parent(Z,Y),woman(X).
sister(X):- parent(P,X),parent(P,Y),woman(Y),print(Y),nl,fail.

% 3

grand_pa(X,Y):- parent(X,Z),parent(Z,Y),man(X).
grand_pa(X):- parent(Z,X),parent(G,Z),man(G),print(G),nl,fail.

grand_pa_pred(X,Y):-father(X,Z),((father(Z,Y));mother(Z,Y)).
grand_pa_pred(X):-father(Y,Z),father(Z,X),print(Y),nl,fail.

grand_pa_and_son(X,Y):- (parent(X,Z),parent(Z,Y),man(X),man(Y));(parent(Y,Z),parent(Z,X),man(X),man(Y)).
grand_pa_and_son_pred(X,Y):- (father(X,Z),(father(Z,Y);mother(Z,Y)));(father(Y,Z),(father(Z,X);mother(Z,Y))).

uncle(X,Y):- brother(X,Z),parent(Z,Y).