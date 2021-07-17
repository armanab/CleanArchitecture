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

namespace CleanApplication.Application.Settings.Queries.GetSettings
{
    public class GetSettingsQuery : IRequestWrapper<PaginatedList<SettingDto>>
    {
        public int Page { get; set; }
        public string Filters { get; set; }
        public string Sorting { get; set; }
        public int PageSize { get; set; }
    }
    public class GetSettingsQueryHandler : IRequestHandlerWrapper<GetSettingsQuery, PaginatedList<SettingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSettingsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResult<PaginatedList<SettingDto>>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var servicePredicateBuilder = PredicateHelper.CreateFilterCondition<Setting>(request.Filters, request.Sorting, request.Page, request.PageSize, new List<string>());
            var experssion=_context.Settings.Get(servicePredicateBuilder);
            var result =await experssion.ProjectTo<SettingDto>(_mapper.ConfigurationProvider)
                 .PaginatedListAsync(servicePredicateBuilder.PaginationData.PageNumber, servicePredicateBuilder.PaginationData.ItemsPerPage);
            return ServiceResult.Success(result);
        }
       
    }
}
