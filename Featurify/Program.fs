// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    let handler = Handlers.conn;
    Console.ReadLine() |> ignore
    0 // return an integer exit code
