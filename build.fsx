#r @"packages/FAKE/tools/FakeLib.dll"

open Fake

Target "Restore" <| fun _ ->
    Shell.Exec
        ("dotnet", "restore src/BitcoinLib.sln")
    |> ignore

Target "Build" <| fun _ ->
    !! (System.IO.Path.Combine ("src", "BitcoinLib.sln"))
    |> MSBuildRelease "" "Rebuild"
    |> ignore

Target "NuGet" (fun _ ->
    Paket.Pack (fun p -> { p with OutputPath = ".nuget" }))

"Restore"
==> "Build"
==> "NuGet"

RunTargetOrDefault "Build"
