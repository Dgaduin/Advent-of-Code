open System
open System.IO

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

// let testInput =
//     [ "467..114.."
//       "...*......"
//       "..35..633."
//       "......#..."
//       "617*......"
//       ".....+.58."
//       "..592....."
//       "......755."
//       "...$.*...."
//       ".664.598.." ]

let dot = "."
let padString (s) = String.concat "" [ dot; s; dot ]
let createPad (n) = String.init (n + 2) (fun y -> dot)

let padInput (input: list<string>) =
    let padHorizontal = [ createPad input[0].Length ]
    let padVertical = input |> List.map padString
    List.concat [ padHorizontal; padVertical; padHorizontal ]

let paddedInput = input |> padInput

let iterCollection =
    seq {
        for i = 1 to paddedInput.Length - 2 do
            for j = 1 to paddedInput[0].Length - 2 do
                yield (paddedInput[i].[j], i, j)

        yield ('a', 0, 0)
    }

let checkNeighbours (i, j) =
    let collection =
        seq {
            for i' in -1 .. 1 do
                for j' in -1 .. 1 do
                    yield
                        paddedInput[i + i'].[j + j'] <> '.'
                        && not (i' = 0 && Char.IsDigit paddedInput[i + i'].[j + j'])
        }

    collection |> Seq.fold (fun acc a -> acc || a) false

let foldFields (numberBuffer, flag, sum) (c, i, j) =
    if Char.IsDigit c then
        (List.append numberBuffer [ c ], flag || checkNeighbours (i, j), sum)
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

let (_, _, result) = Seq.fold foldFields (List.empty, false, 0) iterCollection

printfn "%i" result


let extractNumbers (numberBuffer, coords: Map<(int * int), int>, coordBuffer) (c, i: int, j: int) =
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

let getNeighbours (i: int, j: int) (coll: list<string>) =
    seq {
        for i' in -1 .. 1 do
            for j' in -1 .. 1 do
                yield ((i + i'), (j + j'), coll[i + i'].[j + j'])
    }

let foldFieldsTask2 (map: Map<(int * int), int>) coll sum (c, i, j) =
    if c = '*' then
        let neighbours = getNeighbours (i, j) coll

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
    Seq.fold extractNumbers (List.empty, Map.empty, List.empty) iterCollection

let result2 =
    iterCollection |> Seq.fold (foldFieldsTask2 <| numberMap <| paddedInput) 0

printfn "%i" result2
