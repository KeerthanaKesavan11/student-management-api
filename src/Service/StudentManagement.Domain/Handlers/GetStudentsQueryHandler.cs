using MediatR;
using StudentManagement.Domain.Queries;
using StudentManagement.Models.Models.DTOs;
using StudentManagement.Repository.Interfaces;

namespace StudentManagement.Domain.Handlers
{
    public class GetStudentsQueryHandler : IRequestHandler <GetStudentsQuery,IEnumerable<StudentModel>>
    {
        private readonly IStudentRepository _repository;
        public GetStudentsQueryHandler(IStudentRepository repository )
        {
           _repository = repository;
        }

        public async Task<IEnumerable<StudentModel>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllStudentsAsync(cancellationToken);
        }
    }
}
