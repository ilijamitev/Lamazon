using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Enums;
using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lamazon.Services.Implementations
{
    public class InvoicesService : BaseService, IInvoicesService
    {
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly IMapper _mapper;

        public InvoicesService(IInvoicesRepository invoicesRepository, IMapper mapper)
        {
            _invoicesRepository = invoicesRepository;
            _mapper = mapper;
        }

        public async Task CancelInvoice(int id)
        {
            var invoice = await _invoicesRepository.GetById(id);
            invoice.InvoiceStatusId = (int)InvoiceStatusEnum.Canceled;
            await _invoicesRepository.Update(invoice);
        }

        public async Task CreateInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = _mapper.Map<Invoice>(invoiceViewModel);
            int invoiceId = await _invoicesRepository.Insert(invoice);
            if (invoiceId <= 0)
            {
                throw new Exception($"Something went wrong while saving the new invoice");
            }
        }

        public async Task<List<InvoiceViewModel>> GetAllInvoices()
        {
            var invoices = await _invoicesRepository.GetAllInvoices();
            return _mapper.Map<List<InvoiceViewModel>>(invoices);
        }

        public async Task<PagedResultViewModel<InvoiceViewModel>> GetFilteredInvoices(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel?.search?.value ?? string.Empty;
            var invoicesPagedResult = await _invoicesRepository.GetFilteredInvoices(datatableRequestViewModel.start, datatableRequestViewModel.length, searchValue, datatableRequestViewModel.sortColumn, datatableRequestViewModel.isAscending);
            return _mapper.Map<PagedResultViewModel<InvoiceViewModel>>(invoicesPagedResult);
        }

        public async Task<InvoiceViewModel> GetInvoiceById(int id)
        {
            var invoice = await _invoicesRepository.GetById(id);
            return _mapper.Map<InvoiceViewModel>(invoice);
        }

        public async Task SetAsPaid(int id)
        {
            var invoice = await _invoicesRepository.GetById(id);
            invoice.InvoiceStatusId = (int)InvoiceStatusEnum.Paid;
            await _invoicesRepository.Update(invoice);
        }

        public async Task UpdateInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = _mapper.Map<Invoice>(invoiceViewModel);
            await _invoicesRepository.Update(invoice);
        }
    }
}
