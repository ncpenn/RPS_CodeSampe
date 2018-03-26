using AutoMapper;
using Website.PolicyWebsite.Models;
using System;

namespace Website.PolicyWebsite.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<AddPolicy, InsurancePolicy>()
                        .ForMember(dest => dest.EffectiveDate, s => s.MapFrom(o => o.EffectiveDate))
                        .ForMember(dest => dest.Expiration, s => s.MapFrom(o => o.ExpireDate))
                        .ForMember(dest => dest.PolicyNumber, s => s.MapFrom(o => o.PolicyNumber))
                        .ForMember(dest => dest.InsuredRisk, s => s.MapFrom(o => new InsuredRisk
                        {
                            ConstructionType = new BuildingType
                            {
                                Id = o.SelectedRiskConstruction
                            },
                            YearBuilt = o.YearBuilt,
                            Location = new Address
                            {
                                Street = o.BuildingStreet,
                                City = o.BuildingCity,
                                State = o.BuildingState,
                                ZipCode = o.BuildingZip
                            }
                        }))
                        .ForMember(dest => dest.PrimaryInsured, s => s.MapFrom(o => new PrimaryInsured
                        {
                            PrimaryInsuredFirstName = o.FirstName,
                            PrimaryInsuredLastName = o.LastName,
                            PrimaryInsuredAddress = new Address
                            {
                                Street = o.Street,
                                City = o.City,
                                State = o.State,
                                ZipCode = o.Zip
                            }
                        }));
                });

        }
    }
}