using MediatR;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Command
{
    public class CreateStudentCommand : IRequest<StudentModel>
    {
        public string StudentName { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
