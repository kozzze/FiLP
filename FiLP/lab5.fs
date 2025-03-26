open System

let hello =
    //Console.WriteLine("Hello")
    0

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
resPrint()