using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TortoiseVS.VSHelper
{
    using Process = System.Diagnostics.Process;

    internal class Studio
    {
        private static readonly Lazy<Studio> instance = new Lazy<Studio>(() => new Studio());

        public static DTE2 DTE2;
        public static Package Package;

        private Studio()
        {
        }

        public static void Init(Package package)
        {
            Package = package;
            DTE2 = (DTE2)GetService(typeof(DTE));


        }

        private static object GetService(Type type)
        {
            var package = Package as IServiceProvider;
            return package.GetService(type);
        }

        public static Studio Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public int Line
        {
            get
            {
                return (DTE2.ActiveDocument.Selection as TextSelection).ActivePoint.Line;
            }
        }

        public IEnumerable<string> GetSelectedFilesInSolutionExplorer()
        {
            var items = (Array)DTE2.ToolWindows.SolutionExplorer.SelectedItems;

            return from item in items.Cast<UIHierarchyItem>()
                   let pi = item.Object as ProjectItem
                   select pi.FileNames[1];
        }

        public bool IsFileSelectedInSolutionExplorer(out string file)
        {
            var items = GetSelectedFilesInSolutionExplorer();
            file = items.ElementAtOrDefault(0);
            return !string.IsNullOrEmpty(file);
        }

        public void UnLoadProject()
        {
            Project selectedProject = null;

            if (DTE2.SelectedItems.Count > 0)
            {
                selectedProject = (DTE2.SelectedItems.Item(1)).Project;
            }
            if (selectedProject != null)
            {
                IVsSolution solutionService = GetService(typeof(SVsSolution)) as IVsSolution;

                Debug.Assert(solutionService != null);

                IVsHierarchy selectedHierarchy;
                ErrorHandler.ThrowOnFailure(solutionService.GetProjectOfUniqueName(selectedProject.UniqueName, out selectedHierarchy));
                Debug.Assert(selectedHierarchy != null);

                ErrorHandler.ThrowOnFailure(solutionService.CloseSolutionElement((uint)__VSSLNCLOSEOPTIONS.SLNCLOSEOPT_UnloadProject, selectedHierarchy, 0));
            }
        }

        public string CloseSolution()
        {
            string solution = DTE2.Solution.FileName;
            DTE2.Solution.Close(true);
            return solution;
        }

        public void OpenSolution(string file)
        {
            DTE2.Solution.Open(file);
        }

        public void BuildSolution()
        {
            DTE2.Solution.SolutionBuild.Build();
        }

        internal void Restart(string file)
        {
            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo.FileName = file;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            DTE2.Quit();
        }
    }
}
