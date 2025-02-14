using AutoMapper;
using Factoring.Domain.Contracts.Dtos;
using Factoring.Domain.Invoices.Dtos;
using Factoring.Infrastructure.Entities;

namespace Factoring.Domain.Contracts.MappingProfile
{
    public class FactoringContractMappingProfile : Profile
    {

        public FactoringContractMappingProfile()
        {
            CreateMap<FactoringContract, ContractResponse>()
                .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.ContractId))
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.IssueDate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.DebtorName, opt => opt.MapFrom(src => src.DebtorName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.BankName, opt => opt.MapFrom(src => src.Bank.Name))
                .ForMember(dest => dest.BankSWIFT, opt => opt.MapFrom(src => src.Bank.SWIFT))
                .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => src.Invoices));

            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
