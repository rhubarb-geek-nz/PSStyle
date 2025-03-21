#!/usr/bin/env pwsh
# Copyright (c) 2025 Roger Brown.
# Licensed under the MIT License.

trap
{
	throw $PSItem
}

$ErrorActionPreference = 'Stop'
$InformationPreference = 'Continue'

if (-not $PSStyle)
{
	Import-Module -Name 'rhubarb-geek-nz.PSStyle'
}

if (-not $PSStyle)
{
	throw '$PSStyle is not defined'
}

$PSStyle.GetType().Assembly

$PSStyle | Format-List
$PSStyle.Formatting | Format-List
$PSStyle.Progress | Format-List
$PSStyle.FileInfo | Format-List
$PSStyle.Foreground | Format-List
$PSStyle.Background | Format-List
$PSStyle.Reset 
