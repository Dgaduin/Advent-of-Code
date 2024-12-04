open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "input.txt"

let split (input: string) =
    input.Split([| ' '; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)

let parse0 input = input |> split

let parse2d input =
    input |> split |> Array.map Seq.toArray |> array2D

let parse90 input =
    input
    |> parse2d
    |> fun x ->
        let cols = Array2D.length2 x - 1

        [| for i in 0..cols do
               x[*, i] |> String |]

let parse45 input =
    input
    |> parse2d
    |> fun x ->
        let cols = Array2D.length2 x - 1
        let rows = Array2D.length1 x - 1

        [| for d in 0 .. cols + rows do
               [| for r in 0..d do
                      let c = d - r

                      if r <= rows && c <= cols then
                          yield x[r, c] |]
               |> String |]

let parse135 input =
    input
    |> parse2d
    |> fun x ->
        let cols = Array2D.length2 x - 1
        let rows = Array2D.length1 x - 1

        [| for d in 0 .. cols + rows do
               [| for r in 0..d do
                      let c = cols - d + r

                      if r <= rows && c >= 0 then

                          yield x[r, c] |]
               |> String |]

let task1Regex = Regex @"(?=(XMAS|SAMX))"

let getCountPerLine line =
    line |> task1Regex.Matches |> Seq.length

let getScorePerRotation a =
    a |> Array.map getCountPerLine |> Array.sum

let task1Partial x f =
    f |> List.map (fun y -> y x) |> List.map getScorePerRotation |> List.sum

let funcs = [ parse0; parse45; parse90; parse135 ]

task1Partial input funcs |> printfn "Task 1:%A"

let task2Regex = Regex @"(?=(MAS|SAM))"

let task2Test (a: char array2d) =
    let d45 = [| a[0, 2]; a[1, 1]; a[2, 0] |] |> String |> task2Regex.Matches
    let d135 = [| a[0, 0]; a[1, 1]; a[2, 2] |] |> String |> task2Regex.Matches
    d45.Count + d135.Count = 2

let task2 input =
    input
    |> parse2d
    |> fun x ->
        let cols = Array2D.length2 x - 2
        let rows = Array2D.length1 x - 2

        [ for i in 1..rows do
              for j in 1..cols do
                  task2Test x[i - 1 .. i + 1, j - 1 .. j + 1]
                  |> function
                      | true -> 1
                      | false -> 0 ]
    |> List.sum

input |> task2 |> printfn "Task 2:%A"
