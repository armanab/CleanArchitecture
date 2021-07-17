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

namespace CleanApplication.Application.Settings.Queries.GetSettingById
{
    public class GetSettingByIdQuery : IRequestWrapper<SettingDto>
    {
        public int Id { get; set; }
    }
    public class GetSettingByIdQueryHandler : IRequestHandlerWrapper<GetSettingByIdQuery, SettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSettingByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<SettingDto>> Handle(GetSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var content = await _context.Settings
             .Where(x => x.Id == request.Id)
             .ProjectTo<SettingDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(cancellationToken);

            return content != null ? ServiceResult.Success(content) : ServiceResult.Failed<SettingDto>(ServiceError.NotFound);
        }
    }
}
