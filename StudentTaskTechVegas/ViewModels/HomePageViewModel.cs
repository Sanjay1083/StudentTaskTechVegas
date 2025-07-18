using System.Collections.ObjectModel;
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
        private ObservableCollection<StudentModel> students = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string? parentName;

        [ObservableProperty]
        private string? parentPhoneNumber;

        [ObservableProperty]
        private string? parentImage;

        public HomePageViewModel(IStudentService studentService, IProfileService profileService)
        {
            _studentService = studentService;
            _profileService = profileService;

            // Start loading data
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await LoadStudentsAsync();
            await LoadParentDetailsAsync();
        }

        [RelayCommand]
        private async Task NavigateToAcademic(StudentModel student)
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

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(parentIdString)) return;

                if (!int.TryParse(parentIdString, out int parentId)) return;

                var result = await _studentService.GetStudentsAsync(token, parentId);

                Students.Clear();

                if (result != null)
                {
                    foreach (var student in result)
                    {
                        Students.Add(new StudentModel
                        {
                            StudentId = student.StudentId,
                            Name = student.Name,
                            AdmissionNo = student.AdmissionNo,
                            ClassName = student.ClassName,
                            SectionName = student.SectionName,
                            AcademicYear = student.AcademicYear,
                            Image = $"https://testapi.techvegas.in/StudentImages/{student.Image}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // log error or show message
                Console.WriteLine("Error loading students: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadParentDetailsAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("Token");
                var parentId = await SecureStorage.GetAsync("ParentId");

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(parentId))
                {
                    var profile = await _profileService.GetProfileAsync(token, parentId);

                    if (profile != null)
                    {
                        ParentName = profile.MName;
                        ParentPhoneNumber = profile.MPhoneNumber;
                        //ParentImage = $"https://testapi.techvegas.in/ParentImages/{profile.Image}";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading parent profile: " + ex.Message);
            }
        }
    }
}
