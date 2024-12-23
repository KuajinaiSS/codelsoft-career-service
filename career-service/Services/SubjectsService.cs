﻿using career_service.Repositories.Interfaces;
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

    public override async Task<SubjectProto.PostRequisitesResponse> GetPostRequisitesMap(SubjectProto.Empty request, ServerCallContext context) 
    {
        var relationshipsList = await _unitOfWork.SubjectRelationshipsRepository.Get();
        var postRequisitesMap = new Dictionary<string, List<string>>();
        
        relationshipsList.ForEach(sr =>
        {
            if (postRequisitesMap.ContainsKey(sr.PreSubjectCode))
            {
                postRequisitesMap[sr.PreSubjectCode].Add(sr.SubjectCode);
            }
            else
            {
                postRequisitesMap.Add(sr.PreSubjectCode, new List<string>
                {
                    sr.SubjectCode
                });
            }
        });
        
        var response = new PostRequisitesResponse();
        foreach (var entry in postRequisitesMap)
        {
            response.PostRequisitesMap.Add(new PostRequisites
            {
                PreSubjectCode = entry.Key,
                PostSubjectCodes = { entry.Value }
            });
        }
        
        return await Task.FromResult(response);
    }
    
    public override async Task<SubjectProto.PreRequisitesResponse> GetPreRequisitesMap(SubjectProto.Empty request, ServerCallContext context)
    {
        var relationshipsList = await _unitOfWork.SubjectRelationshipsRepository.Get();
        var preRequisitesMap = new Dictionary<string, List<string>>();

        relationshipsList.ForEach(sr =>
        {
            if (preRequisitesMap.ContainsKey(sr.SubjectCode))
            {
                preRequisitesMap[sr.SubjectCode].Add(sr.PreSubjectCode);
            }
            else
            {
                preRequisitesMap.Add(sr.SubjectCode, new List<string>
                {
                    sr.PreSubjectCode
                });
            }
        });

        var response = new PreRequisitesResponse();
        foreach (var entry in preRequisitesMap)
        {
            response.PreRequisitesMap.Add(new PreRequisites
            {
                SubjectCode = entry.Key,
                PreSubjectCodes = { entry.Value }
            });
        }

        return await Task.FromResult(response);
    }

}