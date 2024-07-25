
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.SearchHistory;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class SearchHistory : Model
{
    [StringLength(SearchHistoryNumberConstants.QuearyLength)]
    public required string Query {  get; set; }
    public required DateTime CreateAt { get; set; }
}
