using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azakaw.Complaints.API.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Azakaw.Complaints.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplaintsController : ApiControllerBase
    {
        private readonly ILogger<ComplaintsController> _logger;

        public ComplaintsController(ILogger<ComplaintsController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> CreateComplaint([FromBody] CreateComplaintCommand createComplaintCommand, ApiVersion apiVersion)
        {
            var newComplaintId = await Mediator.Send(createComplaintCommand);

            return CreatedAtAction(nameof(GetComplaint), new { complaintId = newComplaintId, version = apiVersion.ToString() }, new { id = newComplaintId });
        }

        }
    }
}