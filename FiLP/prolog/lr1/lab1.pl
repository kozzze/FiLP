man:-man(X), print(X),nl, fail.
woman:- woman(X),print(X),nl,fail.

mother(X,Y) :- parent(X, Y), woman(X).
mother(X) :- mother(Y, X), print(Y).

brother(X, Y) :- man(Y), parent(Z,X), parent(Z,Y), man(Z), Y\=X,nl.
brothers(X) :- brother(X,Y), Y\=X, print(Y),nl,fail.

b_s(X,Y) :- parent(Z,X), parent(Z,Y), Y\=X, nl.
b_s(X) :-  setof(Y, b_s(Y, X), Siblings), write(Siblings).

% 2

father(X,Y):- parent(X,Y),man(X).
father(X):- parent(Y,X),man(Y),print(Y).

sister(X,Y):- parent(Z,X),parent(Z,Y),woman(X).
sister(X):- parent(P,X),parent(P,Y),woman(Y),print(Y),nl,fail.