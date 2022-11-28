using System.ComponentModel.DataAnnotations;
using webAplication.Domain;
using webAplication.Domain.Interfaces;

namespace webAplication.Service.Models;


public class RegisterViewModel
{
    public string name { get; set; }

    public string role { get; set; }
}