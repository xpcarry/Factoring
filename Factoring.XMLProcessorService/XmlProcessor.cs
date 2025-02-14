using Factoring.Infrastructure.Entities;
using Factoring.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Xml.Linq;
using Factoring.Domain.Enums;

namespace Factoring.XMLProcessorService
{
    public class XmlProcessor(
        ILoggerFactory loggerFactory,
        FactoringDbContext dbContext)
    {
        private readonly ILogger<XmlProcessor> _logger = loggerFactory.CreateLogger<XmlProcessor>();

        public async Task ProcessXmlFile(string filePath)
        {
            var xml = XElement.Load(filePath);

            foreach (var contractXml in xml.Elements("Contract"))
            {
                if (!ValidateContractXml(contractXml, filePath))
                {
                    continue;
                }

                var bank = await dbContext.Banks.FirstOrDefaultAsync(b =>
                    b.SWIFT == contractXml.Element("BankSWIFT").Value);

                if (bank == null)
                {
                    _logger.LogError($"Bank with SWIFT {contractXml.Element("BankSWIFT").Value} not found in file {filePath}");
                    continue;
                }

                var statusValue = contractXml.Element("Status")?.Value ?? "Active";

                if (!Enum.TryParse(statusValue, true, out ContractStatusEnum status))
                {
                    status = ContractStatusEnum.Active;
                }

                var contract = new FactoringContract
                {
                    IssueDate = DateTime.Parse(contractXml.Element("IssueDate").Value),
                    Amount = decimal.Parse(contractXml.Element("Amount").Value, CultureInfo.InvariantCulture),
                    BankId = bank.BankId,
                    DebtorName = contractXml.Element("Debtor").Value,
                    Status = status,
                };

                var invoicesElement = contractXml.Element("Invoices");

                if (invoicesElement != null && invoicesElement.Elements("Invoice").Any())
                {
                    foreach (var invoiceXml in invoicesElement.Elements("Invoice"))
                    {
                        contract.Invoices.Add(new Invoice
                        {
                            InvoiceNumber = invoiceXml.Element("Number")?.Value ?? "N/A",
                            DueDate = DateTime.TryParse(invoiceXml.Element("DueDate")?.Value, out var dueDate) ? dueDate : DateTime.MaxValue,
                            Amount = decimal.TryParse(invoiceXml.Element("Amount")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount) ? amount : 0m,
                        });
                    }
                }

                await dbContext.AddAsync(contract);
                await dbContext.SaveChangesAsync();
            }
        }

        private bool ValidateContractXml(XElement contractXml, string filePath)
        {
            if (contractXml.Element("BankSWIFT") == null || contractXml.Element("IssueDate") == null ||
                contractXml.Element("Amount") == null || contractXml.Element("Debtor") == null || contractXml.Element("Status") == null)
            {
                _logger.LogError($"Invalid contract structure in file {filePath}");

                return false;
            }

            if (!decimal.TryParse(contractXml.Element("Amount").Value, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                _logger.LogError($"Invalid amount format in file {filePath}");

                return false;
            }

            return true;
        }
    }
}
