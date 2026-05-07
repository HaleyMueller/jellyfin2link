using jellyfin2link.DataEntities.JellyfinModels;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace jellyfin2link.RESTClient.Requests
{
    public class GetItemsRequest
    {
        public Guid? UserId { get; set; }
        public string? MaxOfficialRating { get; set; }
        public bool? HasThemeSong { get; set; }
        public bool? HasThemeVideo { get; set; }
        public bool? HasSubtitles { get; set; }
        public bool? HasSpecialFeature { get; set; }
        public bool? HasTrailer { get; set; }
        public Guid? AdjacentTo { get; set; }
        public int? IndexNumber { get; set; }
        public int? ParentIndexNumber { get; set; }
        public bool? HasParentalRating { get; set; }
        public bool? IsHd { get; set; }
        public bool? Is4K { get; set; }
        public LocationType[]? LocationTypes { get; set; }
        public LocationType[]? ExcludeLocationTypes { get; set; }
        public bool? IsMissing { get; set; }
        public bool? IsUnaired { get; set; }
        public double? MinCommunityRating { get; set; }
        public double? MinCriticRating { get; set; }
        public DateTime? MinPremiereDate { get; set; }
        public DateTime? MinDateLastSaved { get; set; }
        public DateTime? MinDateLastSavedForUser { get; set; }
        public DateTime? MaxPremiereDate { get; set; }
        public bool? HasOverview { get; set; }
        public bool? HasImdbId { get; set; }
        public bool? HasTmdbId { get; set; }
        public bool? HasTvdbId { get; set; }
        public bool? IsMovie { get; set; }
        public bool? IsSeries { get; set; }
        public bool? IsNews { get; set; }
        public bool? IsKids { get; set; }
        public bool? IsSports { get; set; }
        public Guid[]? ExcludeItemIds { get; set; }
        public int? StartIndex { get; set; }
        public int? Limit { get; set; }
        public bool? Recursive { get; set; }
        public string? SearchTerm { get; set; }
        public SortOrder[]? SortOrder { get; set; }
        public Guid? ParentId { get; set; }
        public ItemFields[]? Fields { get; set; }
        public BaseItemKind[]? ExcludeItemTypes { get; set; }
        public BaseItemKind[]? IncludeItemTypes { get; set; }
        public ItemFilter[]? Filters { get; set; }
        public bool? IsFavorite { get; set; }
        public MediaType[]? MediaTypes { get; set; }
        public ImageType[]? ImageTypes { get; set; }
        public ItemSortBy[]? SortBy { get; set; }
        public bool? IsPlayed { get; set; }
        public string[]? Genres { get; set; }
        public string[]? OfficialRatings { get; set; }
        public string[]? Tags { get; set; }
        public int[]? Years { get; set; }
        public bool? EnableUserData { get; set; }
        public int? ImageTypeLimit { get; set; }
        public ImageType[]? EnableImageTypes { get; set; }
        public string? Person { get; set; }
        public Guid[]? PersonIds { get; set; }
        public string[]? PersonTypes { get; set; }
        public string[]? Studios { get; set; }
        public string[]? Artists { get; set; }
        public Guid[]? ExcludeArtistIds { get; set; }
        public Guid[]? ArtistIds { get; set; }
        public Guid[]? AlbumArtistIds { get; set; }
        public Guid[]? ContributingArtistIds { get; set; }
        public string[]? Albums { get; set; }
        public Guid[]? AlbumIds { get; set; }
        public Guid[]? Ids { get; set; }
        public VideoType[]? VideoTypes { get; set; }
        public string? MinOfficialRating { get; set; }
        public bool? IsLocked { get; set; }
        public bool? IsPlaceHolder { get; set; }
        public bool? HasOfficialRating { get; set; }
        public bool? CollapseBoxSetItems { get; set; }
        public int? MinWidth { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public bool? Is3D { get; set; }
        public SeriesStatus[]? SeriesStatus { get; set; }
        public string? NameStartsWithOrGreater { get; set; }
        public string? NameStartsWith { get; set; }
        public string? NameLessThan { get; set; }
        public Guid[]? StudioIds { get; set; }
        public Guid[]? GenreIds { get; set; }
        public bool EnableTotalRecordCount { get; set; } = true;
        public bool? EnableImages { get; set; } = true;


        public override string ToString()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var queryParts = properties
                .SelectMany(prop =>
                {
                    var value = prop.GetValue(this);
                    if (value == null) return Array.Empty<string>();

                    // Handle collections
                    if (value is IEnumerable enumerable && !(value is string))
                    {
                        var list = enumerable.Cast<object>().ToArray();
                        if (list.Length == 0) return Array.Empty<string>();
                        return new[] { $"{prop.Name}={string.Join(",", list)}" };
                    }

                    // Format DateTime in ISO 8601
                    if (value is DateTime dt)
                        return new[] { $"{prop.Name}={dt:O}" };

                    // Boolean values as lowercase
                    if (value is bool b)
                        return new[] { $"{prop.Name}={b.ToString().ToLower()}" };

                    return new[] { $"{prop.Name}={value}" };
                });

            return string.Join("&", queryParts);
        }
    }
}