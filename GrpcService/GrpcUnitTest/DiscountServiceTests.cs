using GrpcService;
using GrpcService.Protos; 
using GrpcService.Services;
using Microsoft.EntityFrameworkCore;
using DiscountService = GrpcService.Services.DiscountService; // Para o DbContext e métodos relacionados

namespace GrpcUnitTest;

public class DiscountServiceTests
{
	private static DiscountDbContext GetInMemoryDbContext()
	{
		var options = new DbContextOptionsBuilder<DiscountDbContext>()
			.UseInMemoryDatabase(databaseName: "DiscountTestDb")
			.Options;

		return new DiscountDbContext(options);
	}

	[Fact]
	public async Task GenerateDiscount_ShouldGenerateCodes()
	{
		// Arrange
		var dbContext = GetInMemoryDbContext();
		var service = new DiscountService(dbContext);
		var request = new GenerateRequest { Count = 5, Length = 8 };

		// Act
		var response = await service.GenerateDiscount(request, null);

		// Assert
		Assert.Equal(5, response.Codes.Count); // Verify if the 5 codes was generated
		Assert.All(response.Codes, code => Assert.Equal(8, code.Length)); // Verify if the code has 8 characters
	}

	[Fact]
	public async Task UseDiscount_ShouldMarkCodeAsUsed()
	{
		// Arrange
		var dbContext = GetInMemoryDbContext();
		var service = new DiscountService(dbContext);
        
		// First let's generate an discount
		var generateRequest = new GenerateRequest { Count = 1, Length = 8 };
		var generateResponse = await service.GenerateDiscount(generateRequest, null);
		var code = generateResponse.Codes[0];

		// Act - Let's use the descount
		var useRequest = new UseRequest { Code = code };
		var useResponse = await service.UseDiscount(useRequest, null);

		// Assert
		Assert.True(useResponse.Success); // Verify if was successful
        
		// Now let's check if the code as marked as used
		var discountCode = await dbContext.DiscountCodes.FirstOrDefaultAsync(c => c.Code == code);
		Assert.True(discountCode.IsUsed);
	}
}