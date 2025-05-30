using System;
using Core.Entities;

namespace Core.Interfaces;

public interface ICartService
{
    Task<ShoppingCart?> GetCartAsync(string key);
    Task<ShoppingCart?> UpdateCartAsync(ShoppingCart cart);
    Task<bool> DeleteCartAsync(string key);
}
