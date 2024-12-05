open System
open System.IO
open System.Text.RegularExpressions

let input = File.ReadAllText "input.txt"

let parseRules (s: string) =
    s.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun s1 ->
        s1.Split('|', StringSplitOptions.RemoveEmptyEntries)
        |> fun s2 -> int s2[0], int s2[1])

let parsePages (s: string) =
    s.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
    |> Array.map (fun s1 -> s1.Split(',', StringSplitOptions.RemoveEmptyEntries) |> Array.map int)

let rulesBase, pagesBase =
    input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
    |> fun x -> parseRules x[0], parsePages x[1]

let rec folderTask1 rules page =
    match page with
    | head :: tail ->
        let z =
            rules
            |> Array.filter (fun (a, _, _) -> a = head)
            |> Array.map (fun (_, b, _) -> b)
            |> set

        Set.isSubset z (set tail) && folderTask1 rules tail
    | [] -> true

//sets are sorted sets for some damn reason
let rules =
    rulesBase
    |> Array.map (fun (a, b) ->
        a,
        b,
        seq {
            a
            b
        }
        |> set)

let rulePass page rules =
    Array.filter (fun (_, _, s) -> Set.isSubset s (set page)) rules

let passed, failed =
    pagesBase
    |> Array.partition (fun page -> folderTask1 (rulePass page rules) (List.ofArray page))

passed
|> Array.map (fun x -> x[x.Length / 2])
|> Array.sum
|> printfn "Task 1: %A"


let rec folderTask2 page stack =
    let endSet = rulePass page rules |> Array.map (fun (_, b, _) -> b) |> set

    let pass, fail = Array.partition (fun (x: int) -> endSet.Contains x) page

    Array.length pass > 0
    |> function
        | true -> folderTask2 pass (stack @ [ fail |> Array.head ])
        | false -> stack @ [ fail |> Array.head ]

failed
|> Array.map (fun x -> folderTask2 x [])
|> Array.map (fun x -> x[x.Length / 2])
|> Array.sum
|> printfn "Task 2: %A"
