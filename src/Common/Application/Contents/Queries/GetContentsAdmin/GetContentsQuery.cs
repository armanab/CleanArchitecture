using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Mappings;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using PredicateBuilder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Queries.GetContentsAdmin
{
    public class GetContentsQuery : IRequestWrapper<PaginatedList<ContentDto>>
    {
        public int Page { get; set; }
        public string Filters { get; set; }
        public string Sorting { get; set; }
        public int PageSize { get; set; }
    }
    public class GetContentsQueryHandler : IRequestHandlerWrapper<GetContentsQuery, PaginatedList<ContentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetContentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResult<PaginatedList<ContentDto>>> Handle(GetContentsQuery request, CancellationToken cancellationToken)
        {
           
            var servicePredicateBuilder = PredicateHelper.CreateFilterCondition<Content>(request.Filters, request.Sorting, request.Page, request.PageSize, new List<string>());
            if (servicePredicateBuilder.SortCondition == null)
                servicePredicateBuilder.SortCondition = SortCondition<Content>.OrderByDescending(q => q.Priority);
            servicePredicateBuilder.IncludedNavigationPropertiesExpression = new List<Expression<Func<Content, object>>> { x => x.Image };
            var experssion=_context.Contents.Get(servicePredicateBuilder);
            var result =await experssion.ProjectTo<ContentDto>(_mapper.ConfigurationProvider)
                 .PaginatedListAsync(servicePredicateBuilder.PaginationData.PageNumber, servicePredicateBuilder.PaginationData.ItemsPerPage);

            return ServiceResult.Success(result);
        }
       
    }
}
