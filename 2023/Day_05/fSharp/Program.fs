open System
open System.IO

//let input = File.ReadAllLines "input.txt"
let testInput =
    [ "seeds: 79 14 55 13"
      ""
      "seed-to-soil map:"
      "50 98 2"
      "52 50 48"
      ""
      "soil-to-fertilizer map:"
      "0 15 37"
      "37 52 2"
      "39 0 15"
      ""
      "fertilizer-to-water map:"
      "49 53 8"
      "0 11 42"
      "42 0 7"
      "57 7 4"
      ""
      "water-to-light map:"
      "88 18 7"
      "18 25 70"
      ""
      "light-to-temperature map:"
      "45 77 23"
      "81 45 19"
      "68 64 13"
      ""
      "temperature-to-humidity map:"
      "0 69 1"
      "1 0 69"
      ""
      "humidity-to-location map:"
      "60 56 37"
      "56 93 4" ]

let parseInput (input: string list) =
    let seeds =
        input[0]
        |> (fun x -> x.Split([| "seeds: "; " " |], StringSplitOptions.RemoveEmptyEntries))
        |> Array.map int

    let groups =
        input
        |> List.filter (fun x -> x <> "")
        |> fun x -> x[2..] @ [ "pad pad" ]
        |> List.fold
            (fun (acc, buff) (x: string) ->
                let split =
                    x.Split([| " "; "\t" |], StringSplitOptions.RemoveEmptyEntries) |> Array.toList

                if split.Length = 2 then
                    (acc @ [ buff ], [])
                else
                    (acc, buff @ [ (split[0],split[1],split[2]) ]))
            ([], [])
        |> fst

    groups


let x = testInput |> parseInput
