using StudentTaskTechVegas.Views;

namespace StudentTaskTechVegas
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(AcademicPage), typeof(AcademicPage)); 
        }
    }
}
