open System
open System.IO

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

type Line = { Id: int; Draws: Map<string, int> }

let extractDraws (s: string) =
    let mapDraws (draws: string[]) =
        let mapSingleDraw (draw: string) =
            draw.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            |> fun x -> (x[1], x[0] |> Int32.Parse)

        Array.map mapSingleDraw draws

    s.Split ',' |> mapDraws

let extractLine (line: string) =
    let splitOptions =
        StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries

    let split = line.Split([| ':'; ';' |], splitOptions)

    let id = split[0].Split(' ')[1] |> Int32.Parse

    let draws =
        Array.map extractDraws split[1..]
        |> Array.collect (fun x -> x)
        |> Array.groupBy (fun (key, _) -> key)
        |> Array.map (fun (key, values) -> (key, Array.maxBy snd values |> snd))
        |> Map.ofArray

    { Id = id; Draws = draws }

let checkIfPossible (line: Line) =
    line.Draws["green"] > 13 || line.Draws["blue"] > 14 || line.Draws["red"] > 12

let lines = List.map extractLine input

//Task 1
lines
|> List.filter checkIfPossible
|> List.sumBy (fun x -> x.Id)
|> printfn "%d"

//Task 2

let getPower (line: Line) =
    line.Draws["green"] * line.Draws["red"] * line.Draws["blue"]

lines |> List.map getPower |> List.sum |> printfn "%d"
