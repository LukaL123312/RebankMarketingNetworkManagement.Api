using RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.UpdateDistributorByIdCommand;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;
using RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;
using RebankMarketingNetworkManagement.Domain;

namespace RebankMarketingNetworkManagement.Application.Distributor.Mapping.Commands;

public static class DistributorMappingHelper
{
    public static Domain.Distributor CommandToEntityMapper(this AddDistributorCommand addDistributorCommand)
    {
        return new Domain.Distributor()
        {
            Name = addDistributorCommand.Name,
            Surname = addDistributorCommand.Surname,
            BirthDate = addDistributorCommand.BirthDate,
            Gender = addDistributorCommand.Gender,
            Photo = addDistributorCommand.Photo,
            PrivateDocumentInformation = new DistributorPrivateDocumentInformation()
            {
                DocumentType = addDistributorCommand.PrivateDocumentInformation.DocumentType,
                SerialNumber = addDistributorCommand.PrivateDocumentInformation.SerialNumber,
                Number = addDistributorCommand.PrivateDocumentInformation.Number,
                IssuingDate = addDistributorCommand.PrivateDocumentInformation.IssuingDate,
                ExpiryDate = addDistributorCommand.PrivateDocumentInformation.ExpiryDate,
                PrivateNumber = addDistributorCommand.PrivateDocumentInformation.PrivateNumber,
                IssuerOrganization = addDistributorCommand.PrivateDocumentInformation.IssuerOrganization,
            },
            ContactInformation = new DistributorContactInformation()
            {
                ContactType = addDistributorCommand.ContactInformation.ContactType,
                ContactInformation = addDistributorCommand.ContactInformation.ContactInformation,
            },
            AddressInformation = new DistributorAddressInformation()
            {
                AddressType = addDistributorCommand.AddressInformation.AddressType,
                Address = addDistributorCommand.AddressInformation.Address,
            },
            RecommenderID = addDistributorCommand.RecommenderID,
        };
    }

    public static Domain.Distributor CommandToEntityMapper(this Domain.Distributor distributor,
        UpdateDistributorByIdCommand updateDistributorByIdCommand)
    {
        if (!string.IsNullOrEmpty(updateDistributorByIdCommand.Name))
        {
            distributor.Name = updateDistributorByIdCommand.Name;
        }

        if (!string.IsNullOrEmpty(updateDistributorByIdCommand.Surname))
        {
            distributor.Surname = updateDistributorByIdCommand.Surname;
        }

        if (updateDistributorByIdCommand.BirthDate.HasValue)
        {
            distributor.BirthDate = updateDistributorByIdCommand.BirthDate.Value;
        }

        if (updateDistributorByIdCommand.Gender.HasValue)
        {
            distributor.Gender = updateDistributorByIdCommand.Gender.Value;
        }

        if (updateDistributorByIdCommand.Photo != null)
        {
            distributor.Photo = updateDistributorByIdCommand.Photo;
        }

        if (updateDistributorByIdCommand.RecommenderID.HasValue)
        {
            distributor.RecommenderID = updateDistributorByIdCommand.RecommenderID.Value;
        }

        if (updateDistributorByIdCommand.PrivateDocumentInformation != null)
        {
            distributor.PrivateDocumentInformation
                .UpdatePrivateDocumentInformationDtoToEntityMapper(updateDistributorByIdCommand.PrivateDocumentInformation);
        }

        if (updateDistributorByIdCommand.ContactInformation != null)
        {
            distributor.ContactInformation
                .UpdateContactInformationDtoToEntityMapper(updateDistributorByIdCommand.ContactInformation);
        }

        if (updateDistributorByIdCommand.AddressInformation != null)
        {
            distributor.AddressInformation
                .UpdateAddressInformationDtoToEntityMapper(updateDistributorByIdCommand.AddressInformation);
        }

        distributor.UpdatedDateTime = DateTime.UtcNow;

        return distributor;
    }

    public static DistributorPrivateDocumentInformation UpdatePrivateDocumentInformationDtoToEntityMapper(this DistributorPrivateDocumentInformation entity,
        DistributorPrivateDocumentInformationUpdateDto dto)
    {
        if (entity is null)
        {
            return new DistributorPrivateDocumentInformation
            {
                DocumentType = dto.DocumentType.Value,
                SerialNumber = dto.SerialNumber,
                Number = dto.Number,
                IssuingDate = dto.IssuingDate.Value,
                ExpiryDate = dto.ExpiryDate.Value,
                PrivateNumber = dto.PrivateNumber,
                IssuerOrganization = dto.IssuerOrganization,
            };
        }


        if (dto.DocumentType.HasValue)
        {
            entity.DocumentType = dto.DocumentType.Value;
        }

        if (!string.IsNullOrEmpty(dto.SerialNumber))
        {
            entity.SerialNumber = dto.SerialNumber;
        }

        if (!string.IsNullOrEmpty(dto.Number))
        {
            entity.Number = dto.Number;
        }

        if (dto.IssuingDate.HasValue)
        {
            entity.IssuingDate = dto.IssuingDate.Value;
        }

        if (dto.ExpiryDate.HasValue)
        {
            entity.ExpiryDate = dto.ExpiryDate.Value;
        }

        if (!string.IsNullOrEmpty(dto.PrivateNumber))
        {
            entity.PrivateNumber = dto.PrivateNumber;
        }

        if (!string.IsNullOrEmpty(dto.IssuerOrganization))
        {
            entity.IssuerOrganization = dto.IssuerOrganization;
        }

        return entity;
    }

    public static DistributorContactInformation UpdateContactInformationDtoToEntityMapper(this DistributorContactInformation entity,
    DistributorContactInformationUpdateDto dto)
    {
        if (entity is null)
        {
            return new DistributorContactInformation
            {
                ContactType = dto.ContactType.Value,
                ContactInformation = dto.ContactInformation,
            };
        }
        if (dto.ContactType.HasValue)
        {
            entity.ContactType = dto.ContactType.Value;
        }

        if (!string.IsNullOrEmpty(dto.ContactInformation))
        {
            entity.ContactInformation = dto.ContactInformation;
        }

        return entity;
    }

    public static DistributorAddressInformation UpdateAddressInformationDtoToEntityMapper(this DistributorAddressInformation entity,
    DistributorAddressInformationUpdateDto dto)
    {
        if (entity is null)
        {
            return new DistributorAddressInformation
            {
                AddressType = dto.AddressType.Value,
                Address = dto.Address,
            };
        }

        if (dto.AddressType.HasValue)
        {
            entity.AddressType = dto.AddressType.Value;
        }

        if (!string.IsNullOrEmpty(dto.Address))
        {
            entity.Address = dto.Address;
        }

        return entity;
    }

    public static DistributorDto DistributorEntityToDto(this Domain.Distributor entity)
    {
        return new DistributorDto
        {
            Name = entity.Name,
            Surname = entity.Surname,
            Gender = entity.Gender,
            BirthDate = entity.BirthDate,
            Photo = entity.Photo,
            AddressInformation = new DistributorAddressInformationDto()
            {
                AddressType = entity.AddressInformation.AddressType,
                Address = entity.AddressInformation.Address,
            },
            ContactInformation = new DistributorContactInformationDto()
            {
                ContactType = entity.ContactInformation.ContactType,
                ContactInformation = entity.ContactInformation.ContactInformation,
            },
            PrivateDocumentInformation = new DistributorPrivateDocumentInformationDto()
            {
                DocumentType = entity.PrivateDocumentInformation.DocumentType,
                SerialNumber = entity.PrivateDocumentInformation.SerialNumber,
                Number = entity.PrivateDocumentInformation.Number,
                PrivateNumber = entity.PrivateDocumentInformation.Number,
                IssuingDate = entity.PrivateDocumentInformation.IssuingDate,
                ExpiryDate = entity.PrivateDocumentInformation.ExpiryDate,
                IssuerOrganization = entity.PrivateDocumentInformation.IssuerOrganization
            },
            RecommenderID = entity.RecommenderID,
            RecommendedDistributorIDs = entity.RecommendedDistributors?.Select(rd => rd.DistributorID).ToList() ?? new List<Guid>()
        };
    }
}