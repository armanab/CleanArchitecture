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

namespace CleanApplication.Application.Users.Queries.GetUsersAdmin
{
    public class GetUsersQuery : IRequestWrapper<PaginatedList<ApplicationUserDto>>
    {
        public int Page { get; set; }
        public string Filters { get; set; }
        public string Sorting { get; set; }
        public int PageSize { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandlerWrapper<GetUsersQuery, PaginatedList<ApplicationUserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResult<PaginatedList<ApplicationUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var servicePredicateBuilder = PredicateHelper.CreateFilterCondition<ApplicationUser>(request.Filters, request.Sorting, request.Page, request.PageSize, new List<string>());
            //servicePredicateBuilder.IncludedNavigationPropertiesExpression = new List<Expression<Func<ApplicationUser, object>>> { x => x.Image };
            servicePredicateBuilder.FilterCondition.And(x => x.IsDeleted, OperatorEnum.Equal, "false");
            servicePredicateBuilder.FilterCondition.And(x => x.Email, OperatorEnum.NotEqual, "administrator@localhost.com");
            var experssion = _context.ApplicationUsers.Get(servicePredicateBuilder);
            var result = await experssion.ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                 .PaginatedListAsync(servicePredicateBuilder.PaginationData.PageNumber, servicePredicateBuilder.PaginationData.ItemsPerPage);

            return ServiceResult.Success(result);
        }
    }
}
