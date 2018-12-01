using System.Collections.Generic;
using BestinClass.Core.Entity;

namespace BestinClass.Core.Domain_Service
{
    public interface ITestEntityRepository

    {
    //CREATE
    TestEntity CreateTestEntity(TestEntity testEntity);

    //READ
    IEnumerable<TestEntity> ReadAllTestEntities();

    TestEntity GetTestEntityById(int id);

    //UPDATE
    TestEntity UpdateTestEntity(TestEntity testEntityUpdate);

    //DELETE
    TestEntity DeleteTestEntity(int id);
    }
}