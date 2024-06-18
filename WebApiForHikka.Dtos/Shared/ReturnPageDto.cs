
using TypeGen.Core.TypeAnnotations;

namespace WebApiForHikka.Dtos.Shared;



[ExportTsInterface(OutputDir = "./TS/Shared/")]
public class ReturnPageDto<T>
{
    public required IReadOnlyCollection<T> Models { get; set; }
    public required int HowManyPages { get; set; }
}