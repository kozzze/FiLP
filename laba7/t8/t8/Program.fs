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