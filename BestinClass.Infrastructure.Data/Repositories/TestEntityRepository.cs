using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BestinClass.Infrastructure.Data.Repositories
{
    public class TestEntityRepository : ITestEntityRepository
    {

        private readonly BestinClassContext _ctx;

        public TestEntityRepository(BestinClassContext ctx)
        {
            _ctx = ctx;
        }
        
        public TestEntity CreateTestEntity(TestEntity testEntity)
        {
            _ctx.Attach(testEntity).State = EntityState.Added;
            _ctx.SaveChanges();
            return testEntity;
        }

        public IEnumerable<TestEntity> ReadAllTestEntities()
        {
            return _ctx.TestEntity;
        }

        public TestEntity GetTestEntityById(int id)
        {
            return _ctx.TestEntity
                .FirstOrDefault(t => t.Id == id);
        }

        public TestEntity UpdateTestEntity(TestEntity testEntityUpdate)
        {
            _ctx.Attach(testEntityUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return testEntityUpdate;        }

        public TestEntity DeleteTestEntity(int id)
        {
            var removed = _ctx.Remove(new TestEntity {Id = id}).Entity;
            _ctx.SaveChanges();
            return removed;        }
    }
}