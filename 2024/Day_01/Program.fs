open System
open System.IO

let input = File.ReadAllText "input.txt"

let task1InputCleanup (l: array<int * int>) =
    Array.map (fun (i, x) -> x) l |> Array.sort

let inputParse (s: string) =
    s.Split([| ' '; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map int
    |> Array.indexed
    |> Array.partition (fun (i, x) -> i % 2 = 0)
    |> fun (a, b) -> task1InputCleanup a, task1InputCleanup b

let coll1, coll2 = input |> inputParse

let task1 = Array.zip coll1 coll2 |> Array.sumBy (fun (a, b) -> abs (a - b))

let scoreMap =
    coll2
    |> Array.groupBy (fun x -> x)
    |> Array.map (fun (i, n) -> i, Array.length n)
    |> Map.ofArray

let scoreLookup (i: int) =
    scoreMap
    |> Map.tryFind i
    |> function
        | Some(x) -> x
        | None -> 0

let task2 = coll1 |> Array.map (fun x -> x * scoreLookup x) |> Array.sum

task2 |> printfn "Task 2 actual input: %i"
task1 |> printfn "Task 1 actual input: %i"
