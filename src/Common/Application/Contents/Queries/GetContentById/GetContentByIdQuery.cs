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

namespace CleanApplication.Application.Contents.Queries.GetContentById
{
    public class GetContentByIdQuery : IRequestWrapper<ContentDto>
    {
        public Guid Id { get; set; }
    }
    public class GetContentByIdQueryHandler : IRequestHandlerWrapper<GetContentByIdQuery, ContentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<ContentDto>> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
        {
            var content = await _context.Contents.Include(x=>x.Image)
             .Where(x => x.Id == request.Id)
             .ProjectTo<ContentDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(cancellationToken);

            return content != null ? ServiceResult.Success(content) : ServiceResult.Failed<ContentDto>(ServiceError.NotFound);
        }
    }
}
