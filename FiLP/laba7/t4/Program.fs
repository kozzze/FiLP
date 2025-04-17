open System

// алгебраический тип сообщений
type EchoMessage =
    | Say of string
    | Exit

// агент
let echoAgent = MailboxProcessor.Start(fun inbox ->
    let rec loop () = async {
        let! msg = inbox.Receive()
        match msg with
        | Say text ->
            printfn "Получено сообщение: %s" text
            return! loop()
        | Exit ->
            printfn "Агент завершает работу."
            return ()
    }
    loop ()
)

[<EntryPoint>]
let main argv =
    printfn "Введите сообщение ('exit' для выхода):"
    let mutable running = true
    while running do
        let input = Console.ReadLine()
        if input = "exit" then
            echoAgent.Post Exit
            running <- false
        else
            echoAgent.Post(Say input)
    0