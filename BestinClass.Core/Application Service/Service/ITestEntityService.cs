using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Application_Service.Service
{
    public interface ITestEntityService
    {
        TestEntity NewTestEntity(string stringTest, int intTest);
        TestEntity CreateTestEntity(TestEntity testEntity);
        
        //READ
        List<TestEntity> GetAllTestEntites();
        TestEntity GetTestEntityById(int id);

        //UPDATE
        TestEntity UpdateTestEntity(TestEntity testEntityUpdate);

        //DELETE
        void DeleteTestEntity(int id);
    }
}