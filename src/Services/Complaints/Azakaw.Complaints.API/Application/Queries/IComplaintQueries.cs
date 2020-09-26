using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azakaw.Complaints.API.Application.Models;

namespace Azakaw.Complaints.API.Application.Queries
{
    public interface IComplaintQueries
    {
        Task<ComplaintResult> GetComplaintByIdAsync(Guid id);
    }
}