using System.Collections.Generic;
using System.Linq;
using BestinClass.Core.Application_Service.Service;
using BestinClass.Core.Domain_Service;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Impl
{
    public class TestEntityService : ITestEntityService
    {

        private readonly ITestEntityRepository _testEntityRepository;

        public TestEntityService(ITestEntityRepository testEntityRepository)
        {
            _testEntityRepository = testEntityRepository;
        }
        
        public TestEntity NewTestEntity(string stringTest, int intTest)
        {
            var testEntity = new TestEntity()
            {
                StringTest = stringTest,
                IntTest = intTest,
            };

            return testEntity;
        }

        public TestEntity CreateTestEntity(TestEntity testEntity)
        {
            return _testEntityRepository.CreateTestEntity(testEntity);
        }

        public List<TestEntity> GetAllTestEntites()
        {
            return _testEntityRepository.ReadAllTestEntities().ToList();
        }

        public TestEntity GetTestEntityById(int id)
        {
            return _testEntityRepository.GetTestEntityById(id);
        }

        public TestEntity UpdateTestEntity(TestEntity testEntityUpdate)
        {
            return _testEntityRepository.UpdateTestEntity(testEntityUpdate);
        }

        public void DeleteTestEntity(int id)
        {
            _testEntityRepository.DeleteTestEntity(id);
        }
    }
}