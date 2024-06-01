using post_office_back.Dtos;

namespace post_office_back.Services
{
    public interface IBagService
    {
        void CreateBag(BagCreationDto bagCreationDto);
        void AddLetters(LetterAddingDto letterAddingDto);
    }
}
