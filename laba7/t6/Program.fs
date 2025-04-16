open System.Windows.Forms

let Form = new Form(Width = 600, Height = 100, Text = "ProgressBar")

let ProgressBar1 = new ProgressBar(Dock = DockStyle.Bottom, Maximum = 50)
let TextBox1 = new TextBox(Width = 300, Height = 100, Top = 20, Left = 150, MaxLength = 25)

let changes _ =
    let value = TextBox1.TextLength * 2
    ProgressBar1.Value <- min value ProgressBar1.Maximum

TextBox1.TextChanged.Add(changes)

Form.Controls.Add(TextBox1)
Form.Controls.Add(ProgressBar1)

[<EntryPoint>]
let main argv =
    Application.Run(Form)
    0