using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.Models;
using StudentTaskTechVegas.Views;

namespace StudentTaskTechVegas.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IStudentService _studentService;
        private readonly IProfileService _profileService;

        [ObservableProperty]
        ObservableCollection<StudentModel> students;

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        private string? parentName;

        [ObservableProperty]
        private string? parentPhoneNumber;
        [ObservableProperty]
        private string? parentimage;

        public HomePageViewModel(IStudentService studentService, IProfileService profileService)
        {
            _studentService = studentService;
            _profileService = profileService;
            Students = new ObservableCollection<StudentModel>();
            _=LoadStudentsAsync();
            LoadParentDetailsAsync();
            
        }
        [RelayCommand]
        async Task NavigateToAcademic(StudentModel student)
        {
            if (student == null) return;

            await Shell.Current.GoToAsync(nameof(AcademicPage), new Dictionary<string, object>
                {
                    { "Student", student }
                });
        }
       

        public async Task LoadStudentsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var token = await SecureStorage.GetAsync("Token");
                var parentIdString = await SecureStorage.GetAsync("ParentId");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(parentIdString))
                    return;

                if (!int.TryParse(parentIdString, out int parentId))
                    return;

                var result = await _studentService.GetStudentsAsync(token, parentId);

                Students.Clear();
                if (result != null)
                {
                    foreach (var student in result)
                    {
                        Students.Add(new StudentModel
                        {
                            SectionName = student.SectionName,
                            StudentId = student.StudentId,
                            AcademicYear = student.AcademicYear,
                            AdmissionNo = student.AdmissionNo,
                            ClassName = student.ClassName,
                            Name = student.Name,
                            Image = $"https://testapi.techvegas.in/StudentImages/{student.Image}"
                        });
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void LoadParentDetailsAsync()
        {
            var token = await SecureStorage.GetAsync("Token");
            var parentId = await SecureStorage.GetAsync("ParentId");

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(parentId))
            {
              var  ProfileNAmes = await _profileService.GetProfileAsync(token, parentId);

                if (ProfileNAmes != null)
                {

                    ParentName = ProfileNAmes?.MName;
                    ParentPhoneNumber = ProfileNAmes.MPhoneNumber;

                }


            }
        }
    }

}
