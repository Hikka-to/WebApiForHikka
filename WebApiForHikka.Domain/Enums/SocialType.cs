using NpgsqlTypes;

namespace WebApiForHikka.Domain.Enums;

public enum SocialType
{
    [PgName("website")] Website,

    [PgName("article")] Article,

    [PgName("book")] Book,

    [PgName("profile")] Profile,

    [PgName("video.other")] VideoOther,

    [PgName("video.movie")] VideoMovie,

    [PgName("video.episode")] VideoEpisode,

    [PgName("video.tv_show")] VideoTvShow,

    [PgName("music.song")] MusicSong,

    [PgName("music.album")] MusicAlbum,

    [PgName("music.playlist")] MusicPlaylist,

    [PgName("music.radio_station")] MusicRadioStation
}