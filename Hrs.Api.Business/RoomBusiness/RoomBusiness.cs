using Hrs.Api.Repository.Models;
using Hrs.Api.Repository.UnitOfWork;
using Hrs.Api.Shared;
using Hrs.Api.Shared.Dtos;

namespace Hrs.Api.Business.RoomBusiness;

public class RoomBusiness(IUnitOfWork unitOfWork) : IRoomBusiness
{
    public async Task<ApiResponse<List<RoomDto>>> GetRoomsAsync()
    {
        ApiResponse<List<RoomDto>> response = new ApiResponse<List<RoomDto>>();
        var repository = unitOfWork.Repository<Room>();
        var entity = await repository.GetAllAsync();
        if (entity == null)
        {
            response.Success = false;
            response.Message = "No rooms found.";
        }
        else
        {
            var roomDtos = entity.Select(h => new RoomDto()
            {
                Id = h.Id,
                HotelId = h.HotelId,
                RoomNumber = h.RoomNumber,
                Type = h.Type,
                PricePerNight = h.PricePerNight,
                IsAvailable = h.IsAvailable
            }).ToList();  
            
            response.Success = true;
            response.Message = "Rooms retrieved successfully.";
            response.Data = roomDtos;
        }
        return response;
    }

    public async Task<ApiResponse<RoomDto>> GetRoomByIdAsync(int id)
    {
        ApiResponse<RoomDto> response = new ApiResponse<RoomDto>();
        var repository = unitOfWork.Repository<Room>();
        var roomDetails = await repository.GetByIdAsync(id);
        if (roomDetails == null)
        {
            response.Success = false;
        }
        else
        {
            RoomDto roomDto = new RoomDto()
            {
                Id = roomDetails.Id,
                HotelId = roomDetails.HotelId,
                RoomNumber = roomDetails.RoomNumber,
                Type = roomDetails.Type,
                PricePerNight = roomDetails.PricePerNight,
                IsAvailable = roomDetails.IsAvailable
            };
            response.Success = true;
            response.Data = roomDto;
        }
        return response;
    }

    public async Task<ApiResponse<RoomDto>> CreateRoomAsync(CreateRoomDto roomDto)
    {
        ApiResponse<RoomDto> response = new ApiResponse<RoomDto>();
        var repository = unitOfWork.Repository<Room>();
        Room room = new Room()
        {
           HotelId = roomDto.HotelId,
           RoomNumber = roomDto.RoomNumber,
           Type = roomDto.Type,
           PricePerNight = roomDto.PricePerNight,
           IsAvailable = roomDto.IsAvailable
        };
       
        await repository.AddAsync(room);
        await repository.SaveChangesAsync();
        response.Success = true;
        return response;
    }

    public async Task<ApiResponse<RoomDto>> UpdateRoomAsync(UpdateRoomDto roomDto)
    {
        ApiResponse<RoomDto> response = new ApiResponse<RoomDto>();
        var repository = unitOfWork.Repository<Room>();
        Room room = new Room()
        {
            RoomNumber = roomDto.RoomNumber,
            Type = roomDto.Type,
            PricePerNight = roomDto.PricePerNight,
            IsAvailable = roomDto.IsAvailable
        };
        repository.Update(room);
        response.Success = true;
        return response;
    }

    public async Task<ApiResponse<RoomDto>> UpdateRoomStatusAsync(UpdateRoomDto updateRoomDto)
    {
        ApiResponse<RoomDto> response = new ApiResponse<RoomDto>();
        var repository = unitOfWork.Repository<Room>();
        Room room = new Room()
        {
            IsAvailable = updateRoomDto.IsAvailable
        };
        repository.Update(room);
        response.Success = true;
        return response;
    }
}