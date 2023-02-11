﻿using System;
using EntityLayer.Common;

namespace EntityLayer.Concrete;

public class Product : BaseEntity
{
	public string? Name { get; set; }
	public int Stock { get; set; }
	public decimal Price { get; set; }
}

