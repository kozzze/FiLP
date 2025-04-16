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