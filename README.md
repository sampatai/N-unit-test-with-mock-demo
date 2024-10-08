# ProductRepositoryTests

This project contains unit tests for the `ProductRepository` class using NUnit, Moq, and AutoFixture. The tests ensure that the repository methods work as expected.

## Getting Started

### Prerequisites

- .NET SDK
- NUnit
- Moq
- AutoFixture


### Add_ShouldAddProduct
This test verifies that a product can be added to the repository.

- Arrange: Create a new product with a unique ID.
- Act: Add the product to the repository.
- Assert: Check that the product count has increased and the last product in the list matches the added product.
### GetAll_ShouldReturnAllProducts
This test ensures that all products can be retrieved from the repository.

- Arrange: (Setup already handles this)
- Act: Retrieve all products from the repository.
- Assert: Verify that the count of retrieved products matches the expected count

### Update_ShouldUpdateProduct
This test checks that a product can be updated in the repository.

- Arrange: Modify the name and price of an existing product.
- Act: Update the product in the repository.
- Assert: Confirm that the product’s details have been updated correctly.
