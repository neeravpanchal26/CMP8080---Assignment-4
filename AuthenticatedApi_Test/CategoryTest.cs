using AuthenticatedApi_Library;

namespace AuthenticatedApi_Test;

[TestClass]
public class CategoryTests
{
    [TestMethod]
    public void Category_SetAndGetId_Success()
    {
        var category = new Category();
        var expectedId = 123;
        
        category.Id = expectedId;
        var actualId = category.Id;

        Assert.AreEqual(expectedId, actualId);
    }

    [TestMethod]
    public void Category_SetAndGetDescription_Success()
    {            
        var category = new Category();
        var expectedDescription = "Test Description";

        category.Description = expectedDescription;
        var actualDescription = category.Description;

        Assert.AreEqual(expectedDescription, actualDescription);
    }

    [TestMethod]
    public void Category_DefaultConstructor_IdDefaultValueZero()
    {
        var category = new Category();
        
        Assert.AreEqual(0, category.Id);
    }

    [TestMethod]
    public void Category_DefaultConstructor_DescriptionDefaultValueNull()
    {    
        var category = new Category();
        
        Assert.IsNull(category.Description);
    }
}
