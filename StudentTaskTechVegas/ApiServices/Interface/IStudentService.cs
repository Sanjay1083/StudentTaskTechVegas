using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.ApiServices.Interface
{
    public interface IStudentService
    {
        Task<List<StudentModel>?> GetStudentsAsync(string token, int parentId);
    }
}
