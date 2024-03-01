using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;
using System.Security.Claims;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context = new InteriorConstructionQuotationSystemContext();

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetQuotationTemps()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[1].Value;
            int uID = int.Parse(userId);
            List<QuotationTemp> quotationTemps = _context.QuotationTemps
            .Include(qt => qt.Product)
            .Include(qt => qt.User).Where(x=>x.UserId == uID)
            .ToList();
            List<QuotationTempsResponse> responses = new List<QuotationTempsResponse>();

            foreach (var quotationTemp in quotationTemps)
            {
                responses.Add(new QuotationTempsResponse(quotationTemp));
            }
            return Ok(responses);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddQuotationTemp(QuotationTempRequest quotationTemp)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userIdValue = claim[1].Value;
            var product = _context.Products.FirstOrDefault(x => x.ProductId == quotationTemp.ProductId);
            if (product == null)
            {
                return Ok("Product Id not exist. Try Again!");
            }
            int userId = int.Parse(userIdValue);
            QuotationTemp checkExist = _context.QuotationTemps.FirstOrDefault(qt => qt.UserId == userId && qt.ProductId == quotationTemp.ProductId);
            if (checkExist == null)
            {
                QuotationTemp quotation = new QuotationTemp();
                quotation.ProductId = quotationTemp.ProductId;
                quotation.Quantity = quotationTemp.Quantity;
                quotation.UserId = userId;

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateQuotationTemp(QuotationTempRequest quotationTemp)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == quotationTemp.ProductId);
            if (product == null)
            {
                return Ok("Product Id not exist. Try Again!");
            }
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userIdValue = claim[1].Value;
            int userId = int.Parse(userIdValue);

            QuotationTemp quotation = new QuotationTemp();
            quotation.ProductId = quotationTemp.ProductId;
            quotation.Quantity = quotationTemp.Quantity;
            quotation.UserId = userId;
            _context.QuotationTemps.Update(quotation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{userId}/{productId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteQuotationTemp( int productId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userIdValue = claim[1].Value;
            int userId = int.Parse(userIdValue);
            var quotationTemp = _context.QuotationTemps.FirstOrDefault(qt => qt.UserId == userId && qt.ProductId == productId);

            if (quotationTemp != null)
            {
                _context.QuotationTemps.Remove(quotationTemp);
                _context.SaveChanges();
                return Ok();

            }
            else
            {
                return Ok("Product not exist in quotation of this user . Try Again!");

            }
        }

    }
}
