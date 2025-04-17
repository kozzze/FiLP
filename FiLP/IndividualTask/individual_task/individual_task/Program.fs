open System
open System.Collections.Generic

let isPalindrome (n: int64) : bool =
    let s = n.ToString()
    s = new string(Array.rev (s.ToCharArray())) 

let findPalindromicSums (limit: int64) : seq<int64> =
    let palindromes = HashSet<int64>()

    let maxStart = int (sqrt (float limit))

    let rec accumulateSquares start current acc =
        let sum = acc + int64 (current * current)
        if sum >= limit then ()
        else
            if current > start && isPalindrome sum then
                palindromes.Add(sum) |> ignore
            accumulateSquares start (current + 1) sum

    [1 .. maxStart]
    |> List.iter (fun start ->
        accumulateSquares start (start + 1) (int64 (start * start))
    )

    palindromes :> seq<int64>

let solve () =
    let limit = 100_000_000L
    let result = findPalindromicSums limit |> Seq.sum
    printfn "Ответ: %d" result

solve ()