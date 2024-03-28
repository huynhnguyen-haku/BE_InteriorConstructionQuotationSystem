using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SWP391API.DTO;
using SWP391API.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;

        public QuotationController(InteriorConstructionQuotationSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetQuotationTemps()
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userId = claim[1].Value;
                int uID = int.Parse(userId);
                List<QuotationTemp> quotationTemps = _context.QuotationTemps
                .Include(qt => qt.Product)
                .Include(qt => qt.User).Where(x => x.UserId == uID)
                .ToList();
                List<QuotationTempsResponse> responses = new List<QuotationTempsResponse>();

                foreach (var quotationTemp in quotationTemps)
                {
                    responses.Add(new QuotationTempsResponse(quotationTemp));
                }
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok(responses);
            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }
        }
        [HttpGet("GetSubmitedQuotationsOfUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetSubmitedQuotationsOfUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userId = claim[1].Value;
                int uID = int.Parse(userId);
                List< Quotation> responses = _context.Quotations.Include(x => x.Style).Include(x => x.CeilingConstruct).Include(x => x.FloorConstruction).Include(x => x.HomeStyle).Include(x => x.WallConstruct).Include(x => x.User).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var jsonString = JsonSerializer.Serialize(responses, options);


                return Ok(responses);
            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }

        }

        [HttpGet("GetAllSubmitedQuotations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllSubmitedQuotations()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userId = claim[1].Value;
                int uID = int.Parse(userId);
                var responses = _context.Quotations.Include(x=>x.Style).Include(x => x.CeilingConstruct).Include(x => x.FloorConstruction).Include(x => x.HomeStyle).Include(x => x.WallConstruct).Include(x => x.User).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var jsonString = JsonSerializer.Serialize(responses, options);
                return Ok(responses);
            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }

        }

        [HttpGet("GetSubmitedQuotationById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetSubmitedQuotationById(int quotationId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userId = claim[1].Value;
                int uID = int.Parse(userId);
                var responses = _context.Quotations.Include(x => x.Style).Include(x => x.CeilingConstruct).Include(x => x.FloorConstruction).Include(x => x.HomeStyle).Include(x => x.WallConstruct).Include(x => x.User).Where(x => x.QuotationId == quotationId).FirstOrDefault();
                if (responses== null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(responses);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }

        }
        [HttpGet("GetSubmitedQuotationDetail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetSubmitedQuotationDetail(int quotationId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userId = claim[1].Value;
                int uID = int.Parse(userId);
                List<QuotationDetail> responses = _context.QuotationDetails.Include(x => x.Product).Where(x => x.QuotationId == quotationId).ToList();
                if(responses.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(responses);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Có lỗi xảy ra: " + e.Message);
            }
            finally
            {
                _context.Dispose(); // Giải phóng tài nguyên
            }

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddQuotationTemp(QuotationTempRequest quotationTemp)
        {
            try
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

                _context.Dispose(); // Giải phóng tài nguyên
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateQuotationTemp(QuotationTempRequest quotationTemp)
        {
            try
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
                QuotationTemp quotation = _context.QuotationTemps.FirstOrDefault(qt => qt.UserId == userId && qt.ProductId == quotationTemp.ProductId);
                if (quotation == null)
                {
                    return Ok("Product Id not exist in quotation of this person. Try Again!");
                }
                quotation.Quantity = quotationTemp.Quantity;
                _context.QuotationTemps.Update(quotation);
                _context.SaveChanges();
                _context.Dispose(); // Giải phóng tài nguyên
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SubmitQuotation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult SubmitQuotation(SubmitQuotationDTO req)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userIdValue = claim[1].Value;
                int userId = int.Parse(userIdValue);
                Quotation quotation = new Quotation();
                quotation.TotalBill = req.TotalConstructionCost + req.TotalProductCost;
                quotation.QuotationStatus = "Pending";
                quotation.CreatedAt = DateTime.Now;
                quotation.StyleId = req.StyleId;
                quotation.Square = req.Witdh * req.Length;
                quotation.UserId = userId;
                quotation.Status = 1;
                quotation.Witdh = req.Witdh;
                quotation.Height = req.Height;
                quotation.Length = req.Length;
                quotation.TotalConstructionCost = req.TotalConstructionCost;
                quotation.TotalProductCost = req.TotalProductCost;
                quotation.HomeStyleId = req.HomeStyleId;
                quotation.FloorConstructionId = req.FloorConstructionId;
                quotation.WallConstructId = req.WallConstructId;
                quotation.CeilingConstructId = req.CeilingConstructId;

                _context.Quotations.Add(quotation);
                _context.SaveChanges();
                Quotation savedquo = _context.Quotations.OrderByDescending(x => x.QuotationId).Take(1).FirstOrDefault();

                List<QuotationDetail> quotationDetails = new List<QuotationDetail>();
                foreach (QuotationDetailDTO quotationDetailDTO in req.quotationDetailDTOs)
                {

                    QuotationDetail quotationDetail = new QuotationDetail();
                    quotationDetail.ProductId = quotationDetailDTO.ProductId;
                    quotationDetail.Quantity = quotationDetailDTO.Quantity;
                    quotationDetail.Price = quotationDetailDTO.Price;
                    quotationDetail.QuotationId = savedquo.QuotationId;
                    quotationDetails.Add(quotationDetail);
                }
                _context.QuotationDetails.AddRange(quotationDetails);
                _context.SaveChanges();
                _context.Dispose(); // Giải phóng tài nguyên

                return Ok("Submit quotation successfully!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{userId}/{productId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteQuotationTemp(int productId)
        {
            try
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
                    _context.Dispose(); // Giải phóng tài nguyên
                    return Ok();

                }
                else
                {
                    return Ok("Product not exist in quotation of this user . Try Again!");

                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/ClearQuotation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ClearQuotation()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userIdValue = claim[1].Value;
                int userId = int.Parse(userIdValue);
                List<QuotationTemp> quotationTemps = _context.QuotationTemps.Where(qt => qt.UserId == userId).ToList();

                if (quotationTemps.Count != 0)
                {
                    _context.QuotationTemps.RemoveRange(quotationTemps);
                    _context.SaveChanges();
                    _context.Dispose(); // Giải phóng tài nguyên
                    return Ok();

                }
                else
                {
                    return Ok("Product not exist in quotation of this user . Try Again!");

                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
