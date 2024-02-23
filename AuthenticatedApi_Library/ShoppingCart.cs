using System;
using System.Collections.Generic;

namespace AuthenticatedApi_Library;
public class ShoppingCart
{
    public int Id { get; set; }
    public AppUser User { get; set; }
    public List<Product> Products { get; set; }
}
