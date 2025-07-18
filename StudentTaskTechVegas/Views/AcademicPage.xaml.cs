using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.Views;

[QueryProperty(nameof(Student), "Student")]
public partial class AcademicPage : ContentPage
{
    private StudentModel _student;

    public StudentModel Student
    {
        get => _student;
        set
        {
            _student = value;
            BindingContext = _student;
        }
    }
    public AcademicPage()
    {
        InitializeComponent();
    }
}