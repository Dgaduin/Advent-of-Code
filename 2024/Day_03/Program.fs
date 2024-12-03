open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "input.txt"

let calculateScore (m: Match) =
    m.Groups
    |> Seq.tail
    |> Seq.filter (fun x -> x.Length <> 0)
    |> Seq.fold (fun prod g -> int g.Value * prod) 1

let regexTask1 = Regex @"mul\((\d{1,3}),(\d{1,3})\)"

input
|> regexTask1.Matches
|> Seq.fold (fun x m -> x + calculateScore m) 0
|> printfn "Task 1: %A"

let regexTask2 = Regex @"mul\((\d{1,3}),(\d{1,3})\)|(don't\(\))|(do\(\))"

input
|> regexTask2.Matches
|> Seq.fold
    (fun (on, sum) m ->
        m.Value
        |> function
            | "do()" -> true, sum
            | "don't()" -> false, sum
            | _ when on -> on, sum + calculateScore m
            | _ -> on, sum)
    (true, 0)
|> snd
|> printfn "Task2: %A"
