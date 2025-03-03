using MediatR;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Queries
{
    public class GetStudentsQuery : IRequest<IEnumerable<StudentModel>>
    {

    }
    public class GetStudentsByIdQuery : IRequest<StudentModel>
    {
        public int StudentId { get; set; }
    }
}
