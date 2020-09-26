using System;
using System.Collections.Generic;
using System.Linq;
using Code.Library.Domain.Models;
using Code.Library.Exceptions;

namespace Azakaw.Complaints.Domain.AggregatesModel.ComplaintAggregate
{
    public class ComplaintStatus : Enumeration
    {
        public static ComplaintStatus Active = new ComplaintStatus(2, nameof(Active).ToLowerInvariant());
        public static ComplaintStatus AwaitingValidation = new ComplaintStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static ComplaintStatus Submitted = new ComplaintStatus(1, nameof(Submitted).ToLowerInvariant());

        public ComplaintStatus(int id, string name) : base(id, name)
        {
        }

        public static ComplaintStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new DomainException($"Possible values for ComplaintStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ComplaintStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException($"Possible values for ComplaintStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static IEnumerable<ComplaintStatus> List() =>
                    new[] { Submitted, AwaitingValidation, Active };
    }
}