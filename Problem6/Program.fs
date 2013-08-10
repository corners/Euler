// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let n = [1..100]
    let ``sum of squares`` = n |> Seq.map (fun x -> x*x)
                                |> Seq.sum
    let sum = n |> Seq.sum
    let ``square of sum`` = sum * sum
              
    printfn "square of sum - sum of squares = %A - %A = %A" ``square of sum`` ``sum of squares`` (``square of sum`` - ``sum of squares``)
    0 // return an integer exit code
