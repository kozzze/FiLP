open System
open System.Collections.Generic

let isPalindrome (n: int64) : bool =
    let s = n.ToString()
    s = String(Array.rev (s.ToCharArray()))

let findPalindromicSums (limit: int64) : seq<int64> =
    let maxStart = int (sqrt (float limit))

    let generateSumsFrom start =
        let rec loop current acc (results: Set<int64>) =
            let sum = acc + int64 (current * current) //вычисляем сумму квадратов
            if sum >= limit then results //остановка рекурсии и возврат квадратов
            else
                let newResults =
                    if current > start && isPalindrome sum then
                        results.Add sum
                    else results
                loop (current + 1) sum newResults
        loop (start + 1) (int64 (start * start)) Set.empty

    [1 .. maxStart]//список стартовых значений
    |> List.map generateSumsFrom
    |> Seq.ofList//в последовательность множеств
    |> Set.unionMany//в одно большое множество
    |> fun s -> s :> seq<int64>  //преобразуем в seq тк ф-я должна это вернуть

let solve () =
    let limit = 100_000_000L
    let result = findPalindromicSums limit |> Seq.sum
    printfn "Ответ: %d" result

solve ()