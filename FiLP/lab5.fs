open System

let hello =
    //Console.WriteLine("Hello")
    0

//2

let solveQ a b c =
    let d = b*b - 4.*a*c
    if d > 0. then
        let x1 = (-b + sqrt(d)) / (2.*a)
        let x2 = (-b - sqrt(d)) / (2.*a)
        Some(x1,x2)
    elif d = 0. then
        let x = -b / (2.*a)
        Some(x,x)
    else
        None
let res = solveQ 1. 2. -3.
let resPrint()=
    match res with
    | Some(x1, x2) when x1 = x2 -> printfn $"Уравнение имеет один корень: {x1}"
    | Some(x1, x2) -> printfn $"Решение: {x1}, {x2}"
    | None -> printfn "Нет вещественных корней"

//resPrint()

//3
let circleS (r: float) : float =
    Math.PI * r * r

let cylinderV (r: float) (h: float) : float =
    circleS r * h

let cirCylPrint () =
    let s = circleS 3.0
    let v = cylinderV 3.0 2.0
    printfn $"Площадь круга = {s}"
    printfn $"Объём цилиндра = {v}"

//cirCylPrint ()

//4
let rec sumD n =
    if n = 0 then 0
    else
        let res = sumD (n/10)
        let last = n % 10
        res + last
let sumPrint() =
    printfn($"Сумма цифр числа = {sumD 123}")
//sumPrint()

//5
let rec sumDDuwn n =
    if n = 0 then 0
    else
        (n % 10) + sumDDuwn (n/10)
let sumDTail n =
    let rec sum n acc =
        if n = 0 then 0
        else
            sum (n / 10) (acc + n % 10)
    sum n 0
printf($"Сумма рекурсия вниз через выражение = {sumDDuwn 123}/n")
printf($"Сумма хвостовая рекурсия = {sumDTail 123}")

