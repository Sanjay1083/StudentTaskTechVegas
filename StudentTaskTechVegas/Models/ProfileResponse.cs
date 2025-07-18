using System;
using System.Collections.Generic;
namespace StudentTaskTechVegas.Models
{
    public class ProfileResponse
    {
        public int ParentId { get; set; }
        public string? FName { get; set; }
        public string? FEducation { get; set; }
        public string? FOccupation { get; set; }
        public string? FPhoneNumber { get; set; }
        public string? FEmailAddress { get; set; }
        public string? MName { get; set; }
        public string? MEducation { get; set; }
        public string? MOccupation { get; set; }
        public string? MPhoneNumber { get; set; }
        public string? MEmailAddress { get; set; }
        public string? ResidenceAddress { get; set; }
        public string? PrimaryNumber { get; set; }
        public string? Password { get; set; }
        public bool Status { get; set; }
    }
}

