using System;
using System.Collections.Generic;
using System.Linq;

namespace CallTracking.Web.Models.Repository
{
    public class LeadSourcesRepository : IRepository<LeadSource>
    {
        private readonly CallTrackingContext _context = new CallTrackingContext();

        public void Create(LeadSource leadSource)
        {
            _context.LeadSources.Add(leadSource);
            _context.SaveChanges();
        }

        public void Update(LeadSource entity)
        {
            throw new NotImplementedException();
        }

        public LeadSource FirstOrDefault(Func<LeadSource, bool> predicate)
        {
            return _context.LeadSources.FirstOrDefault(predicate);
        }

        public LeadSource Find(int id)
        {
            return _context.LeadSources.Find(id);
        }

        public IEnumerable<LeadSource> All()
        {
            return _context.LeadSources.ToList();
        }
    }
}