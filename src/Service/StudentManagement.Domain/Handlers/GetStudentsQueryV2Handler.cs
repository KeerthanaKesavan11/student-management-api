using MediatR;
using StudentManagement.Domain.Queries;
using StudentManagement.Repository.Interfaces;
using StudentManagement.Models.Models.DTOs;

namespace StudentManagement.Domain.Handlers
{
    public class GetStudentsQueryV2Handler : IRequestHandler<GetStudentsQueryV2, IEnumerable<StudentModelV2>>
    {
        private readonly IStudentRepository _repository;

        public GetStudentsQueryV2Handler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentModelV2>> Handle(GetStudentsQueryV2 request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllStudentsAsyncV2(cancellationToken);
        }
    }
}
