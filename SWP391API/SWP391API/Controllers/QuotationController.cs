using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context = new InteriorConstructionQuotationSystemContext();

        [HttpGet]
        public IActionResult GetQuotationTemps()
        {
            List<QuotationTemp> quotationTemps = _context.QuotationTemps
            .Include(qt => qt.Product)
            .Include(qt => qt.User)
            .ToList();
            List<QuotationTempsResponse> responses = new List<QuotationTempsResponse>();

            foreach (var quotationTemp in quotationTemps)
            {
                responses.Add(new QuotationTempsResponse(quotationTemp));
            }
            return Ok(responses);
        }

      

        [HttpPost]
        public IActionResult AddQuotationTemp(QuotationTempRequest quotationTemp)
        {
            QuotationTemp checkExist = _context.QuotationTemps.FirstOrDefault(qt => qt.UserId == quotationTemp.UserId && qt.ProductId == quotationTemp.ProductId);
            if (checkExist == null)
            {
                QuotationTemp quotation = new QuotationTemp();
                quotation.ProductId = quotationTemp.ProductId;
                quotation.Quantity = quotationTemp.Quantity;
                quotation.UserId = quotationTemp.UserId;

                _context.QuotationTemps.Add(quotation);
                _context.SaveChanges();
            }
            else
            {
                checkExist.Quantity = checkExist.Quantity + quotationTemp.Quantity;
                _context.QuotationTemps.Update(checkExist);
                _context.SaveChanges();
            }

            
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateQuotationTemp(QuotationTempRequest quotationTemp)
        {
            QuotationTemp quotation = new QuotationTemp();
            quotation.ProductId = quotationTemp.ProductId;
            quotation.Quantity = quotationTemp.Quantity;
            quotation.UserId = quotationTemp.UserId;
            _context.QuotationTemps.Update(quotation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult DeleteQuotationTemp(int userId, int productId)
        {
            var quotationTemp = _context.QuotationTemps.FirstOrDefault(qt => qt.UserId == userId && qt.ProductId == productId);

            if (quotationTemp != null)
            {
                _context.QuotationTemps.Remove(quotationTemp);
                _context.SaveChanges();
            }
            return Ok();
        }

    }
}
