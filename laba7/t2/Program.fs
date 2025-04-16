open System

type Maybe<'a> =
    | Just of 'a
    | Nothing

// функтор
let fmapMaybe f value =
    match value with
    | Just x -> Just(f x)
    | Nothing -> Nothing

// аппликативный функтор
let applyMaybe f value =
    match f, value with
    | Just f, Just x -> Just(f x)
    | _ -> Nothing

// монада
let bindMaybe f value =
    match value with
    | Just x -> f x
    | Nothing -> Nothing

[<EntryPoint>]
let main args =
    let example = applyMaybe (Just (fun x -> x + 1)) (Just 2)
    Console.WriteLine($"Пример: {example}")

    let lawFunctorIdentity =
        fmapMaybe id (Just 5) = Just 5 &&
        fmapMaybe id Nothing = Nothing
    Console.WriteLine($"Функтор: Идентичность -> {lawFunctorIdentity}")

    let f x = x + 1
    let g x = x * 2
    let composed = fmapMaybe (f >> g) (Just 3)
    let chained = fmapMaybe g (fmapMaybe f (Just 3))
    let lawFunctorComposition = composed = chained
    Console.WriteLine($"Функтор: Композиция -> {lawFunctorComposition}")

    let lawApplicativeIdentity =
        applyMaybe (Just id) (Just 42) = Just 42
    Console.WriteLine($"Аппликативный функтор: Идентичность -> {lawApplicativeIdentity}")

    let lawApplicativeHomomorphism =
        applyMaybe (Just ((+) 3)) (Just 2) = Just 5
    Console.WriteLine($"Аппликативный функтор: Гомоморфизм -> {lawApplicativeHomomorphism}")

    let u = Just ((+) 10)
    let y = 5
    let lawApplicativeInterchange =
        applyMaybe u (Just y) = applyMaybe (Just (fun f -> f y)) u
    Console.WriteLine($"Аппликативный функтор: Перестановка аргумента -> {lawApplicativeInterchange}")

    let fMonad x = Just (x + 2)
    let lawMonadLeft = bindMaybe fMonad (Just 3) = fMonad 3
    Console.WriteLine($"Монада: Левая единица -> {lawMonadLeft}")

    let lawMonadRight = bindMaybe Just (Just 7) = Just 7
    Console.WriteLine($"Монада: Правая единица -> {lawMonadRight}")

    let fAssoc x = Just (x + 2)
    let gAssoc x = Just (x * 3)
    let m = Just 4
    let leftAssoc = bindMaybe gAssoc (bindMaybe fAssoc m)
    let rightAssoc = bindMaybe (fun x -> bindMaybe gAssoc (fAssoc x)) m
    let lawMonadAssociativity = leftAssoc = rightAssoc
    Console.WriteLine($"Монада: Ассоциативность -> {lawMonadAssociativity}")

    0