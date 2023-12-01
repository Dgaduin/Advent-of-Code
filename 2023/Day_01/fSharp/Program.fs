open System
open System.IO

// Setup
let substitutes =
    Map
        [ ("one", 1)
          ("1", 1)
          ("two", 2)
          ("2", 2)
          ("three", 3)
          ("3", 3)
          ("four", 4)
          ("4", 4)
          ("five", 5)
          ("5", 5)
          ("six", 6)
          ("6", 6)
          ("seven", 7)
          ("7", 7)
          ("eight", 8)
          ("8", 8)
          ("nine", 9)
          ("9", 9) ]

let input = File.ReadAllLines "../src/input.txt" |> List.ofArray

// Util
let isDigit (c: char) = Char.IsDigit c
let toNumber (c: char) = int c - int '0'
let toResult (i: int, j: int) = i * 10 + j
let together f g x = (f x, g x)

// TASK 1
let get findFn (s: string) = findFn isDigit s |> toNumber

let getCalibration1 (s: string) =
    together (get Seq.find) (get Seq.findBack) s |> toResult

// Task 1 Result
List.map getCalibration1 input |> List.sum |> printf "%i\n"

// TASK 2
let findKey (baseString: string) (searchString: string) =
    (baseString.IndexOf searchString, searchString)

let findLastKey (baseString: string) (searchString: string) =
    (baseString.LastIndexOf searchString, searchString)

let get2 indexFn orderingFn (s: string) =
    Seq.map (indexFn s) substitutes.Keys
    |> Seq.filter (fun (index, digitWord) -> index <> -1)
    |> orderingFn fst
    |> fun x -> substitutes[snd x]

let getFirst2 (s: string) = get2 findKey Seq.minBy s
let getLast2 (s: string) = get2 findLastKey Seq.maxBy s

let getCalibration2 (x: string) =
    together getFirst2 getLast2 x |> toResult

// Task 2 Result
List.map getCalibration2 input |> List.sum |> printf "%i\n"
