using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using IntermediateLab_Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IntermediateLab_Backend.Application.DTO.Tournament;

public record CreateTournamentDTO
{
	[MaxLength(100)]
	public string   Name                { get; set; } = null!;
	[MaxLength(200)]
	public string?  Location            { get; set; }
	[Required]
	public DateTime InscriptionsEndDate { get; set; }

	[Required]
	[Range(2,32)]
	public int      MaxPlayerAmount     { get; set; }
	[Required]
	[Range(2,32)]
	public int      MinPlayerAmount     { get; set; }
	[Range(0,3000)]
	public int?     MaxPlayerElo        { get; set; } = 3000;
	[Range(0,3000)]
	public int?     MinPlayerElo        { get; set; } = 0;
	[Required]
	public int[] Categories { get; set; }
	[Required]
	public bool     IsWomenOnly         { get; set; } = false;
};