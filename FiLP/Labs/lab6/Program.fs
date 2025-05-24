// let rec readList n = 
//     if n=0 then []
//     else
//     let Head = System.Convert.ToInt32(System.Console.ReadLine())
//     let Tail = readList (n-1)
//     Head::Tail
//
// let readData = 
//     let n=System.Convert.ToInt32(System.Console.ReadLine())
//     readList n

// let rec writeList = function
//     [] ->   let z = System.Console.ReadKey()
//             0
//     | (head : int)::tail -> 
//                        System.Console.WriteLine(head)
//                        writeList tail  
open System
let max2 x y = if x > y then x else y

let rec accCond list (f : int -> int -> int) p acc = 
    match list with
    | [] -> acc
    | h::t ->
                let newAcc = f acc h
                if p h then accCond t f p newAcc
                else accCond t f p acc

let listMin list = 
    match list with 
    |[] -> 0
    | h::t -> accCond list (fun x y -> if x < y then x else y) (fun x -> true) h

let listMax list = 
    match list with 
    |[] -> 0
    | h::t -> accCond list max2 (fun x -> true) h

let listSum list = accCond list (fun x y -> x + y) (fun x -> true) 0

let listPr list = accCond list (fun x y -> x * y) (fun x -> true) 1

 


let f6 list = 
    match list with
    |[] -> 0
    | h::t ->   if (h % 2 = 0) then accCond t max2 (fun x -> ((x % 2) = 0)) h
                else accCond t max2 (fun x -> ((x % 2) <> 0) ) h

let rec frequency list elem count =
        match list with
        |[] -> count
        | h::t -> 
                        let count1 = count + 1
                        if h = elem then frequency t elem count1 
                        else frequency t elem count

let rec freqList list mainList curList = 
        match list with
        | [] -> curList
        | h::t -> 
                    let freqElem = frequency mainList h 0
                    let newList = curList @ [freqElem]
                    freqList t mainList newList

let pos list el = 
    let rec pos1 list el num = 
        match list with
            |[] -> 0
            |h::t ->    if (h = el) then num
                        else 
                            let num1 = num + 1
                            pos1 t el num1
    pos1 list el 1

let getIn list pos = 
    let rec getIn1 list num curNum = 
        match list with 
            |[] -> 0
            |h::t -> if num = curNum then h
                     else 
                            let newNum = curNum + 1
                            getIn1 t num newNum
    getIn1 list pos 1

let f7 list = 
    let fL = freqList list list []
    (listMax fL) |> (pos fL) |> (getIn list)           

let filter list pr = 
    let rec filter1 list pr newList = 
        match list with
        | [] -> newList
        | h::t ->
                let newnewList = newList @ [h]
                if pr h then filter1 list pr newnewList
                else filter1 list pr newList
    filter1 list pr [] 

let even n = ((n % 2) = 0)

let f8Cond list el = (even el) && (even (frequency list el 0))

let f8 list = filter list (f8Cond list)

let delEL list el = filter list (fun x -> (x <> el))

let uniq list = 
    let rec uniq1 list newList = 
        match list with
            |[] -> newList
            | h::t -> 
                        let listWithout = delEL t h
                        let newnewList = newList @ [h]
                        uniq1 listWithout newnewList
    uniq1 list [] 

let rec cifrSum n = 
    if n = 0 then 0
    else (n%10) + (cifrSum (n / 10))

let f9Cond el = ((cifrSum el) > 9) || (even el)

let f9 list = filter list f9Cond

let count x y = x + 1

let f10Cond list El = (accCond list count (fun x -> ((x * x) = El)) 0) > 0

let f10 list = accCond list count (f10Cond list) 0

//---------
//вариант 5 - 5 15 25 35 45 55

//5
let toChurchList (lst: 'a list) : ('a -> 'b -> 'b) -> 'b -> 'b =
    let rec loop lst cons nil =
        match lst with
        | [] -> nil
        | h::t -> cons h (loop t cons nil)
    loop lst
    
let rec findMin list = 
    match list with 
    | [x] -> x  
    | h::t -> 
        let minTail = findMin t
        if h < minTail then h else minTail

let isGlobalMin index list =
    let minValue = findMin list
    let rec loop i lst =
        match lst with
        | [] -> false
        | h::t -> 
            if i = index then h = minValue
            else loop (i + 1) t
    loop 0 list
    
//15
let isLocalMin index list =
    let rec getElement idx lst =
        match lst with
        | [] -> None
        | h::t -> if idx = 0 then Some h else getElement (idx - 1) t

    match getElement index list with
    | Some current ->
        let left = if index > 0 then Some (List.nth list (index - 1)) else None
        let right = if index < List.length list - 1 then Some (List.nth list (index + 1)) else None
        (left, right) |> function
        | (Some l, Some r) -> current < l && current < r
        | (Some l, None) -> current < l
        | (None, Some r) -> current < r
        | (None, None) -> false 
    | None -> false
    
let isLocalMinList index list =
    if index > 0 && index < List.length list - 1 then
        let left = list.[index - 1]
        let right = list.[index + 1]
        let current = list.[index]
        current < left && current < right
    else
        false
        
//25

let findMaxInRange a b list =
    list
    |> List.filter (fun x -> x >= a && x <= b)  
    |> List.max 
let findMaxInRangeChurch a b list =
    let rec filterInRange lst =
        match lst with
        | [] -> []
        | h::t -> 
            if h >= a && h <= b then h :: filterInRange t
            else filterInRange t

    let rec findMax lst maxVal =
        match lst with
        | [] -> maxVal
        | h::t -> 
            if h > maxVal then findMax t h
            else findMax t maxVal

    let filteredList = filterInRange list
    if List.isEmpty filteredList then None  
    else Some (findMax filteredList Int32.MinValue)
      
//35

let findClosestToR R arr =
    arr
    |> List.minBy (fun x -> abs(x - R))

let findClosestToRChurch R list =
    let rec findClosest lst closestVal minDiff =
        match lst with
        | [] -> closestVal
        | h::t ->
            let diff = abs (h - R)
            if diff < minDiff then
                findClosest t h diff
            else
                findClosest t closestVal minDiff

    match list with
    | [] -> None  
    | h::t -> Some (findClosest t h (abs (h - R)))
    

//45
    
let sumInRangeChurch a b list =
    let rec filterInRange lst =
        match lst with
        | [] -> []
        | h::t -> 
            if h >= a && h <= b then h :: filterInRange t
            else filterInRange t

    let rec sum lst acc =
        match lst with
        | [] -> acc
        | h::t -> sum t (acc + h)

    let filteredList = filterInRange list
    sum filteredList 0

//55

let sortByFrequency arr =
    arr
    |> List.countBy id  
    |> List.sortByDescending snd  
    |> List.collect (fun (x, count) -> List.init count (fun _ -> x))  

let countFrequency list =
    let rec count lst countList =
        match lst with
        | [] -> countList
        | h::t -> 
            let countOfH = List.filter (fun x -> x = h) list |> List.length
            let countList' = if List.exists (fun (x, _) -> x = h) countList then countList else (h, countOfH) :: countList
            count t countList'
    count list []

let sortByFrequencyChurch arr =
    let counts = countFrequency arr
    let sortedCounts = List.sortByDescending snd counts  
    List.collect (fun (x, count) -> List.init count (fun _ -> x)) sortedCounts



//------17---------


//1
let lcs (A: 'a list) (B: 'a list) : 'a list =
    let m = List.length A
    let n = List.length B
    let dp = Array2D.init (m + 1) (n + 1) (fun _ _ -> 0)

    for i in 1 .. m do
        for j in 1 .. n do
            if A.[i - 1] = B.[j - 1] then
                dp.[i, j] <- dp.[i - 1, j - 1] + 1
            else
                dp.[i, j] <- max dp.[i - 1, j] dp.[i, j - 1]

    let rec buildLCS i j =
        if i = 0 || j = 0 then []
        else if A.[i - 1] = B.[j - 1] then
            A.[i - 1] :: buildLCS (i - 1) (j - 1)
        else if dp.[i - 1, j] >= dp.[i, j - 1] then
            buildLCS (i - 1) j
        else
            buildLCS i (j - 1)

    buildLCS m n

//2

let processList (input: int list) =
    let divByTwo = List.filter (fun x -> x % 2 = 0) input |> List.map (fun x -> x / 2)
    let divByThree = divByTwo |> List.filter (fun x -> x % 3 = 0)
    let squares = divByThree |> List.map (fun x -> x * x)
    let commonElements = squares |> List.filter (fun x -> List.contains x divByTwo)
    let allElements = List.concat [divByThree; squares; commonElements]
    
    (divByTwo, divByThree, squares, commonElements, allElements)

//3

let gcd a b = 
    let rec loop a b = 
        if b = 0 then a else loop b (a % b)
    loop a b

let findPairsForN (N: int) =
    let pairs = 
        [1..N] 
        |> List.collect (fun x -> 
            [1..N] 
            |> List.choose (fun y -> 
                if x * y = N then 
                    let d = gcd x y 
                    Some (x / d, y / d)
                else None))
    List.distinct pairs

//4

let findPythagoreanTriplets (input: int list) =
    input 
    |> List.collect (fun a ->
        input 
        |> List.collect (fun b ->
            input 
            |> List.choose (fun c ->
                if a < b && b < c && a * a + b * b = c * c then 
                    Some (a, b, c) 
                else None)))
    
//5

let primesUpTo n =
    let sieve = Array.create (n + 1) true
    sieve.[0] <- false
    sieve.[1] <- false
    for i = 2 to int (sqrt (float n)) do
        if sieve.[i] then
            for j = i * i to n do
                sieve.[j] <- false
    [for i in 2..n do if sieve.[i] then yield i]

let findElementsWithAllPrimeDivisors (input: int list) =
    let primes = primesUpTo (List.max input)
    input |> List.filter (fun x ->
        primes |> List.forall (fun p -> x % p = 0))
    
//6
let sortTuples (input: (int * int * int * int * int) list) =
    input |> List.sortBy (fun (a, b, c, d, e) -> [a; b; c; d; e])
    |> List.map (fun (a, b, c, d, e) -> a * 10000 + b * 1000 + c * 100 + d * 10 + e)    

//7

let sumOfDivisors n =
    [1..n/2] |> List.filter (fun x -> n % x = 0) |> List.sum

let findSumOfDivisorsWithCondition (input: int list) =
    let avg = List.averageBy float input
    let evenIndexElements = input |> List.mapi (fun i x -> if i % 2 = 0 then Some x else None) |> List.choose id
    input |> List.sortBy (fun x ->
        sumOfDivisors x)    
//8

// let digitFrequency n =
//     n.ToString()
//     |> Seq.map (fun c -> int c - int '0')
//     |> Seq.countBy id
//     |> Seq.toList
//
// let findAverageOfFrequentDigits (input: int list) =
//     let freq = input |> List.collect digitFrequency
//     input |> List.map (fun num ->
//         let digits = num.ToString() |> Seq.map (fun c -> int c - int '0')
//         let frequentDigits = digits |> Seq.filter (fun d -> List.exists (fun (digit, count) -> digit = d && count > 1) freq)
//         frequentDigits |> Seq.average)    

//9

let processNumbers (input: int list) =
    input |> List.filter (fun x -> x > List.sum input)
    
//10
let processProductAndSum (input: int list) =
    let list2 = input |> List.collect (fun x -> input |> List.map (fun y -> x * y))
    let list3 = input |> List.collect (fun x -> input |> List.map (fun y -> x + y))
    let list4 = input |> List.filter (fun x -> input |> List.filter (fun y -> y % x = 0) |> List.length = 4)
    (list2, list3, list4)

//------18------

let reverseArray arr =
    Array.rev arr
let copyLastElementFromBToA a b =
    Array.append a [| Array.last b |]
let mergeArrays a b =
    Array.append a b
let filterDivisibleBy3 arr =
    Array.filter (fun x -> x % 3 = 0) arr
let readArray () =
    Console.ReadLine().Split(' ') |> Array.map int

let printDifference () =
    let a = readArray ()
    let b = readArray ()
    let aNumber = Array.fold (fun acc x -> acc * 10 + x) 0 a
    let bNumber = Array.fold (fun acc x -> acc * 10 + x) 0 b
    printfn "%d" (aNumber - bNumber)
let printUnion () =
    let a = readArray ()
    let b = readArray ()
    Array.append a b |> Array.distinct |> Array.iter (printf "%d ")
let printIntersection () =
    let a = readArray ()
    let b = readArray ()
    a |> Array.filter (fun x -> Array.contains x b) |> Array.iter (printf "%d ")
let printSymmetricDifference () =
    let a = readArray ()
    let b = readArray ()
    let diff = Array.filter (fun x -> not (Array.contains x b)) a
    let diff2 = Array.filter (fun x -> not (Array.contains x a)) b
    Array.append diff diff2 |> Array.iter (printf "%d ")
let printNumbersDivisibleBy13Or17 () =
    [1..100]
    |> List.filter (fun x -> x % 13 = 0 || x % 17 = 0)
    |> List.toArray
    |> Array.iter (printf "%d ")



[<EntryPoint>]
let main argv = 
//    writeList readData
    // let l = readData
    // let sum : int = listSum l
    // let pr : int= listPr l
    // let min : int= listMin l
    // let max : int= listMax l
    // System.Console.WriteLine((sum,pr,min,max))
    //
    // let ans = f7 l
    // System.Console.WriteLine(ans)
    // let z = System.Console.ReadKey()
    
    
    let arr = [3; 1; 4; 1; 5; 9; 2; 6; 5; 3]
    let index = 3
    let a = 2
    let b = 6
    let R = 3
    // let resultList = isLocalMinList index arr
    // printfn "Метод List: Элемент с индексом %d %s локальный минимум" index (if resultList then "является" else "не является")
    // let resultChurch = isLocalMin index arr
    // printfn "Метод Church: Элемент с индексом %d %s локальный минимум" index (if resultChurch then "является" else "не является")
    // let resultList = findMaxInRange a b arr
    // printfn "Максимальный элемент в интервале [%d, %d] с использованием List: %d" a b resultList
    // let resultChurch = findMaxInRangeChurch a b arr
    // match resultChurch with
    // | Some maxVal -> printfn "Максимальный элемент в интервале [%d, %d] с использованием Church List: %d" a b maxVal
    // | None -> printfn "Нет элементов в интервале [%d, %d]" a b
    // let resultChurch = sumInRangeChurch a b arr
    // printfn "Сумма элементов в интервале [%d, %d] с использованием Church List: %d" a b resultChurch
    // let sortedArrChurch = sortByFrequencyChurch arr
    // printfn "Упорядоченный список с Church List: %A" sortedArrChurch
    // let input = [30; 20; 15; 45] 
    // let result = findElementsAllPrimeDivisors input
    // printfn "Elements with all prime divisors: %A" result
    
    0