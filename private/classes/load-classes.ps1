$c = Get-Content -Path ".\private\classes\adftools.cs" -Raw
Add-Type -TypeDefinition $c -IgnoreWarnings
[SQLPlayer.AdfTools.AdfObject] | Get-Member -Static
[SQLPlayer.AdfTools.AdfObject]::ADF_FOLDERS


$ds = [Microsoft.Azure.Commands.DataFactoryV2.Models.PSDataset]::new()

Import-Module ".\azure.datafactory.tools.psd1" -Force


[SQLPlayer.AdfTools.AdfObject]::GetSimplifiedType("PSabC1")
$adf = [SQLPlayer.AdfTools.Adf]::new()
$adf.GetType() | Format-List
#--------------------------------------------------

$file = "X:\!WORK\GitHub\!SQLPlayer\azure.datafactory.tools\test\BigFactorySample2\dataset\CADOutput1.json"
$SubFolder = "dataset"
$BaseName = "CADOutput1"
$txt = Get-Content $file -Encoding "UTF8"

#$o = New-Object -TypeName AdfObject 
$o = [SQLPlayer.AdfTools.AdfObject]::new($BaseName, $SubFolder)
# $o.Name = $BaseName
# $o.Type = $SubFolder
$o.FileName = $file
$o.Body = $txt | ConvertFrom-Json

$o.AddDependant( "aaa", "dfdgjrig" )

$o
$o.DependsOn.Count
$o.DependsOn.Keys[0]
$o.DependsOn.Values[0]

$o.Body.GetType()
$o.Body.Name

($txt | ConvertFrom-Json).GetType()
PSCustomObject




# https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview
# https://codeburst.io/working-with-json-in-net-core-3-2fd1236126c1
