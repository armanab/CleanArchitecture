using CleanApplication.Application.Common.Interfaces;
using System;

namespace CleanApplication.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
