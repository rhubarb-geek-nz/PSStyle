# rhubarb-geek-nz.PSStyle
PSStyle variable for PowerShell Desktop

PowerShell Desktop does not provide a PSStyle variable. This packages the class from PowerShell Core.

## Build

Build the module with dotnet

```
dotnet publish --configuration Release
```

## Test

The test script exercises the custom formatters

```
PowerShell -ExecutionPolicy ByPass .\test.ps1
```

## Usage

Import the module before using

```
if (-not $PSStyle)
{
	Import-Module -Name 'rhubarb-geek-nz.PSStyle'
}
```

## Notes

The custom formatters require the execution policy bypass. If you must run with the restricted policy then remove the [PSStyle.format.ps1xml](PSStyle.format.ps1xml) file and `FormatsToProcess` from the `rhubarb-geek-nz.PSStyle.psd1` manifest.
