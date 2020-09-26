using System;
using System.Collections.Generic;
using Azakaw.Complaints.API.Application.Models;
using MediatR;

namespace Azakaw.Complaints.API.Application.Commands
{
    public class CreateComplaintCommand : IRequest<Guid>
    {
        private readonly List<ComplaintItemDto> _complaintItems;

        public CreateComplaintCommand()
        {
            _complaintItems = new List<ComplaintItemDto>();
        }

        public CreateComplaintCommand(string name, List<ComplaintItemDto> complaintItems)
        {
            Name = name;
            _complaintItems = complaintItems;
        }

        public IEnumerable<ComplaintItemDto> ComplaintItems => _complaintItems;
        public string Name { get; private set; }
    }
}