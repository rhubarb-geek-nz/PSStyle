// Copyright (c) 2025 Roger Brown.
// Licensed under the MIT License.

using System.Management.Automation;

namespace RhubarbGeekNz.PSStyle
{
    public class ModuleAssemblyInitializer : IModuleAssemblyInitializer
    {
        public void OnImport()
        {
            using (var shell = PowerShell.Create(RunspaceMode.CurrentRunspace))
            {
                shell.AddCommand("Set-Variable")
                    .AddParameter("Name", "PSStyle")
                    .AddParameter("Value", System.Management.Automation.PSStyle.Instance)
                    .Invoke();
            }
        }
    }
}
