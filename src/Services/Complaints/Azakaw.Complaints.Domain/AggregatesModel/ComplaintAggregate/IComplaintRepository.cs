using System;
using System.Threading.Tasks;

namespace Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate
{
    public interface IComplaintRepository
    {
        Task<Guid> AddAsync(Complaint complaint);
    }
}