using career_service.Repositories.Interfaces;
using career_service.Services.Interfaces;
using Grpc.Core;
using SubjectProto;

namespace career_service.Services;

public class SubjectsService : SubjectProto.SubjectService.SubjectServiceBase ,ISubjectsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapperService;
    
    public SubjectsService(IUnitOfWork unitOfWork, IMapperService mapperService)
    {
        _unitOfWork = unitOfWork;
        _mapperService = mapperService;
    }
    
    public override async Task<SubjectsResponse> GetAll(Empty request, ServerCallContext context)
    {
        var response = new SubjectProto.SubjectsResponse();
        var subjects = await _unitOfWork.SubjectsRepository.Get();
        
        response.Subjects.AddRange(subjects.Select(c => new SubjectProto.Subject
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code,
            Credits = c.Credits,
            Departament = c.Department,
            Semester = c.Semester
        }));
        
        return await Task.FromResult(response);
    }

    public override async Task<SubjectsRelationshipsResponse> GetAllRelationships(Empty request, ServerCallContext context)
    {
        var response = new SubjectProto.SubjectsRelationshipsResponse();
        var subjectsRelationships = await _unitOfWork.SubjectRelationshipsRepository.Get();
        
        response.SubjectsRelationships.AddRange(subjectsRelationships.Select(c => new SubjectProto.SubjectsRelationships
        {
            Id = c.Id,
            SubjectCode = c.SubjectCode,
            PreSubjectCode = c.PreSubjectCode
        }));
        
        return await Task.FromResult(response);
    }

    public Task<Dictionary<string, List<string>>> GetPreRequisitesMap()
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<string, List<string>>> GetPostRequisitesMap()
    {
        throw new NotImplementedException();
    }
}