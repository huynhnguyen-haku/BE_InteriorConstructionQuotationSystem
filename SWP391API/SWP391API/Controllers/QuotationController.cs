using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Services;

namespace SWP391API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly InteriorConstructionQuotationSystemContext _context;
        private readonly IQuotationService _quotationService;
        private readonly IAuthenticateService _authenticateService;

        public QuotationController(InteriorConstructionQuotationSystemContext context, IQuotationService quotationService, IAuthenticateService authenticateService)
        {
            _context = context;
            _quotationService = quotationService;
            _authenticateService = authenticateService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetQuotationTemps()
        {
            try
            {

                int uID = _authenticateService.getCurrentUserId();

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
        public async Task<IActionResult> GetSubmitedQuotationsOfUser([FromQuery] QuotationFilterDTO quotationFilterDTO)
        {
            try
            {
                int uID = _authenticateService.getCurrentUserId();

                List<QuotationResponseDTO> quotations = await _quotationService.getQuotationsOfUser(uID, quotationFilterDTO);

                return Ok(quotations);
            }
            catch(Exception ex)
            {
                return BadRequest(new ErrorDTO(ex.Message));
            }
        }

        [HttpPost("{id}/send")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendFinalQuotationContractToUser(int id)
        {
            try
            {
                await _quotationService.sendFinalQuotationContractToUser(id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message + " " + e.StackTrace));
            }
        }


        [HttpGet("GetAllSubmitedQuotations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllSubmitedQuotations([FromQuery] QuotationFilterDTO quotationFilterDTO)
        {
            try
            {
                List<QuotationResponseDTO> quotations = await _quotationService.getAllQuotations(quotationFilterDTO);

                return Ok(quotations);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
      
        }


        [HttpGet("GetSubmitedQuotationById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetSubmitedQuotationById(int quotationId)
        {
            try
            {                

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
                int userId = _authenticateService.getCurrentUserId();

                var product = _context.Products.FirstOrDefault(x => x.ProductId == quotationTemp.ProductId);
                if (product == null)
                {
                    return Ok("Product Id not exist. Try Again!");
                }
                
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
        public async Task<IActionResult> UpdateWholeQuotation(UpdateQuotationDTO updateQuotationDTO)
        {
            try
            {
                int userId = _authenticateService.getCurrentUserId();

                QuotationResponseDTO quotationDTO = await _quotationService.updateQuotation(updateQuotationDTO);

                return Ok(quotationDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }

        }

        [HttpPatch("Status")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateQuotationStatus([FromBody] QuotationUpdateStatusDTO quotationUpdateStatusDTO)
        {
            try
            {
               await _quotationService.updateQuotationStatus(quotationUpdateStatusDTO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }
        }


        [HttpPost("SubmitQuotation")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SubmitQuotation(SubmitQuotationDTO req)
        {
            try
            {
                int userId = _authenticateService.getCurrentUserId();
                
                QuotationResponseDTO quotationDTO = await _quotationService.createQuotation(userId, req);

                return Ok(quotationDTO);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorDTO(e.Message));
            }

        }

        [HttpDelete("{userId}/{productId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteQuotationTemp(int productId)
        {
            try
            {
                int userId = _authenticateService.getCurrentUserId();

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
                int userId = _authenticateService.getCurrentUserId();

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
