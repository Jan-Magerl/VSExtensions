//------------------------------------------------------------------------------
// <copyright file="BlameSolutionExplorerCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace TortoiseVS.Commands
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.Shell;
    using TortoiseVS.Tortoise;
    using TortoiseVS.VSHelper;

    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class BlameSolutionExplorerCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("17ab68aa-fc20-46b0-b36d-a4c2f6c39f33");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlameSolutionExplorerCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private BlameSolutionExplorerCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static BlameSolutionExplorerCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new BlameSolutionExplorerCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            string file;
            if (Studio.Instance.IsFileSelectedInSolutionExplorer(out file))
            {
                TortoiseProc.Instance.Blame(file, 0);
            }
        }
    }
}
