open System
open System.IO

let input = File.ReadAllLines "input.txt" |> Array.toList

let testInput =
    [ "RL"
      ""
      "AAA = (BBB, CCC)"
      "BBB = (DDD, EEE)"
      "CCC = (ZZZ, GGG)"
      "DDD = (DDD, DDD)"
      "EEE = (EEE, EEE)"
      "GGG = (GGG, GGG)"
      "ZZZ = (ZZZ, ZZZ)" ]



let parseInput (input: string list) =
    let path =
        input
        |> List.head
        |> Seq.map (function
            | 'R' -> 1
            | _ -> 0)

    let parseLine (line: string) =
        let parts =
            line.Split([| '='; ' '; '('; ')'; ',' |], StringSplitOptions.RemoveEmptyEntries)

        let name = parts.[0]
        let children = [| parts.[1]; parts.[2] |]
        (name, children)

    let map = input |> List.skip 2 |> List.map parseLine |> Map.ofList
    (path, map)

let (path, map) = parseInput input

let rec walk path (map: Map<string, string[]>) root stepCount =
    let next (field, count, stopped) step =
        if stopped then
            (field, count, stopped)
        else
            let ret = map.[field].[step]

            if ret = "ZZZ" then
                ("ZZZ", count + 1, true)
            else
                (ret, count + 1, false)

    let (field, count, stopped) = path |> Seq.fold next (root, stepCount, false)
    if stopped then count else walk path map field count

// walk path map "AAA" 0 |> printfn "%d"

let roots = map |> Map.keys |> Seq.filter (fun k -> Seq.last k = 'A') |> Seq.toList

let rec walk2 (path: seq<int>) (map: Map<string, string[]>) roots stepCount =
    let next field step = map.[field].[step]

    let pathFolding (roots, count, stopped) step =
        if stopped then
            (roots, count, stopped)
        else
            let newRoots = roots |> List.map (fun root -> next root step)
            let isDone = newRoots |> List.forall (fun root -> Seq.last root = 'Z')

            if isDone then
                (newRoots, count + 1, true)
            else
                (newRoots, count + 1, false)

    let (roots, count, completed) =
        path |> Seq.fold pathFolding (roots, stepCount, false)

    if completed then count else walk2 path map roots count
