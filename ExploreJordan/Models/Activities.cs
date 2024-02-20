﻿using System.ComponentModel.DataAnnotations;

namespace ExploreJordan.Models
{
	public class Activities
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Double Price { get; set; }
		public string Cover { get; set; } = string.Empty;
		public string UserId { get; internal set; }
		public string Address { get; internal set; } = string.Empty;
		public string City { get; set; } = string.Empty;

	}
}
