using System.ComponentModel.DataAnnotations;
using DataType = OfficeOpenXml.FormulaParsing.ExpressionGraph.DataType;

namespace webAplication.Service.Models;

public class JsonModel
{
    [Required]
    //[DataType(DataType)]
    public string text { get; set; }
}