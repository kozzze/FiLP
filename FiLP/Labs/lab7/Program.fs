open System

type IPrint = interface
    abstract member Print: unit -> unit
    end

[<AbstractClass>]
type Figure() =
    abstract member Area: unit -> float

type Rectangle(width: float, height: float) =
    inherit Figure()

    member this.Width = width
    member this.Height = height

    override this.Area() = this.Width * this.Height
    override this.ToString() = sprintf "Прямоугольник: [ширина: %f, высота: %f, площадь: %f]" this.Width this.Height (this.Area())

    interface IPrint with
        member this.Print() =
            Console.WriteLine(this.ToString())

type Square(side: float) = 
    inherit Rectangle(side, side)
    
    member this.Side = side

    override this.ToString() = sprintf "Квадрат: [длина стороны: %f, площадь: %f]" this.Side (this.Area())

    interface IPrint with
        member this.Print() =
            Console.WriteLine(this.ToString())

type Circle(radius: float) =
    inherit Figure()

    member this.Radius = radius

    override this.Area() = System.Math.PI * radius * radius
    override this.ToString() = sprintf "Круг: [радиус: %f, площадь: %f]" this.Radius (this.Area())

    interface IPrint with
        member this.Print() =
            Console.WriteLine(this.ToString())

type GeometricFigure =
    | RectangleF of float * float
    | SquareF of float
    | CircleF of float

let CalculateArea (figure: GeometricFigure) =
    match figure with
    | RectangleF(w, h) -> w * h
    | SquareF(s) -> s * s
    | CircleF(r) -> System.Math.PI * r * r

[<EntryPoint>]
let main args =
    let r = Rectangle(2.5, 3.5)
    Console.WriteLine(r.Area())

    let s = CalculateArea (SquareF 4.)
    Console.WriteLine(s)

    0

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

open System
open System.Text.RegularExpressions

type DriverLicense(series: string, number: string) =
    do
        match Regex.IsMatch(series, @"^\d{4}$") with
        | true -> ()
        | false -> failwith $"Серия должна содержать 4 цифры: {series}"

        match Regex.IsMatch(number, @"^\d{6}$") with
        | true -> ()
        | false -> failwith $"Номер должен содержать 6 цифр: {number}"
    
    member this.Series = series
    member this.Number = number
    
    override this.Equals(obj: obj) = 
        match obj with
        | :? DriverLicense as other -> this.Series = other.Series && this.Number = other.Number
        | _ -> false
    override this.GetHashCode() = hash (this.Series, this.Number)

    override this.ToString() = $"Права на вождение: \nСерия: {this.Series}\nНомер: {this.Number}"

[<EntryPoint>]
let main args =
    let doc1 = DriverLicense("1234", "567891")
    Console.WriteLine(doc1)
    let doc2 = DriverLicense("1234", "567891")
    let doc3 = DriverLicense("0000", "111111")

    Console.WriteLine(doc1.Equals doc2)

    0

open System
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Layout
open Avalonia.Media
open Avalonia.Themes.Fluent

type MainWindow() as this =
    inherit Window()
    do
        this.Title <- "Тригонометрия"
        this.Width <- 300.0
        this.Height <- 200.0
        this.Padding <- Thickness(20.0)

        // Основной контейнер
        let mainStack = StackPanel()
        mainStack.Spacing <- 10.0

        // Поле ввода
        let input = TextBox()
        input.Width <- 200.0
        input.HorizontalAlignment <- HorizontalAlignment.Left
        input.Text <- "30"  // Пример начального значения

        // Метка результата
        let result = TextBlock()
        result.Width <- 200.0
        result.HorizontalAlignment <- HorizontalAlignment.Left
        result.Text <- "Результат: 0.8660"
        result.FontSize <- 16.0

        // Контейнер для кнопок
        let buttonPanel = StackPanel()
        buttonPanel.Orientation <- Orientation.Horizontal
        buttonPanel.Spacing <- 10.0
        buttonPanel.HorizontalAlignment <- HorizontalAlignment.Left

        // Кнопки
        let cosButton = Button(Content = "cos(x)", Width = 70.0)
        let sinButton = Button(Content = "sin(x)", Width = 70.0)
        let tanButton = Button(Content = "tan(x)", Width = 70.0)

        // Функция вычисления
        let compute trigFunc =
            try
                let x = float input.Text
                let radians = x * Math.PI / 180.0
                let value = trigFunc radians
                result.Text <- sprintf "Результат: %.4f" value
            with _ ->
                result.Text <- "Ошибка ввода"

        // Обработчики кнопок
        cosButton.Click.Add(fun _ -> compute Math.Cos)
        sinButton.Click.Add(fun _ -> compute Math.Sin)
        tanButton.Click.Add(fun _ -> compute Math.Tan)

        // Добавляем кнопки в панель
        buttonPanel.Children.Add(cosButton)
        buttonPanel.Children.Add(sinButton)
        buttonPanel.Children.Add(tanButton)

        // Добавляем элементы в основной контейнер
        mainStack.Children.Add(input)
        mainStack.Children.Add(result)
        mainStack.Children.Add(buttonPanel)

        this.Content <- mainStack

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add(FluentTheme())

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            desktop.MainWindow <- MainWindow()
        | _ -> ()

[<EntryPoint>]
let main argv =
    AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(argv)


open System
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Layout
open Avalonia.Media
open Avalonia.Themes.Fluent

type MainWindow() as this =
    inherit Window()
    do
        this.Title <- "Avalonia ListBox Example"
        this.Width <- 300.0
        this.Height <- 400.0
        this.Padding <- Thickness(10.0)

        let mainStack = StackPanel()
        mainStack.Spacing <- 10.0

        let label = TextBlock()
        label.Text <- "Массив из первых 100 натуральных чисел,\nкратных 13 или 17:"
        label.FontSize <- 14.0
        label.Margin <- Thickness(0.0, 0.0, 0.0, 10.0)

        let listBox = ListBox()
        listBox.Width <- 240.0
        listBox.Height <- 300.0
        listBox.Margin <- Thickness(0.0, 0.0, 0.0, 10.0)

        let array =
            Seq.initInfinite (fun num -> num + 1) 
            |> Seq.filter (fun x -> x % 13 = 0 || x % 17 = 0)
            |> Seq.truncate 100
            |> Seq.toArray

        for n in array do
            listBox.Items.Add(n) |> ignore

        mainStack.Children.Add(label)
        mainStack.Children.Add(listBox)

        this.Content <- mainStack

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add(FluentTheme())

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            desktop.MainWindow <- MainWindow()
        | _ -> ()

[<EntryPoint>]
let main argv =
    AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(argv)

open System
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Layout
open Avalonia.Media
open Avalonia.Themes.Fluent

type MainWindow() as this =
    inherit Window()
    do
        this.Title <- "List Mirror"
        this.Width <- 400.0
        this.Height <- 200.0
        this.Padding <- Thickness(20.0)

        let mainStack = StackPanel()
        mainStack.Spacing <- 10.0

        let label1 = TextBlock()
        label1.Text <- "Введите список:"
        label1.FontSize <- 14.0

        let inputBox = TextBox()
        inputBox.Width <- 350.0

        let mirrorButton = Button()
        mirrorButton.Content <- "Отзеркалить"
        mirrorButton.Width <- 100.0
        mirrorButton.HorizontalAlignment <- HorizontalAlignment.Center

        let label2 = TextBlock()
        label2.Text <- "Результат:"
        label2.FontSize <- 14.0

        let label3 = TextBlock()
        label3.Width <- 350.0
        label3.Height <- 50.0
        label3.TextWrapping <- TextWrapping.Wrap

        mirrorButton.Click.Add(fun _ ->
            let text = 
                inputBox.Text.Split([|','; ';'; ' '|], StringSplitOptions.RemoveEmptyEntries) 
                |> Array.map (fun str -> str.Trim()) 
                |> Array.rev 
                |> String.concat " "
            label3.Text <- text
        )

        mainStack.Children.Add(label1)
        mainStack.Children.Add(inputBox)
        mainStack.Children.Add(mirrorButton)
        mainStack.Children.Add(label2)
        mainStack.Children.Add(label3)

        this.Content <- mainStack

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add(FluentTheme())

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            desktop.MainWindow <- MainWindow()
        | _ -> ()

[<EntryPoint>]
let main argv =
    AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .LogToTrace()
        .StartWithClassicDesktopLifetime(argv)

open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Layout
open Avalonia.Themes.Fluent

type MainWindow() as this =
    inherit Window()
    do
        this.Title <- "ProgressBar"
        this.Width <- 600.0
        this.Height <- 100.0

        // Прогресс-бар (прижат к низу)
        let progressBar = ProgressBar()
        progressBar.Maximum <- 50.0
        progressBar.Value <- 0.0
        progressBar.Height <- 20.0
        DockPanel.SetDock(progressBar, Dock.Bottom)

        // Текстовое поле
        let textBox = TextBox()
        textBox.Width <- 300.0
        textBox.Height <- 30.0
        textBox.Margin <- Thickness(150.0, 20.0, 0.0, 0.0)
        textBox.MaxLength <- 25

        // Обработчик изменений текста
        let changes _ =
            let value = float (textBox.Text.Length * 2)
            progressBar.Value <- min value progressBar.Maximum

        textBox.TextChanged.Add(changes)

        // Контейнер для элементов
        let panel = DockPanel()
        panel.Children.Add(textBox)
        panel.Children.Add(progressBar)
        this.Content <- panel

type App() =
    inherit Application()
    override this.Initialize() =
        this.Styles.Add(FluentTheme())
    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktop ->
            desktop.MainWindow <- MainWindow()
        | _ -> ()

[<EntryPoint>]
let main argv =
    AppBuilder
        .Configure<App>()
        .UsePlatformDetect()
        .StartWithClassicDesktopLifetime(argv)