using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentTaskTechVegas.Models
{
    public class StudentModel
    {

        [JsonPropertyName("studentId")]
        public int StudentId { get; set; }

        [JsonPropertyName("admissionNo")]
        public string? AdmissionNo { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("className")]
        public string? ClassName { get; set; }

        [JsonPropertyName("sectionName")]
        public string? SectionName { get; set; }

        [JsonPropertyName("academicYear")]
        public string? AcademicYear { get; set; }
    }

}
