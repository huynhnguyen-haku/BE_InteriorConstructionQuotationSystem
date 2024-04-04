using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SWP391API.DTO;
using SWP391API.Models;
using SWP391API.Repositories;
using SWP391API.Specifications;
using SWP391API.Utilities;
using System.Net.Mail;

namespace SWP391API.Services.Implements
{
    public class QuotationService : IQuotationService
    {
        private readonly Repository<Quotation> _quotationRepository;
        private readonly INotificationService _notificationService;

        private readonly IEmailService _emailService;

        public QuotationService(Repository<Quotation> quotationRepository, INotificationService notificationService, IEmailService emailService)
        {
            _quotationRepository = quotationRepository;
            _notificationService = notificationService;
            _emailService = emailService;
        }

        public async Task<QuotationResponseDTO> createQuotation(int userId, SubmitQuotationDTO req)
        {
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

            foreach (QuotationDetailDTO quotationDetailDTO in req.quotationDetailDTOs)
            {
                QuotationDetail quotationDetail = new QuotationDetail();
                quotationDetail.ProductId = quotationDetailDTO.ProductId;
                quotationDetail.Quantity = quotationDetailDTO.Quantity;
                quotationDetail.Price = quotationDetailDTO.Price;
                
                quotation.QuotationDetails.Add(quotationDetail);
            }

            quotation = await _quotationRepository.AddAsync(quotation);

            var spec = new QuotationByIdSpec(quotation.QuotationId);
            quotation = await _quotationRepository.FirstOrDefaultAsync(spec);

            await _notificationService.NewQuotationSubmitted(quotation);

            return new QuotationResponseDTO(quotation);
        }

        public async Task<List<QuotationResponseDTO>> getAllQuotations(QuotationFilterDTO quotationFilterDTO)
        {
            var spec = new QuotationGetAllSpec(quotationFilterDTO);
            List<Quotation> quotations = await _quotationRepository.ListAsync(spec);

            List<QuotationResponseDTO> quotationResponseDTOs = quotations.Select(q => new QuotationResponseDTO(q)).ToList();

            return quotationResponseDTOs;
        }

        public async Task<List<QuotationResponseDTO>> getQuotationsOfUser(int userId, QuotationFilterDTO quotationFilterDTO)
        {
            var spec = new QuotationByUserIdSpec(userId, quotationFilterDTO);
            List<Quotation> quotations = await _quotationRepository.ListAsync(spec);

            List<QuotationResponseDTO> quotationResponseDTOs = quotations.Select(q => new QuotationResponseDTO(q)).ToList();

            return quotationResponseDTOs;
        }

        public async Task sendFinalQuotationContractToUser(int quotationId)
        {
            var spec = new QuotationByIdSpec(quotationId);
            var quotation = await _quotationRepository.FirstOrDefaultAsync(spec);

            if (quotation == null)
            {
                throw new Exception(ErrorConstants.QuotationNotFound);
            }

            QuestPDF.Settings.License = LicenseType.Community;

            var pdfBytes = Document.Create(container =>
            {
                
                container.Page(page =>
                {

                    
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    
                    page.Content().Column(column =>
                    {
                        column.Item().AlignCenter().Text("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM").Bold().FontSize(13).FontFamily("Times New Roman");
                        column.Item().AlignCenter().Text("Độc lập – Tự do – Hạnh phúc").Bold().Underline().FontSize(13).FontFamily("Times New Roman");

                        column.Item().AlignRight().Text("Hà Nội, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year).FontSize(13).FontFamily("Times New Roman");

                        column.Spacing(2);

                        column.Item().AlignCenter().Text("HỢP ĐỒNG BÁO GIÁ THI CÔNG NỘI THẤT").Bold().FontSize(14).FontFamily("Times New Roman");

                        
                        column.Item().AlignCenter().Text("Số " + quotationId).FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text("- Căn cứ Bộ luật dân sự năm 2015;").FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("- Căn cứ nhu cầu và khả năng của các bên.;").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text("Hôm nay, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + ", chúng tôi gồm:").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text("Bên A:").Bold().FontSize(13).FontFamily("Times New Roman"); 
                        column.Item().Text("Công ty TNHH ICQS").FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Địa chỉ: Quận Hoàn Kiếm, TP Hà Nội").FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Điện thoại: 0123456789").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text("Bên B:").Bold().FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Ông/bà: " + quotation.User.Fullname).FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Chứng minh nhân dân/ Căn cước công dân số: .........................").FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Địa chỉ: ............................").FontSize(13).FontFamily("Times New Roman");
                        column.Item().Text("Điện thoại: .........................").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text("Sau khi thỏa thuận, hai bên đã ký kết hợp đồng báo giá thi công nội thất với các nội dung sau:").FontSize(13).FontFamily("Times New Roman").Bold();

                        column.Item().Text("Điều 1. Mục đích của hợp đồng").FontSize(13).FontFamily("Times New Roman").Bold();
                        column.Item().Text("Hợp đồng này được ký kết nhằm mục đích báo giá thi công nội thất cho công trình của Bên A.").FontSize(13).FontFamily("Times New Roman");


                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("STT").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Tên sản phẩm").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Giá").Bold().FontSize(13).FontFamily("Times New Roman");
                            });

                            table.Cell().Text("1").FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.HomeStyle.Name).FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.HomeStyle.Price + " VNĐ").FontSize(13).FontFamily("Times New Roman");

                            table.Cell().Text("2").FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.FloorConstruction.Name).FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.FloorConstruction.Price + " VNĐ").FontSize(13).FontFamily("Times New Roman");

                            table.Cell().Text("3").FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.WallConstruct.Name).FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.WallConstruct.Price + " VNĐ").FontSize(13).FontFamily("Times New Roman");

                            table.Cell().Text("4").FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.CeilingConstruct.Name).FontSize(13).FontFamily("Times New Roman");
                            table.Cell().Text(quotation.CeilingConstruct.Price + " VNĐ").FontSize(13).FontFamily("Times New Roman");

                        });

                        column.Item().Text("Với các nội thất đi kèm như sau:").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("STT").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Tên nội thất").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Số lượng").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Đơn giá").Bold().FontSize(13).FontFamily("Times New Roman");
                                header.Cell().Text("Tổng tiền").Bold().FontSize(13).FontFamily("Times New Roman");
                            });


                            var listDetails = quotation.QuotationDetails.ToList();

                            for(int i = 0; i < listDetails.Count; i++)
                            {
                                var detail = listDetails[i];

                                table.Cell().Text((i+1).ToString()).FontSize(13).FontFamily("Times New Roman");
                                table.Cell().Text(detail.Product.Name).FontSize(13).FontFamily("Times New Roman");
                                table.Cell().Text(detail.Quantity.ToString()).FontSize(13).FontFamily("Times New Roman");
                                table.Cell().Text(detail.Product.Price + " VNĐ").FontSize(13).FontFamily("Times New Roman");
                                table.Cell().Text(detail.Product.Price * detail.Quantity + " VNĐ").FontSize(13).FontFamily("Times New Roman");

                            }
                           
                        });

                    });


                });

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);


                    page.Content().Column(column =>
                    {
                        column.Item().Text("Điều 2. Giá và phương thức thanh toán").FontSize(13).FontFamily("Times New Roman").Bold();

                        column.Item().Text($"1. Tổng giá trị hợp đồng: {quotation.TotalBill} VNĐ đã bao gồm tiền công sản xuất, lắp đặt, hoàn thiện và các chi phí khác để tạo nên sản phẩm như nêu tại Điều 1 Hợp đồng này và mọi chi phí nộp thuế, phí nếu có.").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text($"2. Bên B thực hiện nghĩa vụ thanh toán tiền cho Bên A sau khi Bên A gửi đề nghị thanh toán và tuân thủ theo những thời điểm, sự kiện sau đây:").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text($"Lần 1: Bên B đồng ý thanh toán cho Bên A số tiền tương ứng 20% giá trị hợp đồng ngay sau khi ký Hợp đồng.").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text($"Lần 2:  Bên B đồng ý thanh toán cho Bên A số tiền tương ứng 30% giá trị hợp đồng tại thời điểm Bên A hoàn thiện toàn bộ sản phẩm thô.").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text($"Lần 3: Bên B đồng ý thanh toán cho Bên A số tiền còn lại của hợp đồng tại thời điểm Bên A bàn giao sản phẩm cho Bên B tại điểm do 2 bên thống nhất.").FontSize(13).FontFamily("Times New Roman");

                        column.Item().Text($"Phương thức thanh toán: Chuyển khoản vào số tài khoản Bên A.").FontSize(13).FontFamily("Times New Roman");






                    });

                });
            }).GeneratePdf();

            _emailService.sendToWithPdfAttachment(quotation.User.Email, "Hợp đồng báo giá thi công nội thất", "Bạn có một hợp đồng báo giá thi công nội thất mới, vui lòng kiểm tra tệp tin PDF sau", pdfBytes, "quotation_contract.pdf");

        }

        public async Task<QuotationResponseDTO> updateQuotation(UpdateQuotationDTO updateQuotationDTO)
        {
            var spec = new QuotationByIdSpec(updateQuotationDTO.QuotationId);
            var quotation = await _quotationRepository.FirstOrDefaultAsync(spec);

            if (quotation == null)
            {
                throw new Exception(ErrorConstants.QuotationNotFound);
            }

            quotation.TotalBill = updateQuotationDTO.TotalConstructionCost + updateQuotationDTO.TotalProductCost;
            quotation.QuotationStatus = "Pending";
            quotation.CreatedAt = DateTime.Now;
            quotation.StyleId = updateQuotationDTO.StyleId;
            quotation.Square = updateQuotationDTO.Witdh * updateQuotationDTO.Length;
            quotation.Witdh = updateQuotationDTO.Witdh;
            quotation.Height = updateQuotationDTO.Height;
            quotation.Length = updateQuotationDTO.Length;
            quotation.TotalConstructionCost = updateQuotationDTO.TotalConstructionCost;
            quotation.TotalProductCost = updateQuotationDTO.TotalProductCost;
            quotation.HomeStyleId = updateQuotationDTO.HomeStyleId;
            quotation.FloorConstructionId = updateQuotationDTO.FloorConstructionId;
            quotation.WallConstructId = updateQuotationDTO.WallConstructId;
            quotation.CeilingConstructId = updateQuotationDTO.CeilingConstructId;

            quotation.QuotationDetails.Clear();

            foreach (QuotationDetailDTO quotationDetailDTO in updateQuotationDTO.quotationDetailDTOs)
            {
                QuotationDetail quotationDetail = new QuotationDetail();
                quotationDetail.ProductId = quotationDetailDTO.ProductId;
                quotationDetail.Quantity = quotationDetailDTO.Quantity;
                quotationDetail.Price = quotationDetailDTO.Price;

                quotation.QuotationDetails.Add(quotationDetail);
            }

            await _quotationRepository.UpdateAsync(quotation);

            await _notificationService.QuotationUpdated(quotation);

            return new QuotationResponseDTO(quotation);
        }

        public async Task updateQuotationStatus(QuotationUpdateStatusDTO req)
        {
            var spec = new QuotationByIdSpec(req.QuotationId);
            Quotation quotation = await _quotationRepository.FirstOrDefaultAsync(spec);

            if (quotation == null)
            {
                throw new Exception(ErrorConstants.QuotationNotFound);
            }

            if (quotation.QuotationStatus != "Pending")
            {
                throw new Exception(ErrorConstants.QuotationNotPending);
            }

            quotation.QuotationStatus = req.QuotationStatus;

            await _quotationRepository.UpdateAsync(quotation);

            await _notificationService.QuotationStatusUpdated(quotation, req.Message);
            
        }
    }
}
