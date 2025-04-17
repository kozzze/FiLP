open FParsec
open System

// Алгебраический тип для выражения (число, сумма, разность)
type Expr =
    | Number of float
    | Plus of Expr * Expr
    | Minus of Expr * Expr

let pstring_ws str = spaces >>. pstring str .>> spaces
let float_ws = spaces >>. pfloat .>> spaces

let parser, parserRef = createParserForwardedToRef<Expr, unit>()

let parsePlus = tuple2 (parser .>> pstring_ws "+") parser |>> Plus 
let parseMinus = tuple2 (parser .>> pstring_ws "-") parser |>> Minus
let parseNumber = float_ws |>> Number

let parseExpression = between (pstring_ws "(") (pstring_ws ")") (attempt parsePlus <|> parseMinus)
parserRef.Value <- parseNumber <|> parseExpression

// вычисление выражения
let rec ParseExpr (e:Expr): float =
    match e with
    | Number(num) -> num
    | Plus(x,y) ->
        let left = ParseExpr(x)
        let right = ParseExpr(y)
        left + right
        
    | Minus(x,y) ->
        let left = ParseExpr(x)
        let right = ParseExpr(y)
        left - right

[<EntryPoint>]
let main argv =
    printfn "Выражение: "
    let input = Console.ReadLine() 
    let expr = run parser input

    match expr with
    | Success (result, _, _) ->
        printfn "Преобразованное выражение: %A" (result)
        printfn "Ответ: %A" (ParseExpr result)
    | Failure (errorMsg, _, _) -> printfn "Ошибка: %s" errorMsg

    0