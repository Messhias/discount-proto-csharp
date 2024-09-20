namespace GrpcService.Models;

public class DiscountCode
{
	public int Id { get; set; }
	public string Code { get; set; } = "";
	public bool IsUsed { get; set; }
}