using MediatR;
using Hospital.Application.Interfaces;
using Hospital.Application.Exceptions;
using QRCoder;
using Hospital.Application.Features.MedicineInventory.Queries; 


public class GetMedicineQrCodeQueryHandler : IRequestHandler<GetMedicineQrCodeQuery, byte[]>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMedicineQrCodeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<byte[]> Handle(GetMedicineQrCodeQuery request, CancellationToken cancellationToken)
    {
        var medicine = await _unitOfWork.Medicines.GetByIdAsync(request.MedicineId);
        if (medicine == null || string.IsNullOrEmpty(medicine.QRCodeData))
        {
            throw new NotFoundException("Medicine or QR Code data not found.");
        }

        //  Generate the QR Code Image 
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(medicine.QRCodeData, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeImageBytes = qrCode.GetGraphic(20);
       

        return qrCodeImageBytes;
    }
}