using AutoMapper;
using WebAdmin.DTOModels.Request.Account;
using WebAdmin.DTOModels.Request.Artist;
using WebAdmin.DTOModels.Request.Genre;
using WebAdmin.DTOModels.Request.Item;
using WebAdmin.DTOModels.Request.Package;
using WebAdmin.DTOModels.Request.Report;
using WebAdmin.DTOModels.Request.Singer;
using WebAdmin.DTOModels.Request.Song;
using WebAdmin.DTOModels.Response;

namespace WebAdmin.Helpers
{
    public class AutoMapperResolver : Profile
    {
        public AutoMapperResolver()
        {
            CreateMap<UpdateItemRequestModel, Item>().ReverseMap();
            CreateMap<UpdateSongRequestModel, Song>().ReverseMap();
            CreateMap<SingerRequestModel, Singer>().ReverseMap();
            CreateMap<GenreRequestModel, Genre>().ReverseMap();
            CreateMap<ArtistRequestModel, Artist>().ReverseMap();
            CreateMap<UpdatePackageRequestModel, Package>().ReverseMap();
            CreateMap<SongArtistRequestModel, SongArtist>().ReverseMap();
            CreateMap<SongGenreRequestModel, SongGenre>().ReverseMap();
            CreateMap<SongSingerRequestModel, SongSinger>().ReverseMap();
            CreateMap<CreateSongRequestModel, CreateSongRequestModel1>().ReverseMap();
            CreateMap<Report, UpdateReportRequestModel>().ReverseMap();
            CreateMap<Account, UpdateAccountRequestModel>().ReverseMap();

        }
    }
}