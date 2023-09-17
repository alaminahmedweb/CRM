using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFollowupService : IRepository<Followup>
    {
        //IEnumerable<Followup> GetFollowupsByDate(DateTime date);
    }
}
