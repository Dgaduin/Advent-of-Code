open System
open System.IO

let testInput =
    "3   4
4   3
2   5
1   3
3   9
3   3"

let input = File.ReadAllText "input.txt"

let task1InputCleanup (l: list<int * int>) =
    List.map (fun (i, x) -> x) l |> List.sort

let inputParse (s: string) =
    s.Split([| ' '; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.map int
    |> Array.toList
    |> List.indexed
    |> List.partition (fun (i, x) -> i % 2 = 0)
    |> fun (a, b) -> (task1InputCleanup a, task1InputCleanup b)

let task1 (a: int list, b: int list) =
    List.zip a b |> List.map (fun (a, b) -> abs (a - b)) |> List.sum

testInput |> inputParse |> task1 |> printfn "Task 1 test input: %i"
input |> inputParse |> task1 |> printfn "Task 1 actual input: %i"

let list1, list2 = input |> inputParse

let task2ScoreLookupMap (list: int list) =
    List.groupBy (fun n -> n) list
    |> List.map (fun (i, a) -> (i, List.length a))
    |> Map.ofList

let scoreMap = list2 |> task2ScoreLookupMap

let scoreLookup (i: int) =
    scoreMap
    |> Map.tryFind i
    |> function
        | Some(t) -> t
        | None -> 0

let task2 (list: int list) =
    list |> List.map (fun x -> x * scoreLookup x) |> List.sum

list1 |> task2 |> printfn "Task 2 test input: %i"
list1 |> task2 |> printfn "Task 2 actual input: %i"
