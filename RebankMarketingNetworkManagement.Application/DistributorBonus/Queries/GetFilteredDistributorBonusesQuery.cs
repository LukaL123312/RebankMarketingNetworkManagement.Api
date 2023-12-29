using MediatR;
using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Dtos;
using System.ComponentModel.DataAnnotations;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Queries;

public class GetFilteredDistributorBonusesQuery : IRequest<List<DistributorBonusDto>>
{
    [FromQuery(Name = "name")]
    public string? DistributorName { get; set; }

    [FromQuery(Name = "surname")]
    public string? DistributorSurname { get; set; }

    [Required]
    [FromQuery(Name = "startDate")]
    public DateTime StartDate { get; set; }

    [Required]
    [FromQuery(Name = "endDate")]
    public DateTime EndDate { get; set; }

    [FromQuery(Name = "minValue")]
    public decimal? MinimumValue { get; set; }

    [FromQuery(Name = "maxValue")]
    public decimal? MaximumValue { get; set; }
}