using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Mappings;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using PredicateBuilder;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Tags.Queries.GetTags
{
    public class GetTagsQuery : IRequestWrapper<PaginatedList<TagDto>>
    {
        public int Page { get; set; }
        public string Filters { get; set; }
        public string Sorting { get; set; }
        public int PageSize { get; set; }
    }
    public class GetTagsQueryHandler : IRequestHandlerWrapper<GetTagsQuery, PaginatedList<TagDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResult<PaginatedList<TagDto>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var servicePredicateBuilder = PredicateHelper.CreateFilterCondition<Tag>(request.Filters, request.Sorting, request.Page, request.PageSize, new List<string>());

            var experssion = _context.Tags.Get(servicePredicateBuilder);
            var result = await experssion.ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                 .PaginatedListAsync(servicePredicateBuilder.PaginationData.PageNumber, servicePredicateBuilder.PaginationData.ItemsPerPage);
            return ServiceResult.Success(result);
        }

    }
}
