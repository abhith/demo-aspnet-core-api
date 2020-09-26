using System;
using System.Threading.Tasks;
using Azakaw.Complaints.API.Application.Commands;
using Azakaw.Complaints.API.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Azakaw.Complaints.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ComplaintsController : ApiControllerBase
    {
        private readonly IComplaintQueries _complaintQueries;
        private readonly ILogger<ComplaintsController> _logger;

        public ComplaintsController(ILogger<ComplaintsController> logger, IComplaintQueries complaintQueries)
        {
            _logger = logger;
            _complaintQueries = complaintQueries;
        }

        //[Authorize(Policy = Application.Authorization.Policies.ComplaintWrite)]
        [HttpPost("", Name = nameof(CreateComplaint))]
        public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintCommand createComplaintCommand, ApiVersion apiVersion)
        {
            var newComplaintId = await Mediator.Send(createComplaintCommand);

            return CreatedAtAction(nameof(GetComplaint), new { complaintId = newComplaintId, version = apiVersion.ToString() }, new { id = newComplaintId });
        }

        //[Authorize(Policy = Application.Authorization.Policies.ComplaintRead)]
        [HttpGet("{complaintId}", Name = nameof(GetComplaint))]
        public async Task<ActionResult> GetComplaint(Guid complaintId)
        {
            var cashbackRequest = await _complaintQueries.GetComplaintByIdAsync(complaintId);

            // TODO(abhith): personal authorization check. i.e To make sure that the authorized user has access to this item.

            if (cashbackRequest == null)
            {
                return NotFound();
            }

            return Ok(cashbackRequest);
        }
    }
}