using ETicaret.Business.Concrete;
using ETicaret.Business.DTOs;

namespace ETicaret.Tests.Business;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateAsync_DuplicateName_Fails()
    {
        var sut = new CategoryService(TestDb.Uow(TestDb.Create()));

        var result = await sut.CreateAsync(new CategorySaveDto { Name = "Giyim" });

        Assert.False(result.Success);
        Assert.Equal("Bu isimde bir kategori zaten var.", result.Message);
    }

    [Fact]
    public async Task DeleteAsync_WithActiveProducts_Fails()
    {
        var sut = new CategoryService(TestDb.Uow(TestDb.Create()));

        var result = await sut.DeleteAsync(1);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task DeleteAsync_EmptyCategory_SoftDeletesAndHidesFromList()
    {
        var sut = new CategoryService(TestDb.Uow(TestDb.Create()));
        var created = await sut.CreateAsync(new CategorySaveDto { Name = "Kitap" });

        var result = await sut.DeleteAsync(created.Data);

        Assert.True(result.Success);
        Assert.DoesNotContain(await sut.GetAllAsync(), c => c.Name == "Kitap");
    }

    [Fact]
    public async Task GetAllAsync_CountsOnlyActiveProducts()
    {
        var db = TestDb.Create();
        db.Products.Find(1)!.IsActive = false;
        db.SaveChanges();
        var sut = new CategoryService(TestDb.Uow(db));

        var electronics = (await sut.GetAllAsync()).Single(c => c.Id == 1);

        Assert.Equal(7, electronics.ProductCount);
    }
}
