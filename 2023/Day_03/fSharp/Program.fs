open System
open System.IO

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

let dot = "."
let padString s = String.concat "" [ dot; s; dot ]
let createPad n = String.init (n + 2) (fun y -> dot)

let padInput (input: list<string>) =
    let padHorizontal = [ createPad input[0].Length ]
    let padVertical = input |> List.map padString
    List.concat [ padHorizontal; padVertical; padHorizontal ]

let paddedInput = input |> padInput

let iterCollection (coll: list<string>) =
    seq {
        for i = 1 to coll.Length - 2 do
            for j = 1 to coll[0].Length - 2 do
                yield (coll[i].[j], i, j)

        yield ('a', 0, 0)
    }

let iterSet = paddedInput |> iterCollection

let getNeighbours (coll: list<string>) (i: int, j: int) =
    seq {
        for i' in -1 .. 1 do
            for j' in -1 .. 1 do
                yield ((i + i'), (j + j'), coll[i + i'].[j + j'])
    }

let checkNeighbours (coll: list<string>) (i, j) =
    getNeighbours coll (i, j)
    |> Seq.map (fun (i'', j'', c) -> c <> '.' && not ((i'' - i) = 0 && Char.IsDigit c))
    |> Seq.fold (fun acc a -> acc || a) false

let task1Fold coll (numberBuffer, flag, sum) (c, i, j) =
    if Char.IsDigit c then
        (List.append numberBuffer [ c ], flag || (checkNeighbours coll) (i, j), sum)
    else
        let value =
            if numberBuffer.Length > 0 then
                String(Array.ofList numberBuffer) |> Int32.Parse
            else
                0

        if flag then
            (List.empty, false, sum + value)
        else
            (List.empty, false, sum)

let (_, _, result) =
    iterSet |> Seq.fold (task1Fold paddedInput) (List.empty, false, 0)

printfn "%i" result

let extractNumbers (numberBuffer, coords, coordBuffer) (c, i: int, j: int) =
    if Char.IsDigit c then
        (List.append numberBuffer [ c ], coords, List.append coordBuffer [ (i, j) ])
    else if numberBuffer.Length > 0 then
        let number = String(Array.ofList numberBuffer) |> Int32.Parse
        let newCoords = coordBuffer |> List.map (fun (i', j') -> (i', j', number))
        let foldFn (acc: Map<(int * int), int>) (i', j', value) = acc.Add((i', j'), value)
        let updatedCoords = newCoords |> List.fold foldFn coords

        (List.empty, updatedCoords, List.empty)
    else
        (List.empty, coords, coordBuffer)

let task2Fold (map: Map<(int * int), int>) coll sum (c, i, j) =
    if c = '*' then
        let neighbours = getNeighbours coll (i, j)

        let numberNeighbours =
            neighbours
            |> Seq.filter (fun (_, _, c') -> Char.IsDigit c')
            |> Seq.map (fun (i', j', c') -> map[(i', j')])
            |> Seq.distinct
            |> List.ofSeq

        if numberNeighbours.Length = 2 then
            sum + (numberNeighbours[0] * numberNeighbours[1])
        else
            sum
    else
        sum

let (_, numberMap, _) =
    Seq.fold extractNumbers (List.empty, Map.empty, List.empty) iterSet

let result2 = iterSet |> Seq.fold (task2Fold <| numberMap <| paddedInput) 0

printfn "%i" result2
