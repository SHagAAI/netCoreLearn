using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCoreMovieCRUD.Models;
using Microsoft.EntityFrameworkCore;
using netCore.Data;
using netCore.DTOs;
using NetCoreLearn.MyServiceResult;
using NetCoreLearn.Repository;

namespace Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IMapper _mapper;
        private readonly IDirectorRepository _directorRepo;

        public DirectorService(IDirectorRepository directorRepo, IMapper mapper)
        {
            _directorRepo = directorRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DirectorResponseDto>> CreateDirectorAsync(CreateDirectorDto newDirectorDto)
        {
            var director = _mapper.Map<Director>(newDirectorDto);

            await _directorRepo.InsertDirectorAsync(director);
            await _directorRepo.SaveChangesAsync();

            var responseDirectorDto = _mapper.Map<DirectorResponseDto>(director);

            return ServiceResult<DirectorResponseDto>.Ok(responseDirectorDto);
        }

        public async Task<ServiceResult<IEnumerable<DirectorResponseDto>>> GetAllDirectorAsync()
        {
            var directors = await _directorRepo.GetAllDirectorsAsync();
            // Only if your teacher specifically requires 404 when empty:
            if (!directors.Any())
            {
                return ServiceResult<IEnumerable<DirectorResponseDto>>.NotFound("No directors found in the database.");
            }
            var responseDtos = _mapper.Map<IEnumerable<DirectorResponseDto>>(directors);
            return ServiceResult<IEnumerable<DirectorResponseDto>>.Ok(responseDtos);
        }

        public async Task<ServiceResult<DirectorResponseDto>> GetDirectorByIdAsync(int id)
        {
            var director = await _directorRepo.GetDirectorByIdAsync(id);

            if (director == null)
            {
                return ServiceResult<DirectorResponseDto>.NotFound($"Director with ID {id} was not found.");
            }

            var responseDto = _mapper.Map<DirectorResponseDto>(director);

            return ServiceResult<DirectorResponseDto>.Ok(responseDto);
        }

        public async Task<ServiceResult<DirectorResponseDto>> UpdateDirectorAsync(int id, UpdateDirectorDto updateDirectorDto)
        {
            var director = await _directorRepo.GetDirectorByIdAsync(id);
            if (director == null)
            {
                return ServiceResult<DirectorResponseDto>.NotFound($"Director with ID {id} was not found.");
            }



            // director.Name = dto.Name;
            _mapper.Map(updateDirectorDto, director);
            await _directorRepo.SaveChangesAsync();

            var responseUpdateDirectorDto = _mapper.Map<DirectorResponseDto>(director);

            return ServiceResult<DirectorResponseDto>.Ok(responseUpdateDirectorDto);
        }

        public async Task<ServiceResult> DeleteDirectorAsync(int id)
        {
            var director = await _directorRepo.GetDirectorByIdAsync(id);
            if (director == null)
            {
                return ServiceResult.NotFound($"Director with ID {id} was not found.");
            }

            await _directorRepo.DeleteDirectorAsync(id);
            await _directorRepo.SaveChangesAsync();

            return ServiceResult.Ok($"Director with ID {id} was deleted successfully.");
        }
    }
}