using System;
using System.Threading.Tasks;
using Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate;
using Microsoft.Extensions.Logging;

namespace Azakaw.Complaints.Infrastructure.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ILogger<ComplaintRepository> _logger;

        public ComplaintRepository(ILogger<ComplaintRepository> logger)
        {
            _logger = logger;
        }

        public Task<Guid> AddAsync(Complaint complaint)
        {
            _logger.LogInformation("Persisting complaint {@Complaint} to DB happens here", complaint);

            // TODO(abhith): DB implementation

            return Task.FromResult(Guid.NewGuid());
        }
    }
}