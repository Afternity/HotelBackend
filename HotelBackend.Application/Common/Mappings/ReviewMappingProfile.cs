using AutoMapper;
using HotelBackend.Domain.Models;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.CreateReviewDTOs;
using HotelBackend.Shared.Contracts.DTOs.ReviewDTOs.UpdateReviewDTOs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewDetailsVMs;
using HotelBackend.Shared.Contracts.VMs.ReviewVMs.ReviewLookupDTOs;

namespace HotelBackend.Application.Common.Mappings
{
    public class ReviewMappingProfile
        : Profile
    {
        public ReviewMappingProfile()
        {
            // createDto
            CreateMap<CreateReviewDto, Review>();

            // updateDto
            CreateMap<UpdateReviewDto, Review>();

            // detailsVm
            CreateMap<Review, ReviewDetailsVm>();

            // lookupDto
            CreateMap<Review, ReviewLookupDto>();
            CreateMap<Review, RatingAndRoomReviewLookupDto>();
        }
    }
}
