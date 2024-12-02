open System
open System.IO

let input =
    File.ReadAllLines "input.txt"
    |> Array.map (fun x -> x.Split(' ') |> Array.map int)

type ScoreTask1 =
    | Inc
    | Dec
    | Fail
    | Start

let incTest x = x > -4 && x < 0
let decTest x = x > 0 && x < 4

let notFail =
    function
    | Fail -> false
    | _ -> true

let scoreTask1 score item =
    match score with
    | Fail -> Fail
    | Start ->
        item
        |> function
            | x when incTest x -> Inc
            | x when decTest x -> Dec
            | _ -> Fail
    | Inc ->
        item
        |> function
            | x when incTest x -> Inc
            | _ -> Fail
    | Dec ->
        item
        |> function
            | x when decTest x -> Dec
            | _ -> Fail

let checkRowTask1 (a: int array) =
    a
    |> Array.pairwise
    |> Array.map (fun (a, b) -> a - b)
    |> Array.fold scoreTask1 Start

input
|> Array.map checkRowTask1
|> Array.where notFail
|> Array.length
|> printfn "Task 1: %A"

let generateIters (a: int list) =
    let n = List.length a

    [ for i = 0 to n - 1 do
          a[.. (i - 1)] @ a[(i + 1) .. n] ]


let checkRowTask2 a =
    Array.toList a
    |> generateIters
    |> List.map List.toArray
    |> List.map checkRowTask1
    |> List.reduce (fun a b ->
        match a, b with
        | Fail, Fail -> Fail
        | Fail, x -> x
        | x, Fail -> x
        | x, y -> x)

input
|> Array.map checkRowTask2
|> Array.where notFail
|> Array.length
|> printfn "Task 2: %A"

// This almost works - 2 remaining problems though around the same thing
// Solutions around removing the first item
// Solutions where you have a situation like 1,5,3,4 - the 1-5 gap triggers first
// and you never get to test the other approach
type ScoreTask2 =
    | Start of Prev: ScoreTask2 * Value: int * Cleared: bool
    | Inc of Prev: ScoreTask2 * Value: int * Cleared: bool
    | Dec of Prev: ScoreTask2 * Value: int * Cleared: bool
    | Fail of Prev: ScoreTask2
    | Noop

let prevRemap prev =
    prev
    |> function
        | Start(x, y, z) -> Start(x, y, true)
        | Inc(x, y, z) -> Inc(x, y, true)
        | Dec(x, y, z) -> Dec(x, y, true)
        | Fail x -> Fail x
        | Noop -> Noop

let rec task2Test state item =
    match state with
    | Fail x -> Fail state
    | Start(prev, value, cleared) ->
        item
        |> function
            | x when value = -1 -> Start(state, item, cleared)
            | x when incTest (value - item) -> Inc(state, item, cleared)
            | x when decTest (value - item) -> Dec(state, item, cleared)
            | x when cleared = false -> task2Test (prevRemap prev) item
            | _ -> Fail state
    | Inc(prev, value, cleared) ->
        item
        |> function
            | x when incTest (value - item) -> Inc(state, item, cleared)
            | x when cleared = false -> task2Test (prevRemap prev) item
            | _ -> Fail state
    | Dec(prev, value, cleared) ->
        item
        |> function
            | x when decTest (value - item) -> Dec(state, item, cleared)
            | x when cleared = false -> task2Test (prevRemap prev) item
            | _ -> Fail state
    | Noop -> Noop

let task2Init = Start(Noop, -1, false)
let task2FalseInit = Start(Noop, -1, true)

let prepTask2 (a: int array) =
    let x = a |> Array.fold task2Test task2Init

    //Ugly workaround for last item issues
    match x with
    | Fail y ->
        y
        |> function
            | Fail _ -> x
            | _ -> a |> Array.rev |> Array.tail |> Array.rev |> Array.fold task2Test task2FalseInit
    | _ -> x
