using EmoloyeeTask.Data;
using EmoloyeeTask.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTask.Data.Repositories
{
    public class LaborCostRepository : DbRepository<LaborCost>
    {
        private readonly AppDbContext _db;

        public LaborCostRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<LaborCost> Add(LaborCost laborCosts)
        {
            if(laborCosts.Assignment != null)
            {
                _db.Entry(laborCosts.Assignment).State = EntityState.Unchanged;
            }
            if(laborCosts.Employee != null)
            {
                _db.Entry(laborCosts.Employee).State = EntityState.Unchanged;
            }

            var result = await _db.LaborCosts.AddAsync(laborCosts);

            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public override async Task Delete(int id)
        {
            var result = await _db.LaborCosts
                                  .FirstOrDefaultAsync(x => x.Id == id);
            if(result != null)
            {
                _db.Remove(result);
                await _db.SaveChangesAsync();
            }
        }

        public override async Task<LaborCost> Get(int id)
        {
            return await _db.LaborCosts
                            .Include(x => x.Assignment)
                            .Include(x => x.Employee)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<IEnumerable<LaborCost>> GetAll()
        {
            return await _db.LaborCosts
                            .Include(x => x.Assignment)
                            .Include(x => x.Employee)
                            .ToListAsync();
        }

        public async Task<LaborCost> Update(LaborCost laborCosts)
        {
            var result = await _db.LaborCosts
                                  .FirstOrDefaultAsync(x => x.Id == laborCosts.Id);

            if(result != null)
            {
                result.Date = laborCosts.Date;
                result.HourCount = laborCosts.HourCount;
                if (laborCosts.AssignmentId != 0)
                {
                    result.AssignmentId = laborCosts.AssignmentId;
                }
                else if (laborCosts.Employee != null)
                {
                    result.AssignmentId = laborCosts.Assignment.Id;
                }
                if (laborCosts.EmployeeId != 0)
                {
                    result.EmployeeId = laborCosts.EmployeeId;
                }
                else if(laborCosts.Employee != null)
                {
                    result.EmployeeId = laborCosts.Employee.Id;
                }
     
                await _db.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}
