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

namespace CleanApplication.Application.Tags.Commands.Delete
{
    public class DeleteTagCommand:IRequestWrapper<bool>
    {
        public int Id { get; set; }

    }
    public class DeleteTagCommandHandler : IRequestHandlerWrapper<DeleteTagCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteTagCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            _context.Tags.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(true);
        }
    }
}
