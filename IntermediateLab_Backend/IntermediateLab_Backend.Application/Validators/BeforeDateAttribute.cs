using System.ComponentModel.DataAnnotations;

namespace IntermediateLab_Backend.Application.Validators;

public class BeforeDateAttribute(in DateTime date) : ValidationAttribute
{
	private DateTime _date { get; set; } = date;

	public override bool IsValid(object? value)
	{
		if (value == null)
			return false;
		
		return base.IsValid(value);
	}
}