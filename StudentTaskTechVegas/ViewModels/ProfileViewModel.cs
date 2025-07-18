using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IProfileService _profileService;

        [ObservableProperty]
        private ProfileResponse? profile;

        public ProfileViewModel(IProfileService profileService)
        {
            _profileService = profileService;
         _=   LoadProfileAsync();
        }

        public async Task LoadProfileAsync()
        {
            var token = await SecureStorage.GetAsync("Token");
            var parentId = await SecureStorage.GetAsync("ParentId");

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(parentId))
            {
                Profile = await _profileService.GetProfileAsync(token, parentId);
            }
        }
    }
}
