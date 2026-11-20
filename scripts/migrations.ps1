<#
  Uso:  .\scripts\migrations.ps1 -Name AddAlgo
  Crea una migracion nueva en Infrastructure/Persistence/Migrations.
  Requiere el archivo .env en la raiz (copia sample.env si lo clonaste de git).
  Equivalente en Visual Studio (Package Manager Console, proyecto por defecto = ClubDeportivo):
        Add-Migration AddAlgo -OutputDir Infrastructure/Persistence/Migrations
#>
param([Parameter(Mandatory = $true)][string]$Name)

$root = Split-Path $PSScriptRoot -Parent
$proj = Join-Path $root 'src\ClubDeportivo\ClubDeportivo.csproj'

dotnet ef migrations add $Name `
    --project $proj --startup-project $proj `
    --context AppDbContext `
    --output-dir Infrastructure/Persistence/Migrations