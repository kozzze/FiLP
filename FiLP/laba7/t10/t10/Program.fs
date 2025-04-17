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