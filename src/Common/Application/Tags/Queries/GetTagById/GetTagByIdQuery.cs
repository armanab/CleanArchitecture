using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Tags.Queries.GetTagById
{
    public class GetTagByIdQuery : IRequestWrapper<TagDto>
    {
        public int Id { get; set; }
    }
    public class GetTagByIdQueryHandler : IRequestHandlerWrapper<GetTagByIdQuery, TagDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var content = await _context.Tags
             .Where(x => x.Id == request.Id)
             .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(cancellationToken);

            return content != null ? ServiceResult.Success(content) : ServiceResult.Failed<TagDto>(ServiceError.NotFound);
        }
    }
}
