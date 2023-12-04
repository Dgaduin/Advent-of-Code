open System
open System.IO

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

let testInput =
    [ "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"
      "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19"
      "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1"
      "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83"
      "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36"
      "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11" ]

let toInt = Int32.Parse

let split (c: char) (s: string) =
    s.Split(c, StringSplitOptions.RemoveEmptyEntries)

let splitOnSpace s = split ' ' s

let parseInput (input: list<string>) =
    input
    |> List.map (fun x ->
        let initSplit = split ':' x
        let gameId = (initSplit[0] |> splitOnSpace).[1] |> toInt
        let gameSplit = initSplit[1] |> split '|'
        let winning = gameSplit[0] |> splitOnSpace |> Array.map toInt |> Set.ofArray
        let drawn = gameSplit[1] |> splitOnSpace |> Array.map toInt |> Set.ofArray
        (gameId, (Set.intersect winning drawn).Count))

let task1 (gameId: int, score: int) =
    if score = 0 then 0 else pown 2 <| score - 1

input |> parseInput |> List.sumBy task1 |> printfn "%i"


let task2 (map: Map<int, int>, sum: int) (gameId: int, score: int) =
    let multi = map[gameId]

    if score > 0 then
        let extraCounts =
            seq {
                for i = gameId + 1 to gameId + score do
                    yield i
            }

        let t a =
            if map.ContainsKey a then
                (a, map[a] + multi)
            else
                (a, multi)

        let newMap =
            Seq.map t extraCounts |> Seq.fold (fun acc (a, b) -> Map.add a b acc) map

        (newMap, sum + multi)
    else
        (map, sum + multi)

let seedMap = seq { for i in 1 .. input.Length -> (i, 1) }

input |> parseInput |> List.fold task2 (Map(seedMap), 0) |> snd |> printfn "%i"
