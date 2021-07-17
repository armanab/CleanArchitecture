using AutoMapper;
using CleanApplication.Application.Common.Interfaces;
using CleanApplication.Application.Common.Models;
using CleanApplication.Application.Dto;
using CleanApplication.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Commands.Create
{
    public class CreateSettingCommand : IRequestWrapper<SettingDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string InputType { get; set; }
        public string Title { get; set; }
    }
    public class CreateSettingCommandHandler : IRequestHandlerWrapper<CreateSettingCommand, SettingDto>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateSettingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<SettingDto>> Handle(CreateSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = new Setting
            {
                Name = request.Name,
                Value = request.Value,
                InputType = request.InputType,
                Title = request.Title
            };

            await _context.Settings.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<SettingDto>(entity));
        }
    }
}
