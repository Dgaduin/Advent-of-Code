open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "input.txt"

let parseMap (input: string) =
    input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    |> Array.map Seq.toArray
    |> array2D

let getStart input =
    [ for i in 0 .. Array2D.length1 input - 1 do
          for j in 0 .. Array2D.length2 input - 1 do
              if input[i, j] = '^' then
                  yield i, j ]
    |> List.head

let map = parseMap input
let x', y' = getStart map

let navCheck x y =
    try
        Some(map[x, y], x, y)
    with _ ->
        None

type Move =
    | Up of x: int * y: int
    | Right of x: int * y: int
    | Down of x: int * y: int
    | Left of x: int * y: int

let turn move =
    move
    |> function
        | Up(x, y) -> Right(x, y)
        | Right(x, y) -> Down(x, y)
        | Down(x, y) -> Left(x, y)
        | Left(x, y) -> Up(x, y)

let step move =
    move
    |> function
        | Up(x, y) ->
            navCheck (x - 1) y
            |> function
                | Some(c, i, j) ->
                    match c with
                    | '#' -> Some(turn move)
                    | _ -> Some(Up(i, j))
                | None -> None
        | Right(x, y) ->
            navCheck x (y + 1)
            |> function
                | Some(c, i, j) ->
                    match c with
                    | '#' -> Some(turn move)
                    | _ -> Some(Right(i, j))
                | None -> None
        | Down(x, y) ->
            navCheck (x + 1) y
            |> function
                | Some(c, i, j) ->
                    match c with
                    | '#' -> Some(turn move)
                    | _ -> Some(Down(i, j))
                | None -> None
        | Left(x, y) ->
            navCheck x (y - 1)
            |> function
                | Some(c, i, j) ->
                    match c with
                    | '#' -> Some(turn move)
                    | _ -> Some(Left(i, j))
                | None -> None

let testLoop (input: char array2d) move stack =
    let findBlock v = Array.tryFindIndex (fun c -> c = '#') v

    move
    |> function
        | Up(a, b) ->
            let vector = input[a, (b + 1) ..]

            findBlock vector
            |> function
                | Some s -> List.contains ((a, b + s)) stack
                | None -> false
        | Right(a, b) ->
            let vector = input[(a + 1) .., b]

            findBlock vector
            |> function
                | Some s -> List.contains ((a + s, b)) stack
                | None -> false
        | Down(a, b) ->
            let vector = input[a, 0 .. (b - 1)] |> Array.rev

            findBlock vector
            |> function
                | Some s -> List.contains ((a, b - s)) stack
                | None -> false
        | Left(a, b) ->
            let vector = input[0 .. (a - 1), b] |> Array.rev

            findBlock vector
            |> function
                | Some s -> List.contains ((a - s, b)) stack
                | None -> false

let rec navigateMap (input: char array2d) move stack l =
    let a, b =
        move
        |> function
            | Up(x, y) -> x, y
            | Right(x, y) -> x, y
            | Down(x, y) -> x, y
            | Left(x, y) -> x, y

    let newL = if testLoop input move stack then l + 1 else l

    step move
    |> function
        | Some m -> navigateMap input m (stack @ [ (a, b) ]) newL
        | None -> stack @ [ (a, b) ], newL



let (test, score) = navigateMap map (Up(x', y')) [] 0

test |> Set.ofList |> Set.count |> printfn "Task 1: %A"
printfn "Task 2: %A" score
