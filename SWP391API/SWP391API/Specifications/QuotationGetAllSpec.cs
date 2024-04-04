﻿using Ardalis.Specification;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Specifications
{
    public class QuotationGetAllSpec : Specification<Quotation>
    {
        public QuotationGetAllSpec(QuotationFilterDTO quotationFilterDTO)
        {
            Query
                .Include(q => q.User)
                .Include(q => q.QuotationDetails)
                .Search(q => q.User.Fullname,
                "%" + quotationFilterDTO.FullnameContains + "%", quotationFilterDTO.FullnameContains != null)
                .Where(q => q.Status == quotationFilterDTO.QuotationStatus,
                    quotationFilterDTO.QuotationStatus != null)
                .Where(q => q.CreatedAt >= quotationFilterDTO.FromDate,
                    quotationFilterDTO.FromDate != null)
                .Where(q => q.CreatedAt <= quotationFilterDTO.ToDate,
                                   quotationFilterDTO.ToDate != null)
                .Where(q => q.TotalBill >= quotationFilterDTO.MinTotalBill,
                                   quotationFilterDTO.MinTotalBill != null)
                .Where(q => q.TotalBill <= quotationFilterDTO.MaxTotalBill,
                                   quotationFilterDTO.MaxTotalBill != null);
        }
    }
}
