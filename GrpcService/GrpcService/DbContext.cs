using Microsoft.EntityFrameworkCore;
using GrpcService.Models;

namespace GrpcService
{
	public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
	{
		public DbSet<DiscountCode> DiscountCodes { get; set; }
	}
}