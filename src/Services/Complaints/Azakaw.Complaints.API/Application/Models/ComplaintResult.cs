using System;
using System.Collections.Generic;

namespace Azakaw.Complaints.API.Application.Models
{
    public class ComplaintResult
    {
        public ComplaintResult()
        {
            ComplaintItems = new List<ComplaintItemDto>();
        }

        public List<ComplaintItemDto> ComplaintItems { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}