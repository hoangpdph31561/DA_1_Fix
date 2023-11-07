using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Roombooking.Request;
using BaseSolution.Application.Interfaces.Repositories.ReadWrite;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ViewModels;

namespace BaseSolution.Infrastructure.ViewModels.Roombooking
{
    public class RoombookingDeleteViewModel : ViewModelBase<RoombookingDeleteRequest>
    {
        public readonly IRoombookingReadWriteRepository _roombookingReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        public RoombookingDeleteViewModel(IRoombookingReadWriteRepository roombookingReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _roombookingReadWriteRepository = roombookingReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public async override Task HandleAsync(RoombookingDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roombookingReadWriteRepository.DeleteRoomBookingAsync(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the Roombooking"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Roombooking")
                    }
                };
            }
        }
    }
}
