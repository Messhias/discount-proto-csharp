using Grpc.Core;
using GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class DiscountService(DiscountDbContext dbContext) : Protos.DiscountService.DiscountServiceBase
    {
        public override async Task<GenerateResponse> GenerateDiscount(GenerateRequest request, ServerCallContext context)
        {
            var response = new GenerateResponse();

            // Generate random codes
            var random = new Random();
            for (int i = 0; i < request.Count; i++)
            {
                var code = new string(Enumerable.Range(0, (int)request.Length)
                    .Select(_ => (char)random.Next('A', 'Z' + 1)).ToArray());

                // We'll add the codes into the databse.
                var discountCode = new DiscountCode
                {
                    Code = code,
                    IsUsed = false
                };

                dbContext.DiscountCodes.Add(discountCode);
                response.Codes.Add(code); // Add to result callback
            }

            // wait for the db context save it to database.
            await dbContext.SaveChangesAsync();

            return response;
        }

        public override async Task<UseResponse> UseDiscount(UseRequest request, ServerCallContext context)
        {
            var response = new UseResponse();

            // Check if the code is already exists.
            var discountCode = await dbContext.DiscountCodes
                .FirstOrDefaultAsync(c => c.Code == request.Code && !c.IsUsed);

            if (discountCode == null)
            {
                response.Success = false; // code is already used
            }
            else
            {
                discountCode.IsUsed = true;
                await dbContext.SaveChangesAsync();
                response.Success = true; // It's ok :) 
            }

            return response;
        }
    }
}
