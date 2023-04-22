using Microsoft.AspNetCore.Mvc;

namespace ConferencePlanning.DTO.ConferenceDto;

public class ConferenceWithImgDto
{
    public IFormFile Img { get; set; }
    
    [ModelBinder(BinderType = typeof(FormDataJsonBinder))]
    public ConferenceDto ConferenceDto { get; set; }
}