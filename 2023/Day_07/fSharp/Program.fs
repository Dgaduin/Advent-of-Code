open System
open System.IO

let input = File.ReadAllLines "../input.txt" |> Array.toList
let testInput = [ "32T3K 765"; "T55J5 684"; "KK677 28"; "KTJJT 220"; "QQQJA 483" ]

let parseInput (input: string list) =
    input
    |> List.map (fun line -> line.Split [| ' ' |])
    |> List.map (fun line -> (line.[0], int line.[1]))

let parsedInput = parseInput input

let cardMapTask1 =
    function
    | a when Char.IsDigit a -> a
    | 'T' -> 'B'
    | 'J' -> 'C'
    | 'Q' -> 'D'
    | 'K' -> 'E'
    | 'A' -> 'F'
    | _ -> '0'

let handValueFn =
    function
    | [ 5 ] -> 7
    | [ 4; 1 ] -> 6
    | [ 3; 2 ] -> 5
    | [ 3; 1; 1 ] -> 4
    | [ 2; 2; 1 ] -> 3
    | [ 2; 1; 1; 1 ] -> 2
    | [ 1; 1; 1; 1; 1 ] -> 1
    | _ -> 0

let totalHandValueFn (s: string) =
    let handValue =
        s
        |> Seq.groupBy (fun c -> c)
        |> Seq.map (fun (c, s) -> (Seq.length s))
        |> Seq.sortDescending
        |> Seq.toList
        |> handValueFn

    let cardValue = Seq.map cardMapTask1 s |> Seq.toArray |> System.String
    String.Concat(handValue, cardValue)

let task1 (input: (string * int) list) =
    input
    |> List.map (fun (hand, bid) -> (bid, totalHandValueFn hand))
    |> List.sortBy snd
    |> List.fold (fun (sum, i) (bid, _) -> (sum + (bid * i), i + 1)) (0, 1)


let cardMapTask2 =
    function
    | a when Char.IsDigit a -> a
    | 'T' -> 'B'
    | 'J' -> '0'
    | 'Q' -> 'D'
    | 'K' -> 'E'
    | 'A' -> 'F'
    | _ -> '0'

// Hand value / Number of jokers -> new hand value
let handValueFnTask2 =
    function
    | (6, 1)
    | (6, 4)
    | (5, 3)
    | (5, 2) -> 7
    | (4, 3)
    | (4, 1)
    | (3, 2) -> 6
    | (3, 1) -> 5
    | (2, 2)
    | (2, 1) -> 4
    | (1, 1) -> 2
    | (x, _) -> x

let totalHandValueFnTask2 (s: string) =
    let jCount = s |> Seq.filter (fun c -> c = 'J') |> Seq.length

    let handValue =
        s
        |> Seq.groupBy (fun c -> c)
        |> Seq.map (fun (c, s) -> (Seq.length s))
        |> Seq.sortDescending
        |> Seq.toList
        |> handValueFn
        |> fun x -> handValueFnTask2 (x, jCount)

    let cardValue = Seq.map cardMapTask2 s |> Seq.toArray |> System.String
    String.Concat(handValue, cardValue)

let task2 (input: (string * int) list) =
    input
    |> List.map (fun (hand, bid) -> (bid, totalHandValueFnTask2 hand))
    |> List.sortBy snd
    |> List.fold (fun (sum, i) (bid, _) -> (sum + (bid * i), i + 1)) (0, 1)

task1 parsedInput |> printfn "%A"
task2 parsedInput |> printfn "%A"
