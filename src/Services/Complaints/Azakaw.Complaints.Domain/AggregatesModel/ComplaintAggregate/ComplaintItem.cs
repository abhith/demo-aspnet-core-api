using System;
using Code.Library.Dtos;

namespace Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate
{
    public class ComplaintItem : EntityDto<Guid>
    {
        public ComplaintItem(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Description { get; private set; }
        public string Title { get; private set; }
    }
}