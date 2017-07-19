namespace TortoiseVS.Windows
{
    using Microsoft.VisualStudio.PlatformUI;

    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : DialogWindow
    {
        public About()
        {
            InitializeComponent();
            //TextBlockVersion.Text = "Version " + typeof(About).Assembly.GetName().Version;
        }

        private void HyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
