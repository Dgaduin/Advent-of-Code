open System
open System.IO

let input = File.ReadAllLines "input.txt" |> Array.toList

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

let isDone a = Seq.last a = 'Z'

let rec walk path (map: Map<string, string[]>) root stepCount =
    let next (field, count, stopped) step =
        if stopped then
            (field, count, stopped)
        else
            let ret = map.[field].[step]

            if isDone ret then
                (ret, count + 1, true)
            else
                (ret, count + 1, false)

    let (field, count, stopped) = path |> Seq.fold next (root, stepCount, false)
    if stopped then count else walk path map field count

walk path map "AAA" 0 |> printfn "%d"

let rec gcd (a: int64) (b: int64) = if b = 0 then abs a else gcd b (a % b)
let lcmSimple a b = a * b / (gcd a b)

let rec lcm =
    function
    | [ a; b ] -> lcmSimple a b
    | head :: tail -> lcmSimple (head) (lcm (tail))
    | [] -> 1

map
|> Map.keys
|> Seq.filter (fun k -> Seq.last k = 'A')
|> Seq.toList
|> List.map (fun root -> walk path map root 0)
|> List.map int64
|> lcm
|> printfn "%A"
