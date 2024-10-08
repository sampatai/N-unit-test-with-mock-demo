using NUnit.Framework;
using Moq;
using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using NUnitTest;

[TestFixture]
public class ProductRepositoryTests
{
    private Mock<IProductRepository> _mockRepository;
    private List<Product> _mockProducts;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _mockProducts = _fixture.CreateMany<Product>(3).ToList();

        _mockRepository = new Mock<IProductRepository>();
        _mockRepository.Setup(repo => repo.GetAll()).Returns(_mockProducts);
        _mockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((int id) => _mockProducts.FirstOrDefault(p => p.Id == id));
        _mockRepository.Setup(repo => repo.Add(It.IsAny<Product>())).Callback((Product product) => _mockProducts.Add(product));
        _mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).Callback((Product product) =>
        {
            var existingProduct = _mockProducts.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
        });
        _mockRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Callback((int id) =>
        {
            var product = _mockProducts.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _mockProducts.Remove(product);
            }
        });
    }

    [Test]
    public void Add_ShouldAddProduct()
    {
        var product = _fixture.Create<Product>();
        product.Id = 4; 

        _mockRepository.Object.Add(product);

        Assert.Equals(4, _mockProducts.Count);
        Assert.Equals(product.Name, _mockProducts.Last().Name);
    }

    [Test]
    public void GetAll_ShouldReturnAllProducts()
    {
        var result = _mockRepository.Object.GetAll();

        Assert.Equals(3, result.Count());
    }

    [Test]
    public void Update_ShouldUpdateProduct()
    {
        var product = _mockProducts.First();
        product.Name = "Updated Product";
        product.Price = 15.0;

        _mockRepository.Object.Update(product);

        var updatedProduct = _mockProducts.First(p => p.Id == product.Id);
        Assert.Equals("Updated Product", updatedProduct.Name);
        Assert.Equals(15.0, updatedProduct.Price);
    }

    [Test]
    public void Delete_ShouldRemoveProduct()
    {
        var product = _mockProducts.First();

        _mockRepository.Object.Delete(product.Id);

        Assert.Equals(2, _mockProducts.Count);
       
    }
}
