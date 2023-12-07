open System
open System.IO

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

let parseInput (input: string list) =
    let seeds =
        input[0]
        |> (fun x -> x.Split([| "seeds: "; " " |], StringSplitOptions.RemoveEmptyEntries))
        |> Array.map int64
        |> List.ofArray

    let groups =
        input
        |> List.filter (fun x -> x <> "")
        |> fun x -> x[2..] @ [ "pad pad" ]
        |> List.fold
            (fun (acc, buff) (x: string) ->
                let split = x.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.toList

                if split.Length = 2 then
                    (acc @ [ buff ], [])
                else
                    (acc, buff @ [ (int64 split[0], int64 split[1], int64 split[2]) ]))
            ([], [])
        |> fst

    (seeds, groups)

let (seeds, groups) = input |> parseInput

let task1Fn (a, b, c) =
    function
    | i when i >= b && i < b + c -> -(a + (i - b))
    | i -> i

let noop x = x
let innerFold acc a = acc >> task1Fn a
let outerFold acc a = acc >> a >> abs

let fnSum l =
    l |> List.map (List.fold innerFold noop) |> List.fold outerFold noop

seeds |> List.map (fnSum groups) |> List.min |> printfn "%i"

let task2Seeds =
    seeds
    |> List.chunkBySize 2
    |> List.map (fun x -> (x[0], (x[1] + x[0])))
    |> List.sort

let changeFn x y = fun n -> n - (y - x)

let pad coll =
    let (r, l) =
        List.fold
            (fun (acc, last: int64) (x, y, z) ->
                if x = last + int64 1 || x = 0 then
                    ((x, y, z) :: acc, y)
                else
                    ((x, y, z) :: (last, x - int64 1, noop) :: acc), y)
            (list.Empty, int64 0)
            coll

    (List.rev r) @ [ (l + int64 1, Int64.MaxValue, noop) ]

let task2Groups =
    groups
    |> List.map (List.map (fun (x, y, z) -> (y, (y + z) - int64 1, changeFn x y)))
    |> List.map (List.sortBy (fun (x, y, z) -> x))
    |> List.map pad

printfn "%i" task2Seeds.Length
printfn "%i" task2Groups.Length

let checkBounds x y x' y' =
    (x >= x' && x < y', y >= x' && y < y', (x < x' && y > y'))

let mapBounds (x: int64) (y: int64) (x': int64) (y': int64) (z: int64 -> int64) =
    checkBounds x y x' y'
    |> function
        | true, true, _ -> [ (z x, z y) ]
        | true, false, _ -> [ (z x, z y') ]
        | false, true, _ -> [ (z x', z y) ]
        | false, false, false -> []
        | false, false, true -> [ (x, x' - int64 1); (z x', z y'); (y' + int64 1, y) ]

let mapSourceToDestination (x, y) coll =
    List.map (fun (x', y', z') -> mapBounds x y x' y' z') coll |> List.collect noop

let mapSourceToDestinationMulti source filter =
    source |> List.iter (fun (x, y) -> printf "%i-%i:" x y)
    printfn ""

    source
    |> List.map mapSourceToDestination
    |> List.map (fun x -> x filter)
    |> List.collect noop
    |> List.distinct
    |> List.sort

task2Groups
|> List.fold (fun acc x -> mapSourceToDestinationMulti acc x) task2Seeds
|> List.sort
|> List.take 5
|> List.iter (fun (x, y) -> printfn "%i:%i;" x y)
