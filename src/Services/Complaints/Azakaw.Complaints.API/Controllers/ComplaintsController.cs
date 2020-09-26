using System;
using System.Threading.Tasks;
using Azakaw.Complaints.API.Application.Commands;
using Azakaw.Complaints.API.Application.Models;
using Azakaw.Complaints.API.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Azakaw.Complaints.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ComplaintsController : ApiControllerBase
    {
        private readonly IComplaintQueries _complaintQueries;
        private readonly ILogger<ComplaintsController> _logger;

        public ComplaintsController(ILogger<ComplaintsController> logger, IComplaintQueries complaintQueries)
        {
            _logger = logger;
            _complaintQueries = complaintQueries;
        }

        /// <summary>
        /// Creates new complaint
        /// </summary>
        /// <param name="createComplaintCommand"></param>
        /// <param name="apiVersion"></param>
        /// <returns>Id of the newly created complaint</returns>
        //[Authorize(Policy = Application.Authorization.Policies.ComplaintWrite)]
        [HttpPost("", Name = nameof(CreateComplaint))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintCommand createComplaintCommand, ApiVersion apiVersion)
        {
            var newComplaintId = await Mediator.Send(createComplaintCommand);

            return CreatedAtAction(nameof(GetComplaint), new { complaintId = newComplaintId, version = apiVersion.ToString() }, new { id = newComplaintId });
        }

        /// <summary>
        /// Get complaint details by complaintId
        /// </summary>
        /// <param name="complaintId">complaint id</param>
        /// <returns></returns>
        //[Authorize(Policy = Application.Authorization.Policies.ComplaintRead)]
        [HttpGet("{complaintId}", Name = nameof(GetComplaint))]
        public async Task<ActionResult<ComplaintResult>> GetComplaint(Guid complaintId)
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