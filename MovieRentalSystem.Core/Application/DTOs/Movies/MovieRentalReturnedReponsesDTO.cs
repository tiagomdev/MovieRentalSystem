

namespace MovieRentalSystem.Core.Application.DTOs.Movies
{
    public class MovieRentalReturnedReponsesDTO
    {
        public MovieRentalReturnedReponsesDTO(bool passedDeliveryDay)
        {
            if(passedDeliveryDay)
            {
                Status = "Alert";
                Message = "The film was returned late";
            }
            else
            {
                Status = "Success";
                Message = "The film was returned within";
            }
        }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}
