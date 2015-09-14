using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CallTracking.Web.Models.Repository
{
    public class LeadsRepository : IRepository<Lead>
    {
        private readonly CallTrackingContext _context = new CallTrackingContext();
        public void Create(Lead lead)
        {
            _context.Leads.Add(lead);
            _context.SaveChanges();
        }

        public void Update(Lead lead)
        {
            _context.Entry(lead).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Lead FirstOrDefault(Func<Lead, bool> predicate)
        {
            return _context.Leads.FirstOrDefault(predicate);
        }

        public Lead Find(int id)
        {
            return _context.Leads.Find(id);
        }

        public IEnumerable<Lead> All()
        {
            return _context.Leads.ToList();
        }
    }
}