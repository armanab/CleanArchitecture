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

namespace CleanApplication.Application.Contents.Commands.Delete
{
    public class DeleteContentCommand:IRequestWrapper<bool>
    {
        public Guid Id { get; set; }

    }
    public class DeleteContentCommandHandler : IRequestHandlerWrapper<DeleteContentCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteContentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contents
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Content), request.Id);
            }

            _context.Contents.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(true);
        }
    }
}
