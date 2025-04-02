open System
open Microsoft.FSharp.Collections

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
//printf($"Сумма рекурсия вниз через выражение = {sumDDuwn 123}")
//printf($"Сумма хвостовая рекурсия = {sumDTail 123}")



//Сумма делителей числа (лаба)
//вверх
let rec quanDelVV x index=
    match index with
    | index when (x%index = 0 && index < x) -> 1 + quanDelVV x (index+1)
    | index when (index >= x) -> 0
    | _ -> 0 + quanDelVV x (index+1)

//System.Console.WriteLine(quanDelVV 10 1)

//вниз
let quanDelVN x = 
    let rec quanDelVN x index sum =
        let isNeed = (x%index = 0) && (index < x)
        let new_sum = sum + 1
        match isNeed with
            | true -> quanDelVN x (index+1) (new_sum)
            | false when index < x -> quanDelVN x (index+1) sum
            | _ -> sum
    quanDelVN x 1 0
//System.Console.WriteLine(quanDelVN 10)

//дз 5 лаб до конца

//6

let factorial n =
    let rec mul n acc =
        match n with
            | 0 | 1 -> acc
            | _ -> mul(n-1) (acc*n)
    mul n 1

let sumDigits num : int =
    let rec digitalSubSum num currentSum = 
        if num = 0 then currentSum
        else
            let currentNum = num / 10
            let digital = num % 10
            let accumulator = currentSum + digital
            digitalSubSum currentNum accumulator
    digitalSubSum num 0

let isTrue (b:bool) =
    match b with
        | true -> factorial 5
        | false -> sumDigits 123

//Console.WriteLine(isTrue false)

//7-8
let rec reduce (n:int) (func : int -> int -> int) (acc:int) =
    match n with
        | 0 -> acc
        | _ ->
            let digit = (n%10)
            let newAcc = func acc digit
            let curDigit = (n/10)
            reduce curDigit func newAcc

let testReduce () =
    Console.WriteLine(reduce 1234 (fun acc digit -> acc + digit) 0)
    Console.WriteLine(reduce 1234 (fun acc digit -> acc * digit) 1)
    Console.WriteLine(reduce 1234 (fun acc digit -> if digit < acc then digit else acc) 10)
    Console.WriteLine(reduce 1234 (fun acc digit -> if digit > acc then digit else acc) 0)

//testReduce()

//9-10
let rec filterReduce (n:int) (func : int -> int -> int) (acc:int) (condition : int -> bool) =
    match n with
        | 0 -> acc
        | _ ->
            let digit = n%10
            let newacc =    
                match condition digit with
                    | true -> func acc digit
                    | false -> acc
            let curDigit = n / 10
            filterReduce curDigit func newacc condition

let filterReduceTest () = 
    Console.WriteLine(filterReduce 12345 (fun acc digit -> acc + digit) 0 (fun digit -> digit % 2 = 0))
    Console.WriteLine(filterReduce 12345 (fun acc digit -> acc * digit) 1 (fun digit -> digit <> 1))
    Console.WriteLine(filterReduce 12345 (fun acc digit -> acc + 1) 0 (fun digit -> digit > 3))
    Console.WriteLine(filterReduce 12345 (fun acc digit -> if digit < acc then digit else acc) 10 (fun digit -> true))
//filterReduceTest()

//11
let quiz input =
    match input with
        | "F#"|"Prolog" -> Console.WriteLine("Ну ты подлиза")
        | "C" -> Console.WriteLine("Ну-ну, удачи")
        | "Ruby" -> Console.WriteLine("ВХАХВАХВХАХВА")
        | _ -> Console.WriteLine("Иди учись, бестолочь")
//quiz "Ruby"

//12
let curryQuiz () =
    let input = Console.ReadLine()
    let proc = quiz input
    let output = Console.WriteLine proc
    output
    
let superQuiz () =
    (Console.ReadLine >> quiz >> Console.WriteLine)()


//13
let rec gcd a b =
    match b with
        | 0 -> a
        | _ -> gcd b (a%b)

let coprimeDigits (n :int) (func: int -> int -> int) (acc :int) =
        let rec loop cur acc =
            match cur with
                | 0 -> acc
                | _ ->
                    let digit = cur % 10
                    let newAcc =
                        match digit with
                            | 0 -> acc
                            | digit when gcd n digit = 1 -> func acc digit
                            | _ -> acc
                    loop (cur/10) newAcc
        loop n acc

let coprimeDigitsTest () =
    Console.WriteLine(coprimeDigits 12345 (fun acc digit -> acc + digit) 0)
    Console.WriteLine(coprimeDigits 12345 (fun acc digit -> acc * digit) 1)
    Console.WriteLine(coprimeDigits 12345 (fun acc digit -> if digit < acc then digit else acc) 10)
    Console.WriteLine(coprimeDigits 12345 (fun acc digit -> if digit > acc then digit else acc) 0)
    Console.WriteLine(coprimeDigits 12345 (fun acc digit -> acc + 1) 0)


//14

let eiler n =
    let rec loop cur acc =
        match cur with
        | 0 -> acc
        | _ ->
            let newAcc =
                match gcd n cur with
                | 1 -> acc + 1
                | _ -> acc
            loop (cur - 1) newAcc
    loop (n-1) 0

//15
let coprimeFilter (n:int) (condition: int -> bool) (func: int -> int -> int) (initial:int) =
    let rec loop cur acc =
        match cur with
        | 0 -> acc
        | _ ->
            let digit = n % 10
            let newAcc =
                if gcd n digit = 1 && condition digit then func acc digit
                else acc
            loop (cur/10) newAcc
    loop n initial

let coprimeFilterTest () =
    Console.WriteLine(coprimeFilter 12345 (fun digit -> digit % 2 = 0) (fun acc digit -> digit + acc) 0)
    Console.WriteLine(coprimeFilter 12345 (fun digit -> digit > 3) (fun acc digit -> digit * acc) 1)
    Console.WriteLine(coprimeFilter 12345 (fun digit -> digit <> 1) (fun acc digit -> if digit < acc then digit else acc) 10)
    Console.WriteLine(coprimeFilter 12345 (fun digit -> digit % 5 <> 0) (fun acc digit -> if digit > acc then digit else acc) 0)
    Console.WriteLine(coprimeFilter 12345 (fun digit -> digit < 4) (fun acc digit -> acc + 1) 0)

//16 var 1

//method 1 - sum prime divisors
let isPrime n =
    match n with
    | n when n <= 1 -> false
    | _ ->
        let rec check i =
            match i * i > n with
            | true -> true
            | false -> 
                match n % i = 0 with
                | true -> false
                | false -> check (i + 1)
        check 2

let sumPrimeDivisors n =
    let rec loop i acc =
        match i > n with
        | true -> acc
        | false ->
            match n % i = 0 && isPrime i with
            | true -> loop (i + 1) (acc + i)
            | false -> loop (i + 1) acc
    loop 2 0

 //method - 2 count %2!=0 > 3
let countDigitsMore3 n =
    let rec loop num count =
        match num with
        | 0 -> count
        | _ ->
            let digit = num % 10
            match digit % 2 <> 0 && digit > 3 with
            | true -> loop (num / 10) (count + 1)
            | false -> loop (num / 10) count
    loop n 0

//method 3 - Найти прозведение таких делителей числа, сумма цифр которых мень-ше, чем сумма цифр исходного числа.
let multDivSumDigLessSum n =
    let targetSum = sumDigits n
    let rec loop divisor acc =
        match divisor with
        | 0 -> acc
        | _ ->
            match n % divisor = 0 && sumDigits divisor < targetSum with
            | true -> loop (divisor - 1) (acc * divisor)
            | false -> loop (divisor - 1) acc
    match n with
    | 0 | 1 -> 0 
    | _ -> 
        let result = loop n 1
        if result = 1 then 0 else result




let testMethods () =
    let number1 = 30
    let number2 = 123456789
    printfn "Метод 1: Сумма простых делителей числа %d = %d" number1 (sumPrimeDivisors number1)
    printfn "Метод 2: Количество четных чисел > 3 числа %d = %d" number2 (countDigitsMore3 number2)
    printfn "Метод 2: Прозведение делителей, сумма цифр меньше, чем сумма цифр исходного числа = %d" (multDivSumDigLessSum 12)
//testMethods()

let getFunctionByNumber =
    function
    | 1 -> sumPrimeDivisors
    | 2 -> countDigitsMore3
    | 3 -> multDivSumDigLessSum
    | _ -> failwith "Неверный номер функции (допустимо 1-3)"

let main =
    let parseInput (input: string) =
        match input.Split() |> Array.map int with
        | [| funcNum; arg |] -> (funcNum, arg)
        | _ -> failwith "Ожидается два числа через пробел"   
    let executeFunction (funcNum, arg) =
        (getFunctionByNumber funcNum) arg
    let printResult result =
        printfn "Result = %d" result
    (Console.ReadLine >> parseInput >> executeFunction >> printResult) ()

main
