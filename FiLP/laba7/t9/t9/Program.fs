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