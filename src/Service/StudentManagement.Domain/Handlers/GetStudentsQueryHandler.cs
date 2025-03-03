using MediatR;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository;
using StudentManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Handlers
{
    public class GetStudentsQueryHandler : IRequestHandler <GetStudentsQuery,IEnumerable<StudentModel>>
    {
        private readonly IStudentRepository _repository;
        public GetStudentsQueryHandler(IStudentRepository repository )
        {
           _repository = repository;
        }

        //public Task<IEnumerable<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<IEnumerable<StudentModel>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllStudentsAsync(cancellationToken);
        }
    }
}
