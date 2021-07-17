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
    public class GetSettingByNameQuery : IRequestWrapper<SettingDto>
    {
        public string Name { get; set; }
    }
    public class GetSettingByNameQueryHandler : IRequestHandlerWrapper<GetSettingByNameQuery, SettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSettingByNameQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<SettingDto>> Handle(GetSettingByNameQuery request, CancellationToken cancellationToken)
        {
            var content = await _context.Settings
             .Where(x => x.Name == request.Name)
             .ProjectTo<SettingDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(cancellationToken);

            return content != null ? ServiceResult.Success(content) : ServiceResult.Failed<SettingDto>(ServiceError.NotFound);
        }
    }
}
