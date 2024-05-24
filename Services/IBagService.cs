using post_office_back.Dtos;

namespace post_office_back.Services
{
    public interface IBagService
    {
        HttpResponseMessage CreateBag(BagCreationDto bagCreationDto);
        HttpResponseMessage AddLetters(LetterAddingDto letterAddingDto);
    }
}
