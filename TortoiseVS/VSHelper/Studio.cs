namespace TortoiseVS.VSHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Process = System.Diagnostics.Process;

    internal class Studio
    {
        private static readonly Lazy<Studio> InstanceValue = new Lazy<Studio>(() => new Studio());

        private static DTE2 dTe2;
        private static Package package;

        private Studio()
        {
        }

        public static Studio Instance
        {
            get
            {
                return InstanceValue.Value;
            }
        }

        public static DTE2 DTE2
        {
            get
            {
                return dTe2;
            }

            set
            {
                dTe2 = value;
            }
        }

        public static Package Package
        {
            get
            {
                return package;
            }

            set
            {
                package = value;
            }
        }

        public int Line
        {
            get
            {
                TextSelection textSelection = DTE2.ActiveDocument.Selection as TextSelection;
                if (textSelection != null)
                {
                    return textSelection.ActivePoint.Line;
                }
                return 0;
            }
        }

        public static void Init(Package pk)
        {
            Package = pk;
            DTE2 = (DTE2)GetService(typeof(DTE));
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
                selectedProject = DTE2.SelectedItems.Item(1).Project;
            }
            if (selectedProject != null)
            {
                IVsSolution solutionService = GetService(typeof(SVsSolution)) as IVsSolution;

                Debug.Assert(solutionService != null, "solutionService is null");

                IVsHierarchy selectedHierarchy;
                ErrorHandler.ThrowOnFailure(solutionService.GetProjectOfUniqueName(selectedProject.UniqueName, out selectedHierarchy));
                Debug.Assert(selectedHierarchy != null, "selectedHierarchy is null");

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

        private static object GetService(Type type)
        {
            var pk = Package as IServiceProvider;
            return pk.GetService(type);
        }
    }
}
