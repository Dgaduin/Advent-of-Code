open System
// For more information see https://aka.ms/fsharp-console-apps
//Time:        44     82     69     81
//Distance:   202   1076   1138   1458
let input = [ (44L, 202L); (82L, 1076L); (69L, 1138L); (81L, 1458L) ]
let task2Input = (44826981L, 202107611381458L)

//Time:      7  15   30
//Distance:  9  40  200
let testInput = [ (7L, 9L); (15L, 40L); (30L, 200L) ]
let task2TestInput = (71530L, 940200L)

let distanceFn maxTime time = (maxTime - time) * time
let rangeFn (maxTime: int64) = [ 1L .. maxTime - 1L ]

let raceFn (maxTime, maxValue) =
    rangeFn maxTime
    |> List.map (distanceFn maxTime)
    |> List.filter (fun x -> x > maxValue)
    |> List.length

input |> List.map raceFn |> List.fold (fun acc a -> acc * a) 1 |> printfn "%i"

// raceFn task2Input |> printfn "%i" -> too slow

let findMid min max = (max - min) / 2L + min

let rec bisect (min: int64) max f =
    let mid = findMid min max

    if min = max then min
    else if f mid then bisect min mid f
    else bisect mid (max - 1L) f

let task2 (maxTime, maxValue) =
    let fn a = (distanceFn maxTime a) > maxValue
    let midPoint = findMid 0L maxTime
    let b = bisect 1L midPoint fn
    printfn "%i %i" b ((midPoint - b) * 2L)

task2 task2Input
