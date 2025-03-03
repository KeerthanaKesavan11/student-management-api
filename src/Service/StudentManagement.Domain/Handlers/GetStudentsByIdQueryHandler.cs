using FluentValidation;
using MediatR;
using StudentManagement.API.Exception;
using StudentManagement.Domain.Queries;
using StudentManagement.Domain.Validators;
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
    public class GetStudentsByIdQueryHandler : IRequestHandler<GetStudentsByIdQuery, StudentModel>
    {
        private readonly IStudentRepository _repository;
        public GetStudentsByIdQueryHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentModel> Handle(GetStudentsByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetStudentsByIdQueryValidator();
            var results = await validator.ValidateAsync(request, cancellationToken);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
            var student = await _repository.GetStudentsByIdAsync(request.StudentId, cancellationToken);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }
            return student;
        }
    }
   
}
