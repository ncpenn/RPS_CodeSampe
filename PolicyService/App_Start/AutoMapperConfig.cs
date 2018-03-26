using AutoMapper;
using Service.PolicyService.DAL;
using Service.PolicyService.Models;
using System;

namespace Service.PolicyService.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<InsurancePolicy, Policy>()
                        .ForMember(dest => dest.EffectiveDate, s => s.MapFrom(o => o.EffectiveDate))
                        .ForMember(dest => dest.ExpirationDate, s => s.MapFrom(o => o.Expiration))
                        .ForMember(dest => dest.PolicyNumber, s => s.MapFrom(o => o.PolicyNumber))
                        .ForMember(dest => dest.PrimaryInsured, s => s.MapFrom(o => new DAL.PrimaryInsured
                        {
                            FirstName = o.PrimaryInsured.PrimaryInsuredFirstName,
                            LastName = o.PrimaryInsured.PrimaryInsuredLastName,
                            MiddleName = o.PrimaryInsured.PrimaryInsuredMiddleName,
                            Address = new DAL.Address
                            {
                                Street = o.PrimaryInsured.PrimaryInsuredAddress.Street,
                                City = o.PrimaryInsured.PrimaryInsuredAddress.City,
                                State = o.PrimaryInsured.PrimaryInsuredAddress.State,
                                ZipCode = o.PrimaryInsured.PrimaryInsuredAddress.ZipCode,
                            },
                            PolicyNumber = int.Parse(o.PolicyNumber)
                        }))
                        .ForMember(dest => dest.RiskInsured, s => s.MapFrom(o => new RiskInsured
                        {
                            Construction = int.Parse(o.InsuredRisk.ConstructionType.Id),
                            YearBuilt = DateTime.Parse(o.InsuredRisk.YearBuilt),
                            PolicyNumber = int.Parse(o.PolicyNumber),
                            Address = new DAL.Address
                            {
                                Street = o.InsuredRisk.Location.Street,
                                City = o.InsuredRisk.Location.City,
                                State = o.InsuredRisk.Location.State,
                                ZipCode = o.InsuredRisk.Location.ZipCode,
                            }
                        }));
                });
        }
    }
}