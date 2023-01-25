using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Profiles;

public class LibraryProfile : AutoMapper.Profile
{
    public LibraryProfile()
    {
        // Source => Target
        CreateMap<Book, BookDto>();

    }
}