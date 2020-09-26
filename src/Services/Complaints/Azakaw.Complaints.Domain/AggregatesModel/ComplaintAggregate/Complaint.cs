using System;
using System.Collections.Generic;
using Code.Library.Dtos;

namespace Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate
{
    public class Complaint : EntityDto<Guid>
    {
        private readonly List<ComplaintItem> _complaintItems;

        public Complaint(string name) : this()
        {
            Name = name;
            ComplaintStatus = ComplaintStatus.Submitted;
        }

        protected Complaint()
        {
            _complaintItems = new List<ComplaintItem>();
        }

        public IReadOnlyCollection<ComplaintItem> ComplaintItems => _complaintItems;
        public ComplaintStatus ComplaintStatus { get; private set; }
        public string Name { get; private set; }

        public void AddComplaintItem(string title, string description)
        {
            var complaintItem = new ComplaintItem(title, description);
            _complaintItems.Add(complaintItem);
        }
    }
}