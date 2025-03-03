using MediatR;
using StudentManagement.Domain.Command;
using StudentManagement.Domain.Validators;
using StudentManagement.Models.Models;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Handlers
{
   
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentModel>
    {
        private readonly IStudentRepository _repository;

        public CreateStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentModel> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateStudentCommandValidator();
            var results = await validator.ValidateAsync(request, cancellationToken);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }

            var student = new Student
            {
                StudentName = request.StudentName,
                Dob = request.Dob,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            await _repository.AddStudentAsync(student, cancellationToken);

            return new StudentModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Dob = student.Dob,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address
            };
        }
    }

}
