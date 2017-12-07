#r @"packages/FAKE/tools/FakeLib.dll"

open Fake

Target "Build" <| fun _ ->
    !! (System.IO.Path.Combine ("src", "BitcoinLib.sln"))
    |> MSBuildRelease "" "Rebuild"
    |> ignore

Target "NuGet" (fun _ ->
    Paket.Pack (fun p -> { p with OutputPath = ".nuget" }))

"Build"
==> "NuGet"

RunTargetOrDefault "Build"
