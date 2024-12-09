using Grpc.Core;

namespace career_service.Services.Interfaces;

public interface ISubjectsService
{
    public Task<SubjectProto.SubjectsResponse> GetAll(SubjectProto.Empty request, ServerCallContext context);

    public Task<SubjectProto.SubjectsRelationshipsResponse> GetAllRelationships(SubjectProto.Empty request, ServerCallContext context);
    
    public Task<SubjectProto.PostRequisitesResponse> GetPostRequisitesMap(SubjectProto.Empty request, ServerCallContext context);
    
    public Task<Dictionary<string, List<string>>> GetPreRequisitesMap();

    
}