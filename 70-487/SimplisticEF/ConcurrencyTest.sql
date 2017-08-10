select *
  from music.Artist
 where ArtistId = 1

update music.Artist
   set Name = 'Kino - from SSMS'
 where ArtistId = 1

select *
  from music.Artist
 where ArtistId = 1


select *
  from music.Album
 where AlbumId = 3

update music.Album
   set Price = Price + 0.01,
       [Version] = [Version] + 1
 where AlbumId = 3

select *
  from music.Album
 where AlbumId = 3