using AutoMapper;
using CleanApplication.Application.Common.Exceptions;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Commands.Update
{
    public class UpdateSettingCommand : IRequestWrapper<SettingDto>
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Value { get; set; }
        public string InputType { get; set; }
        public string Title { get; set; }

    }
    public class UpdateSettingCommandHandler : IRequestHandlerWrapper<UpdateSettingCommand, SettingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<SettingDto>> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Settings.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Setting), request.Id);
            }
          
            entity.Value = request.Value;
            entity.Title = request.Title;
            entity.InputType = request.InputType;
          
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SettingDto>(entity));
        }
    }
}
