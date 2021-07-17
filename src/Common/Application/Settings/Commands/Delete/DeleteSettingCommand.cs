using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Commands.Delete
{
    public class DeleteSettingCommand:IRequestWrapper<bool>
    {
        public int Id { get; set; }

    }
    public class DeleteSettingCommandHandler : IRequestHandlerWrapper<DeleteSettingCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Settings
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Setting), request.Id);
            }

            _context.Settings.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(true);
        }
    }
}
