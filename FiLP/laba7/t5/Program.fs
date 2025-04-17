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