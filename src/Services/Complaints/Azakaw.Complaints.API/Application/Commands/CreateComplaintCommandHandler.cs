using System;
using System.Threading;
using System.Threading.Tasks;
using Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate;
using MediatR;

namespace Azakaw.Complaints.API.Application.Commands
{
    public class CreateComplaintCommandHandler : IRequestHandler<CreateComplaintCommand, Guid>
    {
        private readonly IComplaintRepository _complaintRepository;

        public CreateComplaintCommandHandler(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<Guid> Handle(CreateComplaintCommand message, CancellationToken cancellationToken)
        {
            var complaint = new Complaint(message.Name);

            foreach (var item in message.ComplaintItems)
            {
                complaint.AddComplaintItem(item.Title, item.Description);
            }

            var newComplaintId = await _complaintRepository.AddAsync(complaint);

            return newComplaintId;
        }
    }
}