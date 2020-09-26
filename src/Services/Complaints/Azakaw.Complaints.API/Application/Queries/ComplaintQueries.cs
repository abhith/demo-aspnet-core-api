using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azakaw.Complaints.API.Application.Models;
using Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate;
using Microsoft.Extensions.Logging;

namespace Azakaw.Complaints.API.Application.Queries
{
    public class ComplaintQueries : IComplaintQueries
    {
        private readonly ILogger<ComplaintQueries> _logger;

        public ComplaintQueries(ILogger<ComplaintQueries> logger)
        {
            _logger = logger;
        }

        public Task<ComplaintResult> GetComplaintByIdAsync(Guid id)
        {
            _logger.LogInformation("Reading complaint from DB happens here({ComplaintId})", id);
            // TODO(abhith): read from DB

            var output = new ComplaintResult
            {
                Id = id,
                Name = "Demo Complaint",
                Status = ComplaintStatus.Submitted.Name,
                ComplaintItems = new List<ComplaintItemDto> { new ComplaintItemDto
                {
                    Title = "Title 1",
                    Description = "Some description"
                }, new ComplaintItemDto()
                {
                    Title = "Title 2",
                    Description = "Some description"
                }}
            };

            return Task.FromResult(output);
        }
    }
}