using System;
using System.Collections.Generic;
using Azakaw.Complaints.API.Application.Models;
using MediatR;

namespace Azakaw.Complaints.API.Application.Commands
{
    public class CreateComplaintCommand : IRequest<Guid>
    {
        public CreateComplaintCommand()
        {
            ComplaintItems = new List<ComplaintItemDto>();
        }

        public IEnumerable<ComplaintItemDto> ComplaintItems { get; set; }
        public string Name { get; set; }
    }
}