Get-ChildItem  *.csproj  -Recurse | ForEach-Object  { Remove-Item *.nupkg } 
Get-ChildItem  *.csproj  -Recurse | ForEach-Object  { & nuget.exe pack  $_ -Symbols -IncludeReferencedProjects -Prop Configuration=Release -Prop Platform=AnyCPU} 
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") 


