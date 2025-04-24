:- dynamic car/12.

car(tesla_model_s, 2, 2, 1, 0, 0, 2, 0, 1, 1, 1, 1).
car(bmw_x5, 1, 0, 2, 1, 1, 1, 0, 1, 0, 1, 2).
car(audi_q7, 1, 0, 2, 2, 2, 1, 0, 0, 1, 1, 2).
car(ford_focus, 2, 0, 0, 1, 3, 0, 1, 0, 1, 1, 1).
car(toyota_camry, 0, 0, 0, 3, 4, 1, 0, 0, 0, 1, 1).
car(mercedes_benz_e_class, 0, 0, 2, 4, 0, 2, 0, 1, 0, 1, 2).
car(bmw_3_series, 0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1).
car(ford_mustang, 3, 0, 1, 2, 3, 2, 0, 0, 0, 1, 2).
car(nissan_370z, 3, 0, 1, 0, 4, 2, 0, 0, 0, 1, 1).
car(chevrolet_camaro, 3, 0, 1, 1, 3, 2, 0, 1, 0, 1, 1).
car(honda_civic, 2, 0, 0, 3, 4, 0, 1, 1, 0, 1, 1).
car(volkswagen_golf, 2, 0, 0, 0, 4, 1, 1, 1, 1, 1, 1).
car(lexus_rx, 1, 0, 2, 2, 2, 1, 1, 0, 0, 1, 2).
car(porsche_911, 3, 0, 1, 1, 1, 2, 0, 1, 0, 1, 2).
car(audi_a6, 0, 0, 2, 2, 2, 1, 1, 1, 1, 1, 2).
car(ferrari_488, 3, 0, 1, 1, 0, 2, 0, 0, 1, 1, 2).
car(bmw_m3, 0, 0, 1, 0, 1, 2, 0, 1, 0, 1, 2).
car(chevrolet_silverado, 1, 0, 0, 4, 3, 1, 0, 1, 0, 1, 1).
car(hyundai_santa_fe, 1, 0, 2, 3, 4, 1, 1, 1, 0, 1, 2).
car(bmw_z4, 3, 0, 1, 1, 1, 1, 0, 1, 1, 1, 2).
car(tesla_model_3, 2, 2, 1, 0, 0, 2, 1, 1, 0, 1, 1).

question1(X1):- write("Тип кузова: 0 - Седан, 1 - Внедорожник, 2 - Хэтчбек, 3 - Купе, 4 - Кабриолет"), nl, read(X1).
question2(X2):- write("Тип топлива: 0 - Бензин, 1 - Дизель, 2 - Электрический"), nl, read(X2).
question3(X3):- write("Привод: 0 - Передний, 1 - Задний, 2 - Полный"), nl, read(X3).
question4(X4):- write("Цвет: 0 - Белый, 1 - Черный, 2 - Красный, 3 - Синий, 4 - Серый"), nl, read(X4).
question5(X5):- write("Бренд: 0 - Tesla, 1 - BMW, 2 - Audi, 3 - Ford, 4 - Toyota"), nl, read(X5).
question6(X6):- write("Максимальная скорость: 0 - до 150 км/ч, 1 - до 200 км/ч, 2 - более 200 км/ч"), nl, read(X6).

pr:- question1(X1), question2(X2), question3(X3), question4(X4), question5(X5), question6(X6),
    car(X, X1, X2, X3, X4, X5, X6, _, _, _, _, _), % Используем car/12
    write("Ваша машина: "), write(X), nl.